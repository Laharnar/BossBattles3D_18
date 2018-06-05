using System.Collections;
[System.Serializable]
public class AttackAction:UnitAction {
    public TriggerAnimation attack;

    public override IEnumerator RunAction() {
        activeSource.SetLock(true);
        yield return activeSource.StartCoroutine(attack.RunAnimation(activeSource.anim));
        activeSource.SetLock(false);
    }
}
