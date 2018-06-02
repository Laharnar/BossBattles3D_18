using System.Collections;
using UnityEngine;
// attacking
public partial class UnitController : MonoBehaviour {
    public IUnitControls ctr;

    int comboCounter;

    public Animator anim;

    // TODO: separate into list of scriptable objects, or separate script.
    public TriggerAnimation lightAttack = new TriggerAnimation("LightCombo", 0.8f, "LightCombo");
    public TriggerAnimation strongAttack = new TriggerAnimation("StrongCombo", 1.0f, "StrongCombo");
    public BoolAnimation blockAction = new BoolAnimation("Blocking", 1.0f, "Blocking");

    IEnumerator UpdateAttacks() {
        while (true) {
            if (!controlsLocked) {
                bool[] attackInput = ctr.GetInputs();
                bool lightComboCode = attackInput[0];
                bool strongComboCode = attackInput[1]; 
                bool blockCode = attackInput[2];

                // combos
                if (lightComboCode) {
                    controlsLocked = true;
                    yield return StartCoroutine(lightAttack.RunAnimation(anim));
                    controlsLocked = false;
                }
                else if (strongComboCode) {
                    controlsLocked = true;
                    yield return StartCoroutine(strongAttack.RunAnimation(anim));
                    controlsLocked = false;
                }
                // blocking
                if (!controlsLocked && blockCode) {
                    blockAction.value = true;
                } else {
                    blockAction.value = false;
                }
                blockAction.RunBoolAnimation(anim);
            }
            yield return null;
        }
    }

}

// Dashing
public partial class UnitController : MonoBehaviour {
    public float dashLength = 10f;
    public float dashTime = 1f;

    void Dash() {
        Vector3 dashDirection;
        if (hraw == 0 && vraw == 0) {
            dashDirection = Vector3.back;// backstep
            //return;
        } else {
            dashDirection = Vector3.forward;// roll
        }
        float dspeed = dashLength / dashTime;
        RigMove(dashDirection * dashLength, dspeed);//pot=hitrost*čas
    }

    void AnimTrigger(string name) {
        if (anim)
            anim.SetTrigger(name);
    }
    private IEnumerator CinemaDash() {
        controlsLocked = true;
        if ((int)hraw == -1 || (int)vraw == 1) {
            AnimTrigger("RollLeft");
        } else if ((int)hraw == 1 || (int)vraw == -1) {
            AnimTrigger("RollRight");
        }

        Vector3 d = Vector3.right * hraw + Vector3.forward * vraw;
        tTarget.forward = d;//Camera.main.ScreenToWorldPoint(d);
        float t = Time.time + dashTime;
        while (Time.time <= t) {
            Dash();
            yield return null;
        }
        controlsLocked = false;
    }
}

public partial class UnitController : MonoBehaviour {

    public Transform tTarget;

    public float speed = 5f;
    public float rotationSpeedDegrees = 5f;

    public Rigidbody rig;

    [SerializeField] bool controlsLocked = false;

    protected float hraw, vraw;

    // Use this for initialization
    void Start() {
        if (!rig)
            rig = GetComponent<Rigidbody>();

        StartCoroutine(UpdateAttacks());
    }

    // Update is called once per frame
    void Update() {
        bool dashCommand = ctr.GetInputs()[3];

        Vector3 raw = ctr.GetRawDirectionInput();
        hraw = raw.x;
        vraw = raw.z;

        if (!controlsLocked) {
            if (dashCommand) {
                controlsLocked = true;
                StartCoroutine(CinemaDash());
            } else {
                if (anim) {
                    anim.SetBool("Running", hraw != 0 || vraw != 0);
                }
                Move();
            }
        }

        ctr.PostMoveUpdate();
    }

    private void Move() {
        Vector3 dirVector = ctr.GetDirSmooth();
        float h = dirVector.x;
        float v = dirVector.z;

        Vector3 dirFromCam = CalcMoveDir();
        TurnTowards(dirFromCam, v, h);

        // insta stop: (vraw*vraw+hraw*hraw)/2
        RigMove(Vector3.forward * (v * v + h * h) / 2, speed);

    }

    // TODO: export as player specific calculaction
    private Vector3 CalcMoveDir() {
        Vector3 dirRaw = new Vector3(hraw, 0, vraw);
        Vector3 camf = Camera.main.transform.forward;
        Vector3 dirFromCam = Camera.main.transform.TransformDirection(dirRaw.normalized);
        return dirFromCam;
    }

    private void TurnTowards(Vector3 dirFromCam, float v, float h) {
        if (h != 0 || v != 0) {
            Vector3 p = Vector3.RotateTowards(tTarget.forward, dirFromCam, 2 * Mathf.PI / 360 * rotationSpeedDegrees * Time.deltaTime * 10, 0);
            tTarget.forward = p;
        }
    }

    private void RigMove(Vector3 dir, float speed) {
        dir = dir.normalized;
        rig.MovePosition(tTarget.position + tTarget.TransformDirection(dir) * speed * Time.deltaTime);
    }
}
