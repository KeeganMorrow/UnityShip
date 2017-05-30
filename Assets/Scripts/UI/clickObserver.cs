using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickObserver : MonoBehaviour {

    public float maxDistance = Mathf.Infinity;
    public GameObject moveObject;
    public GameObject shipObject;

	// Use this for initialization
	void Start () {
		
	}
	
    //replace Update method in your class with this one
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Only use user layer 4 (ship hull)
        var layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            Debug.Log(hit.point);
            var hitTransform = hit.rigidbody.GetComponent<Transform>();
            var tilecoord = shipObject.GetComponent<shipSpace>().GetTileCoordWorld(hit.point);
            moveObject.GetComponent<CharNavigate>().shipX = tilecoord.x;
            moveObject.GetComponent<CharNavigate>().shipZ = tilecoord.z;
        }
        else
        {

            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
        }
    }
}
