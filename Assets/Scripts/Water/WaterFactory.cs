using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaterFactory : MonoBehaviour {

    public float tileSize = 100f;
    public float lodOffsetY = 0.0f;
    public int tileRes = 10;
    public int tileLodRes = 5;
    public GameObject followObj;
    PlaneGenerator generator;
    WaterController waterController;
    private GameObject[] lods = new GameObject[8];

    GameObject makeLodBase()
    {
        var lodWater = new GameObject("lodWater");
        lodWater.SetActive(false);

        var transform = lodWater.GetComponent<Transform>();
        transform.parent = GetComponent<Transform>();
        transform.position = new Vector3(0f, lodOffsetY, 0f);

        var meshFilter = lodWater.AddComponent<MeshFilter>();
        Assert.IsNotNull(generator);
        meshFilter.mesh = generator.CreatePlaneMesh(tileLodRes, tileLodRes, tileSize, tileSize, false, true, false);

        var meshRenderer = lodWater.AddComponent<MeshRenderer>();

        meshRenderer.material = GetComponent<MeshRenderer>().material;
        
        var waterGeometry = lodWater.AddComponent<WaterGeometry>();
        Assert.IsNotNull(waterController);
        waterGeometry.waterController = waterController;
        return lodWater;
    }

    GameObject buildLod(GameObject baseLod, string name, Vector3 position)
    {
        var lod = Instantiate(baseLod, position, new Quaternion(0, 0, 0, 0), transform);
        lod.name = name;
        lod.SetActive(true);
        return lod;
    }

    void buildLods()
    {
        var baseLod = makeLodBase();
        var transform = GetComponent<Transform>();

        lods[0] = buildLod(baseLod, "lodNorth", new Vector3(0, lodOffsetY, tileSize));
        
        lods[1] = buildLod(baseLod, "lodNorthWest", new Vector3(-tileSize, lodOffsetY, tileSize));

        lods[2] = buildLod(baseLod, "lodWest", new Vector3(-tileSize, lodOffsetY, 0));

        lods[3] = buildLod(baseLod, "lodSouthWest", new Vector3(-tileSize, lodOffsetY, -tileSize));

        lods[4] = buildLod(baseLod, "lodSouth", new Vector3(0, lodOffsetY, -tileSize));

        lods[5] = buildLod(baseLod, "lodSouthEast", new Vector3(tileSize, lodOffsetY, -tileSize));

        lods[6] = buildLod(baseLod, "lodNorthEast", new Vector3(tileSize, lodOffsetY, tileSize));

        lods[7] = buildLod(baseLod, "lodEast", new Vector3(tileSize, lodOffsetY, 0));

        Destroy(baseLod);

    }

    void Start()
    {
        generator = new PlaneGenerator();
        GetComponent<MeshFilter>().mesh = generator.CreatePlaneMesh(tileRes, tileRes, tileSize, tileSize, false, true, false);
        waterController = GetComponent<WaterController>();
        buildLods();
    }

    private void Update()
    {
        var followPos = followObj.GetComponent<Transform>().position;
        var transform = GetComponent<Transform>();
        transform.position = new Vector3(followPos.x, transform.position.y, followPos.z);
    }
}
