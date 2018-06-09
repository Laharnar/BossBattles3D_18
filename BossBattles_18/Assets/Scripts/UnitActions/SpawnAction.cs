using System.Collections;
using UnityEngine;

[System.Serializable]
public class SpawnAction : UnitAction {
    public float wait;
    public CooldownAbility attack;

    public override IEnumerator RunAction() {
        UnitController source = activeSource;
        GameObject.Instantiate(attack.pref, activeSource.tTarget.transform.position, new Quaternion());
        if (wait > 0)
        yield return new WaitForSeconds(wait);
    }
}
