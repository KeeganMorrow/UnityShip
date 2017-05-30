using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Tile
{
    public bool accessible;
    public float height;
};

public struct TileCoord
{
    public int x;
    public int z;

    public TileCoord(int tx, int tz)
    {
        x = tx;
        z = tz;
    }
};

public class ShipSpace : MonoBehaviour {
    public float gridSize = 1.0f;
    public Vector3 tileOffset = new Vector3(0f, 0f, 0f);
    public int gridWidth = 1;
    public int gridLength = 1;
    private Tile [,] grid;
	// Use this for initialization
	void Start () {
        grid = new Tile[gridWidth, gridLength];
		for(int x=0; x > gridWidth; x++)
        {
            for(int z=0; z > gridWidth; z++)
            {
                var tile = new Tile();
                tile.accessible = true;
                tile.height = 0.0f;
                grid[x, z] = tile;
            }
        }
	}
	
    public bool isAccessible()
    {
        //var tile = grid[]
        return true;
    }

    public Vector3 GetPosition(TileCoord coord)
    {
        var result = tileOffset;
        result.x += coord.x * gridSize;
        result.z += coord.z * gridSize;
        return result;
    }

    public TileCoord GetTileCoordLocal(Vector3 pos)
    {
        int x = (int)((pos.x - tileOffset.x) / gridSize);
        int z = (int)((pos.z - tileOffset.z) / gridSize);
        return new TileCoord(x, z);
    }

    public TileCoord GetTileCoordWorld(Vector3 pos)
    {
        return GetTileCoordLocal(transform.InverseTransformPoint(pos));
    }
}
