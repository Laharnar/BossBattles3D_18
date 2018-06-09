using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class TriggerAnimation : AnimationEvent {
    public string triggerName;

    public override IEnumerator RunAnimation(Animator anim) {
        if (anim) {
            anim.SetTrigger(triggerName);
        }
        yield return new WaitForSeconds(animLenght);
        yield return null;
    }
    public TriggerAnimation(string attackName, float length, string triggerName):base(attackName, length) {
        this.triggerName = triggerName;
    }
}
