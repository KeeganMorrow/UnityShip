using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyant : MonoBehaviour {

    public GameObject water;
    public float airDrag = 1.0f;
    public float waterDrag = 5.0f;
    private WaterController waterController;
    private Transform transform;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        waterController = water.GetComponent<WaterController>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = waterController.DistanceToWater(transform.position, Time.fixedTime);
        if (distance < 0)
        {
            rigidbody.drag = waterDrag;
            Vector3 buoyantForce = new Vector3(0f, distance * -1000f, 0f);
            rigidbody.AddForce(buoyantForce);
        }
        else
        {
            rigidbody.drag = airDrag;
        }
        Debug.Log("Distance to water is " + distance, this);
	}
}
