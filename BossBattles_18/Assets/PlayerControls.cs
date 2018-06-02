using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControls:MonoBehaviour, IUnitControls {

    public KeyCode lightAttackCode;
    public KeyCode strongAttackCode;
    public KeyCode blockCode;

    public CameraControls cam;

    public Vector3 GetDirSmooth() {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // TODO: export as player only input
    public bool[] GetInputs() {
        return new bool[4] { Input.GetKeyDown(lightAttackCode), Input.GetKeyDown(strongAttackCode), Input.GetKey(this.blockCode), Input.GetKeyDown(KeyCode.Space) };
    }

    public Vector3 GetRawDirectionInput() {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    // TODO: export as player specific
    public void PostMoveUpdate() {
        float camRot = Input.GetAxis("Mouse X");
        cam.Move();
        cam.Rotate(camRot);
    }

}
