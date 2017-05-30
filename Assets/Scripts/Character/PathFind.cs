using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour {

    private TileCoord tileDest;
    private ShipObject shipObj;
    private ShipSpace shipSpace;
	// Use this for initialization
	void Start () {
        shipObj = GetComponent<ShipObject>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MovePosition(TileCoord destination)
    {
        shipObj.shipX = destination.x;
        shipObj.shipZ = destination.z;
    }

    public void MovePosition(Vector3 position)
    {
        var destination = shipSpace.GetTileCoordLocal(position);
        MovePosition(destination);
    }

}
