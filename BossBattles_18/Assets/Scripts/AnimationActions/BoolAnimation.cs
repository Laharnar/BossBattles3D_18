using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class BoolAnimation : AnimationEvent {

    public string triggerName;

    public bool value;

    public override void RunBoolAnimation(Animator anim) {
        if (anim) {
            anim.SetBool(triggerName, value);
        }
    }
    
    public override IEnumerator RunAnimation(Animator anim) {
        throw new NotImplementedException("Don't use this call");
    }

    public BoolAnimation(string name, float length, string triggerName) : base(name, length) {
        this.triggerName = triggerName;
    }
}
