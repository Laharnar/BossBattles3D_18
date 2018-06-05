using System.Collections;
using UnityEngine;

[System.Serializable]
public class SpawnAction : UnitAction {
    public CooldownAbility attack;

    public override IEnumerator RunAction() {
        UnitController source = activeSource;
        GameObject.Instantiate(attack.pref, activeSource.tTarget.transform.position, new Quaternion());
        yield return new WaitForSeconds(attack.rate);
    }
}
