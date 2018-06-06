public class AILogic1 : AILogic {

    public AttackAction lightAttack;
    public SpawnAction aoeAttack;
    public MoveAction move;

    public override UnitAction GetAction() {
        if (transform.DistanceToPlayer()< 2f) {
            return lightAttack;
        }
        if (aoeAttack.attack.IsReady()) {
            return aoeAttack;
        }
        return move;
    }
}
