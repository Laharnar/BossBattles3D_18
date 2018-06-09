using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UnitController : MonoBehaviour {
    public int alliance;

}

// Damage and hp
public partial class UnitController:MonoBehaviour {

    internal bool dead = false;
    public int health = 50;
    public Weapon activeWeapon;
    public ToggleAction damagedAction;

    public void TryDamage(Weapon weapon) {

        if (dead) return;

        StartCoroutine(Damaged());

        health -= weapon.baseDamage;
        if (health <= 0) {
            dead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Damaged() {
        anim.SetTrigger("Attacked");
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Die() {
        yield return null;// wait animation to end. And/or fade out screen.
        Destroy(transform.root.gameObject);
    }
}
// attacking
public partial class UnitController : MonoBehaviour {
    public UnitControls ctr;

    public Animator anim;

    internal void SetAttacking(bool v) {
        if (activeWeapon)
            activeWeapon.attackPlaying = v;
    }
    IEnumerator UpdateAttacks() {
        while (true) {
            if (dead) break;
            if (!controlsLocked) {
                UnitAction.activeSource = this;
                bool[] attackInput = ctr.GetInputs();

                // Note: With toggle actions, like blocking, only the first one will be activated.
                bool toggleAction = false;
                UnitAction[] actions = ctr.GetActions();
                for (int i = 0; i < actions.Length && i < attackInput.Length; i++) {
                    // With toggle actions, we just activate them if we can.
                    if (actions[i].GetType() == typeof(ToggleAction)) {
                        (actions[i] as ToggleAction).toggleAnim.value = attackInput[i] && !toggleAction;
                        (actions[i] as ToggleAction).RunToggleAction();
                        if (!toggleAction) { // no toggles are active this frame so far
                            toggleAction = true;
                        }
                    }
                    else if (attackInput[i]) {
                        // With all others we wait until they end. Active toggle action, like blocking will be interrupted.
                        UnitAction.activeSource = this;
                        yield return StartCoroutine(actions[i].RunAction());
                    }
                }
            }
            yield return null;
        }
    }
}
public partial class UnitController : MonoBehaviour {

    public Transform tTarget;

    public float speed = 5f;
    public float rotationSpeedDegrees = 5f;

    public Rigidbody rig;

    [SerializeField] internal bool controlsLocked = false;

    internal float hraw, vraw;
    public BoolAnimation running;
    
    // Use this for initialization
    void Start() {
        if (!rig)
            rig = GetComponent<Rigidbody>();


        StartCoroutine(UpdateAttacks());
    }

    // Update is called once per frame
    void Update() {
        if (dead) return;
        //bool dashCommand = ctr.GetInputs()[3];

        Vector3 raw = ctr.GetRawDirectionInput();
        hraw = raw.x;
        vraw = raw.z;

        if (!controlsLocked) {
            running.value = hraw != 0 || vraw != 0;
            running.RunBoolAnimation(anim);
            Move();
        }

        ctr.PostMoveUpdate();
    }
    public void MoveCustom(float h, float v) {
        Vector3 calcMove = new Vector3(h, 0, v);
        TurnTowards(calcMove, v, h);

        // insta stop: (vraw*vraw+hraw*hraw)/2
        RigMove(Vector3.forward * (v * v + h * h) / 2, speed);

    }
    public void Move() {
        Vector3 dirVector = ctr.GetDirSmooth();
        MoveCustom(dirVector.x, dirVector.z);
    }

    private void TurnTowards(Vector3 dirFromCam, float v, float h) {
        if (h != 0 || v != 0) {
            Vector3 p = Vector3.RotateTowards(tTarget.forward, dirFromCam, 2 * Mathf.PI / 360 * rotationSpeedDegrees * Time.deltaTime * 10, 0);
            tTarget.forward = p;
        }
    }

    public void RigMove(Vector3 dir, float speed) {
        dir = dir.normalized;
        rig.MovePosition(tTarget.position + tTarget.TransformDirection(dir) * speed * Time.deltaTime);
    }

    public void SetLock(bool v) {
        controlsLocked = v;
    }
}
