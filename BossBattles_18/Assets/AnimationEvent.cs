using System.Collections;
using UnityEngine;

public abstract class AnimationEvent {
    public string name;
    public float animLenght;

    public AnimationEvent(string attackName, float length) {
        this.name = attackName;
        this.animLenght = length;
    }

    public abstract IEnumerator RunAnimation(Animator anim);

    public abstract void RunBoolAnimation(Animator anim);
}
