using System.Collections;
[System.Serializable]
public class AttackAction:UnitAction {
    public TriggerAnimation attack;

    public override IEnumerator RunAction() {
        UnitController uc = activeSource ;
        uc.SetLock(true);
        uc.SetAttacking(true);
        yield return uc.StartCoroutine(attack.RunAnimation(activeSource.anim));
        uc.SetAttacking(false);
        uc.SetLock(false);
    }
}
