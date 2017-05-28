using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour {
    public float spring;
    public float damper;
    public float minDistance;
    public float maxDistance;
    private GameObject source;
    private Vector3 sourceLocalPos;
	// Use this for initialization
	void Start () {
        source = GetComponent<ProjectileData>().source;
        sourceLocalPos = GetComponent<ProjectileData>().sourceLocalPos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.gameObject;
        if (contact != source)
        {
            var hitbody = contact.GetComponent<Rigidbody>();
            var hittransform = contact.GetComponent<Transform>();
            var sourcebody = source.GetComponent<Rigidbody>();
            var joint = source.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.enableCollision = true;
            joint.anchor = sourceLocalPos;
            joint.connectedAnchor = hittransform.InverseTransformPoint(collision.contacts[0].point);
            joint.connectedBody = hitbody;
            joint.spring = spring;
            joint.damper = damper;
            joint.maxDistance = maxDistance;
            joint.minDistance = minDistance;
        }
    }
}
