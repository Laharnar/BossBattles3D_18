using UnityEngine;

public partial class EnemyControls : UnitControls {
    public Transform tTarget;
    PlayerControls player;
    public override Vector3 GetDirSmooth() {
        PlayerControls player = GetPlayer();
        return (player.transform.position - tTarget.position).normalized;
    }

    private PlayerControls GetPlayer() {
        if (!player) {
            player = GameObject.FindObjectOfType<PlayerControls>();
        }
        return player;
    }

    public override bool[] GetInputs() {
        PlayerControls player = GetPlayer();
        bool lightAttack = Vector3.Distance(player.transform.position, tTarget.position)<2f;
        return new bool[4] { lightAttack, false, false, false };
    }

    public override Vector3 GetRawDirectionInput() {
        PlayerControls player = GetPlayer();
        return (player.transform.position - tTarget.position).normalized;
    }

    public override void PostMoveUpdate() {
        //nothing
    }
}
