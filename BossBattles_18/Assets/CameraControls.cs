using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
    public Transform tTarget;

    public bool move = true;
    public Vector3 offset;
    public Transform target;

    public float rotSpeed = 5f;
    public Transform rotationTarget;
    Vector3 rotOffset;
	// Use this for initialization
	void Start () {
        offset = Camera.main.transform.position - tTarget.position;
    }
	
	// Update is called once per frame
	public void Move () {
        if (move) {
            Vector3 targetPos = target.position + offset+rotOffset;
            Camera.main.transform.position = targetPos;
        }
    }

    internal void Rotate(float rot) {
        Vector3 pos = Camera.main.transform.position;
        Camera.main.transform.RotateAround(rotationTarget.position, Vector3.up, rot*rotSpeed*Time.deltaTime);
        rotOffset += Camera.main.transform.position - pos;
    }
}
