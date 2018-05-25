using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float speed = 5f;

    public Rigidbody rig;

    public CameraControls cam;

    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float rot = Input.GetAxis("Mouse X");

        Vector3 dir = new Vector3(h, 0, v);
        Move(dir);

        cam.Move();
        cam.Rotate(rot);
	}

    private void Move(Vector3 dir) {
        rig.MovePosition(transform.position+transform.TransformDirection(dir) * speed * Time.deltaTime);
    }
}
