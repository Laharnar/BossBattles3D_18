using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControls : UnitControls {

    public KeyCode lightAttackCode;
    public KeyCode strongAttackCode;
    public KeyCode blockCode;

    public CameraControls cam;

    public DashAction dash;
    public AttackAction lightAttack;
    public AttackAction strongAttack;
    public ToggleAction blockAction; //= new BoolAnimation("Blocking", 1.0f, "Blocking");

    public override Vector3 GetDirSmooth() {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // TODO: export as player only input
    public override  bool[] GetInputs() {
        return new bool[] { Input.GetKeyDown(lightAttackCode), Input.GetKeyDown(strongAttackCode), Input.GetKey(this.blockCode), Input.GetKeyDown(KeyCode.Space), true };
    }

    public override Vector3 GetRawDirectionInput() {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    // TODO: export as player specific
    public override void PostMoveUpdate() {
        float camRot = Input.GetAxis("Mouse X");
        cam.Move();
        cam.Rotate(camRot);
    }

    public override UnitAction[] GetActions() {
        return new UnitAction[] { lightAttack, strongAttack, blockAction, dash };
    }
}
