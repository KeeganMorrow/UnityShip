using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyant : MonoBehaviour {

    public GameObject water;
    public float airDrag = 1.0f;
    public float waterDrag = 5.0f;
    public float floaterOffsetY = 0f;
    public float floaterOffsetBackwardZ = 1.0f;
    public float floaterOffsetForwardZ = 1.0f;
    public float floaterOffsetX = 1.0f;
    public float buoyancyFactor = 1000f;
    public Vector3 centerOfMass = new Vector3(0, 0, 0);
    private WaterController waterController;
    private Transform transform;
    private Rigidbody rigidbody;
    private GameObject[] surfacePoints;

    // Use this for initialization

    private GameObject addSurfacePoint(string name, Vector3 position)
    {
        var obj = new GameObject(name);
        obj.SetActive(false);
        var trans = obj.GetComponent<Transform>();
        trans.SetParent(GetComponent<Transform>().parent);
        trans.position = transform.TransformPoint(position);
        var rig = obj.AddComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeAll;
        //rig.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        var spring = obj.AddComponent<SpringJoint>();
        spring.connectedBody = rigidbody;
        spring.spring = 5000f;
        obj.SetActive(true);
        return obj;
    }

    private void Awake()
    {
        waterController = water.GetComponent<WaterController>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        surfacePoints = new GameObject[4];
    }

    private void Start () {
        surfacePoints[0] = addSurfacePoint("floaterBowStar", new Vector3(floaterOffsetX, floaterOffsetY, floaterOffsetBackwardZ));
        surfacePoints[1] = addSurfacePoint("floaterSternStar", new Vector3(floaterOffsetX, floaterOffsetY, -floaterOffsetBackwardZ));
        surfacePoints[2] = addSurfacePoint("floaterBowLar", new Vector3(-floaterOffsetX, floaterOffsetY, floaterOffsetForwardZ));
        surfacePoints[3] = addSurfacePoint("floaterSternLar", new Vector3(-floaterOffsetX, floaterOffsetY, -floaterOffsetForwardZ));
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
    private void FixedUpdate()
    {
        rigidbody.centerOfMass = centerOfMass;
        for ( int i = 0; i < surfacePoints.Length; i++)
        {
            var obj = surfacePoints[i];
            Vector3 worldPoint = obj.GetComponent<Transform>().position;
            var waterY = waterController.DistanceToWater(worldPoint, Time.fixedTime);
            worldPoint.y = waterY;
            obj.GetComponent<Transform>().position = worldPoint;
            //Vector3 pointForce = getBuoyancyForce(worldPoint);
            //rigidbody.AddForceAtPosition(pointForce, worldPoint);
            //Debug.DrawRay(worldPoint, pointForce*.01f, Color.green);
        }
    }
}
