using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour {
    public Vector3 windVector = Vector3.zero;
    public Vector3 windPoint = new Vector3(0, 1, 0);
    public float windScale = 0.5f;

    private Rigidbody rigidbody;

	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        //transform.rotation = Quaternion.LookRotation(windVector);
        rigidbody.AddForceAtPosition(windVector * windScale, transform.TransformPoint(windPoint));
        Debug.DrawRay(transform.TransformPoint(windPoint), windVector*0.1f, Color.green);
	}
}
