using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    public GameObject target;
    public Vector3 targetPos;
    public Vector3 pos;
    public float length;
    private LineRenderer lineRenderer;
    private SpringJoint springJoint;
    private HingeJoint hingeJoint;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        hingeJoint = GetComponent<HingeJoint>();
        springJoint = GetComponent<SpringJoint>();
        springJoint.connectedBody = target.GetComponent<Rigidbody>();
        springJoint.connectedAnchor = targetPos;
        springJoint.anchor = pos;
    }
    
    // Update is called once per frame
    void Update () {
        // TODO: Use rope length to move the boom
        var axis = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(axis) > 0.01)
        {
            var spring = hingeJoint.spring;
            spring.targetPosition += axis * 200;
            hingeJoint.spring = spring;
            //length = length + axis * 5;
        }

        //springJoint.maxDistance = length;
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.TransformPoint(pos);
        positions[1] = target.GetComponent<Transform>().TransformPoint(targetPos);
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
}
