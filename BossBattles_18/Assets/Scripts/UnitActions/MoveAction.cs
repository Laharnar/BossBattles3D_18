using System.Collections;
using UnityEngine;

[System.Serializable]
public class MoveAction : ToggleAction{

    public override IEnumerator RunAction() {
        yield return null;
    }

    internal override void RunToggleAction() {
        UnityEngine.Vector3 raw = activeSource.ctr.GetRawDirectionInput();
        activeSource.hraw = raw.x;
        activeSource.vraw = raw.z;

        activeSource.running.value = activeSource.hraw != 0 || activeSource.vraw != 0;
        activeSource.running.RunBoolAnimation(activeSource.anim);
        activeSource.Move();
    }
}



