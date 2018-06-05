using System.Collections;
using UnityEngine;

[System.Serializable]
public class SpawnAction : UnitAction {
    public CooldownAbility attack;

    public override IEnumerator RunAction() {
        activeSource.SetLock(true);
        GameObject.Instantiate(attack.pref, activeSource.tTarget.transform.position, new Quaternion());
        yield return new WaitForSeconds(attack.rate);
        activeSource.SetLock(false);
    }
}
