using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyant : MonoBehaviour {

    public GameObject water;
    public float airDrag = 1.0f;
    public float waterDrag = 5.0f;
    public float floaterOffsetY = 0f;
    public float floaterOffsetBackwardX = 1.0f;
    public float floaterOffsetForwardX = 1.0f;
    public float floaterOffsetZ = 1.0f;
    public float buoyancyFactor = 1000f;
    public Vector3 centerOfMass = new Vector3(0, 0, 0);
    private WaterController waterController;
    private Transform transform;
    private Rigidbody rigidbody;
    private Vector3[] testpoints;

    // Use this for initialization
    private void Start () {
        waterController = water.GetComponent<WaterController>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();

        testpoints = new Vector3[4];
    }

    private Vector3 getBuoyancyForce(Vector3 worldPoint)
    {
        float distance = waterController.DistanceToWater(worldPoint, Time.fixedTime);
        Vector3 buoyantForce = new Vector3(0, 0, 0);
        if (distance < 0)
        {
            buoyantForce.y = distance * -buoyancyFactor;
        }
        return buoyantForce;
    }

    // Update is called once per frame
    private void Update()
    {
        rigidbody.centerOfMass = centerOfMass;

        testpoints[0] = new Vector3(floaterOffsetBackwardX, floaterOffsetY, floaterOffsetZ);
        testpoints[1] = new Vector3(floaterOffsetBackwardX, floaterOffsetY, -floaterOffsetZ);
        testpoints[2] = new Vector3(-floaterOffsetForwardX, floaterOffsetY, floaterOffsetZ);
        testpoints[3] = new Vector3(-floaterOffsetForwardX, floaterOffsetY, -floaterOffsetZ);
        for ( int i = 0; i < testpoints.Length; i++)
        {
            Vector3 worldPoint = transform.TransformPoint(testpoints[i]);
            Vector3 pointForce = getBuoyancyForce(worldPoint);
            rigidbody.AddForceAtPosition(pointForce, worldPoint);
            Debug.DrawRay(worldPoint, pointForce*.01f, Color.green);
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < testpoints.Length; i++)
        {
            Vector3 testpoint = transform.TransformPoint(testpoints[i]);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(testpoint, .25f);
        }
    }

}
