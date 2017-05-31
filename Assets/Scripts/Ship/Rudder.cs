using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudder : MonoBehaviour {
    public float forceFactor = 3000f;
    public float rudderSpeed = 15.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate () {
        var rudderAx = Input.GetAxis("Rudder");
		if (rudderAx != 0f)
        {
            var motor = GetComponent<HingeJoint>().motor;
            motor.targetVelocity = rudderSpeed * rudderAx;
            GetComponent<HingeJoint>().motor = motor;
        }
        var parentBody = transform.parent.parent.GetComponent<Rigidbody>();
        var parentTransform = transform.parent.parent;
        var worldVel = Vector3.Dot(GetComponent<Rigidbody>().velocity + parentTransform.GetComponent<Rigidbody>().velocity, parentTransform.forward);
        var force = transform.right * worldVel *(Vector3.Dot(parentTransform.right, transform.forward)) * forceFactor;
        parentBody.AddForceAtPosition(force, transform.position);
	}
}
