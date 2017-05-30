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

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        springJoint = GetComponent<SpringJoint>();
        springJoint.connectedBody = target.GetComponent<Rigidbody>();
        springJoint.connectedAnchor = targetPos;
        springJoint.anchor = pos;
	}
	
	// Update is called once per frame
	void Update () {
        springJoint.maxDistance = length;
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.TransformPoint(pos);
        positions[1] = target.GetComponent<Transform>().TransformPoint(targetPos);
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
        var axis = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(axis) > 0.01)
        {
            length = length + axis * 5;
        }
	}
}
