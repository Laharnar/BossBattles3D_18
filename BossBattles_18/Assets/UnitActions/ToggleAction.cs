using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class ToggleAction : UnitAction {
    public BoolAnimation toggleAnim;

    public override IEnumerator RunAction() {
        //Debug.Log("RNNUNGNGA");
        yield return null;
    }

    internal void RunToggleAction() {
        toggleAnim.RunBoolAnimation(activeSource.anim);

    }
}
