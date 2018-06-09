using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class AnimationEvent {
    public string name;
    public float animLenght;

    public AnimationEvent(string attackName, float length) {
        this.name = attackName;
        this.animLenght = length;
    }

    public virtual IEnumerator RunAnimation(Animator anim) {
        yield return new WaitForSeconds(animLenght);
        yield return null;
    }

    public virtual void RunBoolAnimation(Animator anim) {
    }

}

public class SpawnEvent :AnimationEvent{

    public Transform pref;

    public SpawnEvent(string attackName, float length, Transform pref):base(attackName, length) {
        this.pref = pref;
    }

    public override IEnumerator RunAnimation(Animator anim) {
        GameObject.Instantiate(pref, anim.transform.position, new Quaternion());
        yield return new WaitForSeconds(animLenght);
        yield return null;
    }

    public virtual void RunBoolAnimation(Animator anim) {

    }
}
