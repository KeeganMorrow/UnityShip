using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharNavigate : MonoBehaviour {
    public GameObject shipGrid;
    public int shipX;
    public int shipZ;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var grid = shipGrid.GetComponent<shipSpace>();
        transform.localPosition = grid.GetPosition(new TileCoord(shipX, shipZ));
	}
}
