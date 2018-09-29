﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W)) {
            rigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A)) {
            if (!Input.GetKey(KeyCode.D)) {
                transform.Rotate(Vector3.forward);
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            if (!Input.GetKey(KeyCode.A)) {
                transform.Rotate(Vector3.back);
            }
        }
    }
}