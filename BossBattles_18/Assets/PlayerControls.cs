using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attacking
public partial class PlayerControls : MonoBehaviour {

    int comboCounter;
    public KeyCode lightAttackCode;
    public KeyCode strongAttackCode;
    public KeyCode blockCode;

    bool blocking = false;

    public Animator anim;

    string runningAnimation;

    public float lightAttackLength=0.5f;
    public float strongAttackLength = 1f;

    IEnumerator UpdateAttacks() {
        while (true) {
            if (!controlsLocked) {
                // combos
                if (Input.GetKeyDown(lightAttackCode)) {
                    controlsLocked = true;
                    yield return StartCoroutine(LightAttack());
                    controlsLocked = false;
                }
                if (Input.GetKeyDown(strongAttackCode)) {
                    controlsLocked = true;
                    yield return StartCoroutine(StrongAttack());
                    controlsLocked = false;
                }

                // blocking
                if (!controlsLocked && Input.GetKey(blockCode)) {
                    runningAnimation = "Blocking";
                    blocking = true;
                } else {
                    blocking = false;
                }
                anim.SetBool("Blocking", blocking);
            }
            yield return null;
        }
    }

    IEnumerator LightAttack() {
        runningAnimation = "LightCombo";
        AnimTrigger("LightCombo");
        yield return new WaitForSeconds(lightAttackLength);
        yield return null;
    }

    IEnumerator StrongAttack() {
        runningAnimation = "StrongCombo";
        AnimTrigger("StrongCombo");
        yield return new WaitForSeconds(strongAttackLength);
        yield return null;
    }

    /// <summary>
    /// Call from animations, by hand.
    /// </summary>
    public void AnimationEnd() {
        runningAnimation = "";
    }
}

// Dashing
public partial class PlayerControls:MonoBehaviour {
    public float dashLength = 10f;
    public float dashTime = 1f;

    void Dash() {
        Vector3 dashDirection;
        if (hraw == 0 && vraw == 0) {
            dashDirection = Vector3.back;// backstep
            //return;
        } else
        {
            dashDirection = Vector3.forward;// roll
        }
        float dspeed = dashLength / dashTime;
        RigMove(dashDirection*dashLength, dspeed);//pot=hitrost*čas
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

        Vector3 d =  Vector3.right * hraw + Vector3.forward * vraw;
        transform.forward = d;//Camera.main.ScreenToWorldPoint(d);
        float t = Time.time + dashTime;
        while (Time.time <= t) {
            Dash();
            yield return null;
        }
        controlsLocked = false;
    }

}

public partial class PlayerControls : MonoBehaviour {

    public float speed = 5f;
    public float rotationSpeedDegrees = 5f;
    
    public Rigidbody rig;

    public CameraControls cam;

    float lastHraw;

    [SerializeField] bool controlsLocked = false;

    protected float hraw, vraw;
    protected float rot;

    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();

        StartCoroutine(UpdateAttacks());
	}
	
	// Update is called once per frame
	void Update () {
        hraw = Input.GetAxisRaw("Horizontal");
        vraw = Input.GetAxisRaw("Vertical");
        rot = Input.GetAxis("Mouse X");

        if (!controlsLocked) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                controlsLocked = true;
                StartCoroutine(CinemaDash());
            } else {
                Move();
            }
        }

        cam.Move();
        cam.Rotate(rot);
        lastHraw = hraw;
    }


    private void Move() {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (anim) {
            anim.SetBool("Running", hraw!= 0 || vraw!= 0);
        }
        Vector3 dir = new Vector3(h, 0, v);
        Vector3 dirRaw = new Vector3(hraw, 0, vraw);
        Vector3 camf = Camera.main.transform.forward;
        Vector3 dirFromCam = Camera.main.transform.TransformDirection(dirRaw.normalized);

        if (h != 0 || v != 0) {
            Vector3 p = Vector3.RotateTowards(transform.forward, dirFromCam, 2 * Mathf.PI / 360 * rotationSpeedDegrees * Time.deltaTime * 10, 0);
            transform.forward = p;
        }

        // insta stop: (vraw*vraw+hraw*hraw)/2
        RigMove(Vector3.forward * (v * v + h * h) / 2, speed);

    }

    private void RigMove(Vector3 dir, float speed) {
        dir = dir.normalized;
        rig.MovePosition(transform.position+transform.TransformDirection(dir) * speed * Time.deltaTime);
    }
}
