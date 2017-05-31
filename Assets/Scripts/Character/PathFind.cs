using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour {

    private TileCoord tileDest;
    private ShipObject shipObj;
    private ShipSpace shipSpace;
    private TileCoord destination;
	// Use this for initialization
	void Start () {
        shipObj = GetComponent<ShipObject>();
	}
	
	// Update is called once per frame
	void Update () {
        int x = shipObj.shipX, z = shipObj.shipZ;
        if (shipObj.shipZ < destination.z)
        {
            z = shipObj.shipZ + 1;
        }
        else if (shipObj.shipZ > destination.z)
        {
            z = shipObj.shipZ - 1;
        }

        if (shipObj.shipX < destination.x)
        {
            x = shipObj.shipX + 1;
        }
        else if (shipObj.shipX > destination.x)
        {
            x = shipObj.shipX - 1;
        }
        shipObj.shipX = x;
        shipObj.shipZ = z;
	}

    public void MovePosition(TileCoord dest)
    {
        destination = dest;
    }

    public void MovePosition(Vector3 position)
    {
        var destination = shipSpace.GetTileCoordLocal(position);
        MovePosition(destination);
    }

}
