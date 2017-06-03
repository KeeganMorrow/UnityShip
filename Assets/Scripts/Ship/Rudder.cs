using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rudder : MonoBehaviour {
    public float forceFactor = 3000f;
    public float rudderSpeed = 15.0f;
    public float maxAngle = 45f;
    public Slider uiSlider;
    // Use this for initialization
    void Start () {
        var hinge = GetComponent<HingeJoint>();
        uiSlider.minValue = -maxAngle;
        uiSlider.maxValue = maxAngle;
        var hingeLimits = hinge.limits;
        hingeLimits.min = -maxAngle;
        hingeLimits.max = maxAngle;
        hinge.limits = hingeLimits;
    }
    
    void FixedUpdate () {
        var hinge = GetComponent<HingeJoint>();
        var rudderAx = Input.GetAxis("Rudder");
        var spring = hinge.spring;
        spring.targetPosition = rudderAx * maxAngle;
        uiSlider.value = spring.targetPosition;
        hinge.spring = spring;
    
        var parentBody = transform.parent.parent.GetComponent<Rigidbody>();
        var parentTransform = transform.parent.parent;
        var worldVel = Vector3.Dot(GetComponent<Rigidbody>().velocity + parentTransform.GetComponent<Rigidbody>().velocity, parentTransform.forward);

        var force = transform.right * worldVel *(Vector3.Dot(parentTransform.right, transform.forward)) * forceFactor;
        parentBody.AddForceAtPosition(force, transform.position);
    }
}
