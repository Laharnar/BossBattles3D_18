using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float speed = 5f;
    public float rotationSpeedDegrees = 5f;

    public Rigidbody rig;

    public CameraControls cam;

    float lastHraw;

    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float hraw = Input.GetAxisRaw("Horizontal");
        float vraw = Input.GetAxisRaw("Vertical");
        float rot = Input.GetAxis("Mouse X");


        Vector3 dir = new Vector3(h, 0, v);
        Vector3 dirRaw = new Vector3(hraw, 0, vraw);
        Vector3 camf = Camera.main.transform.forward;
        Vector3 dirFromCam = Camera.main.transform.TransformDirection(dirRaw.normalized);
        
        if (h != 0 || v != 0) {
            Vector3 p = Vector3.RotateTowards(transform.forward, dirFromCam, 2*Mathf.PI/360* rotationSpeedDegrees*Time.deltaTime*10, 0);
            transform.forward = p;
        }

        // insta stop: (vraw*vraw+hraw*hraw)/2
        Move(Vector3.forward * (v*v+h*h)/2);

        cam.Move();
        cam.Rotate(rot);
        lastHraw = hraw;
    }

    private void Move(Vector3 dir) {
        dir = dir.normalized;
        rig.MovePosition(transform.position+transform.TransformDirection(dir) * speed * Time.deltaTime);
    }
}
