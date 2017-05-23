using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour {

    public float movespeed;
    public float rotatespeed;
    public float jumpspeed;
    private Transform transform;
    private Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(transform.forward * movespeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(transform.forward * -movespeed);
        }
        else
        {
            rigidbody.AddForce(-0.1f * rigidbody.velocity);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddTorque(new Vector3(0, 1, 0) * -rotatespeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddTorque(new Vector3(0, 1, 0) * rotatespeed);
        }
        else
        {
            rigidbody.AddTorque(-0.1f * rigidbody.angularVelocity);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * jumpspeed);
        }
    }
}
