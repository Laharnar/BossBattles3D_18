using UnityEngine;
public static class UnitPositionHelper {
    public static float AngleToPlayer(this Transform enemy) {
        return Vector3.Angle(enemy.forward, PlayerControls.s.position);
    }

    public static float DistanceToPlayer(this Transform enemy) {
        return Vector3.Distance(enemy.position, PlayerControls.s.position);
    }
}