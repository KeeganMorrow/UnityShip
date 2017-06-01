using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour {
    public Vector3 windVector = Vector3.zero;
    public Vector3 windPoint = new Vector3(0, 1, 0);
    public Vector3 dirRotation = new Vector3(1, 0, 0);
    public float windScale = 0.5f;
    public float drag = 1.0f;
    public float lift = 1.0f;
    public float liftLossAngle = 30f;

    private AnimationCurve liftCurve = new AnimationCurve();
    private AnimationCurve dragCurve = new AnimationCurve();

    private Rigidbody rigidbody;

	void Start () {
        rigidbody = transform.parent.parent.GetComponent<Rigidbody>();
        liftCurve = new AnimationCurve();
        dragCurve = new AnimationCurve();
        liftCurve.AddKey(new Keyframe(0f, 0f, 0f, 0f));
        liftCurve.AddKey(new Keyframe(liftLossAngle, 1.0f, 0f, 0f));
        liftCurve.AddKey(new Keyframe(180f, 0f, 0f, 0f));
        liftCurve.postWrapMode = WrapMode.Loop;
        liftCurve.preWrapMode = WrapMode.Loop;
        dragCurve.AddKey(new Keyframe(0f, 0f, 0f, 0f));
        dragCurve.AddKey(new Keyframe(90f, 1f, 0f, 0f));
        dragCurve.postWrapMode = WrapMode.Loop;
        dragCurve.preWrapMode = WrapMode.Loop;
	}
	
	void FixedUpdate () {
        //transform.rotation = Quaternion.LookRotation(windVector);
        //TODO: Use relative velocity to wind
        var sailDir = transform.TransformDirection(Quaternion.Euler(dirRotation) * transform.forward);
        // TODO: Decide which makes more sense
        var effectWind = windVector + rigidbody.velocity;

        var rotation = Quaternion.Euler(0, 90, 0);
        var windAngle = Vector3.Angle(windVector, sailDir);
        var liftCoefficient = lift * liftCurve.Evaluate(windAngle);
        var sailRight = Quaternion.Euler(0, 0, 90) * sailDir;
        var cross = Vector3.Cross(windVector, sailDir);
        if (cross.y < 0)
        {
            liftCoefficient *= -1f;
        }
        //liftCoefficient *= negate;

        var liftForce = liftCoefficient * (rotation * effectWind);

        var dragCoefficient = drag * dragCurve.Evaluate(windAngle);
        var dragForce =  dragCoefficient * effectWind;
        //rigidbody.AddForceAtPosition(dragForce, transform.TransformPoint(windPoint));
        //rigidbody.AddForceAtPosition(liftForce, transform.TransformPoint(windPoint));
        // Temporary - only take the component that moves the ship forward (simulates keel)
        rigidbody.AddForce(transform.parent.forward.normalized * Vector3.Dot(dragForce, transform.parent.forward.normalized));
        rigidbody.AddForce(transform.parent.forward.normalized * Vector3.Dot(liftForce, transform.parent.forward.normalized));
        // Invert vector for visualization purposes
        Debug.DrawRay(transform.TransformPoint(windPoint), -effectWind, Color.green);
        Debug.DrawRay(transform.TransformPoint(windPoint), dragForce/drag, Color.red);
        Debug.DrawRay(transform.TransformPoint(windPoint), liftForce/lift, Color.blue);
        Debug.DrawRay(transform.TransformPoint(windPoint), sailDir * 10, Color.yellow);
	}
}
