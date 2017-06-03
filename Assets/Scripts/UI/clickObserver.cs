using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickObserver : MonoBehaviour {

    public float maxDistance = Mathf.Infinity;
    public GameObject moveObject;
    public GameObject controlObject;

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
            var tilecoord = hit.rigidbody.GetComponent<ShipSpace>().GetTileCoordWorld(hit.point);
            moveObject.transform.parent = hit.rigidbody.GetComponent<Transform>();
            var shipObject = moveObject.GetComponent<ShipObject>();
            shipObject.shipX = tilecoord.x;
            shipObject.shipZ = tilecoord.z;
            moveObject.SetActive(true);
            if (Input.GetMouseButton(0))
            {
                controlObject.GetComponent<PathFind>().MovePosition(tilecoord);
            }
        }
        else
        {
            moveObject.SetActive(false);
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
        }
    }
}
