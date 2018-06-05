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
        Debug.Log("Dash action.");
        activeSource.SetLock(true);
        float hraw = activeSource.hraw;
        float vraw = activeSource.vraw;
        Animator anim = activeSource.anim;
        if ((int)hraw == -1 || (int)vraw == 1) {
            activeSource.StartCoroutine(rollLeft.RunAnimation(anim));
        } else if ((int)hraw == 1 || (int)vraw == -1) {
            activeSource.StartCoroutine(rollRight.RunAnimation(anim));
        }

        Vector3 d = Vector3.right * hraw + Vector3.forward * vraw;
        Vector3 camd = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * d;// Camera.main.ScreenToWorldPoint(d);
        activeSource.tTarget.forward = camd;
        float t = Time.time + dashTime;

        Vector3 dashDirection;
        if (hraw == 0 && vraw == 0)
            dashDirection = Vector3.back;// backstep
        else dashDirection = Vector3.forward;// roll
        
        float dspeed = dashLength / dashTime;

        while (Time.time <= t) {
            if (activeSource.dead) break;
            activeSource.RigMove(dashDirection * dashLength, dspeed);//pot=hitrost*čas//rigmove
            yield return null;
        }
        activeSource.SetLock(false);
        Debug.Log("Dash action end.");
    }

}
