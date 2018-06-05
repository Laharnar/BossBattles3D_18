using System;
using System.Collections;
using UnityEngine;
[System.Serializable]
public class DashAction : UnitAction {
    public float dashLength = 10f;
    public float dashTime = 1f;


    public TriggerAnimation rollLeft = new TriggerAnimation("RollLeft", 1, "RollLeft");
    public TriggerAnimation rollRight = new TriggerAnimation("RollRight", 1, "RollRight");

    public override IEnumerator RunAction() {
        UnitController source = activeSource;
        source.SetLock(true);
        float hraw = source.hraw;
        float vraw = source.vraw;
        Animator anim = source.anim;
        if ((int)hraw == -1 || (int)vraw == 1) {
            source.StartCoroutine(rollLeft.RunAnimation(anim));
        } else if ((int)hraw == 1 || (int)vraw == -1) {
            source.StartCoroutine(rollRight.RunAnimation(anim));
        }

        Vector3 d = Vector3.right * hraw + Vector3.forward * vraw;
        Vector3 camd = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * d;// Camera.main.ScreenToWorldPoint(d);
        source.tTarget.forward = camd;
        float t = Time.time + dashTime;

        Vector3 dashDirection;
        if (hraw == 0 && vraw == 0)
            dashDirection = Vector3.back;// backstep
        else dashDirection = Vector3.forward;// roll
        
        float dspeed = dashLength / dashTime;

        while (Time.time <= t) {
            if (source.dead) break;
            source.RigMove(dashDirection * dashLength, dspeed);//pot=hitrost*čas//rigmove
            yield return null;
        }
        source.SetLock(false);
        Debug.Log("Dash action end.");
    }

}
