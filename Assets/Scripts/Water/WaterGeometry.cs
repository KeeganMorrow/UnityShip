using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGeometry : MonoBehaviour
{

    private Mesh watermesh;
    private WaterController waterController;
    private float baseheight = 0;

    // Use this for initialization
    void Start()
    {
        watermesh = GetComponent<MeshFilter>().mesh;
        waterController = GetComponent<WaterController>();
    }

    void FixedUpdate()
    {
        Vector3[] vertices = watermesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexPos = watermesh.vertices[i];
            vertexPos.y = baseheight;
            Vector3 vertexPosGlobal = GetComponent<Transform>().TransformPoint(vertexPos);
            vertexPosGlobal.y = waterController.GetWaveYPos(vertexPos, Time.fixedTime);
            vertices[i] = GetComponent<Transform>().InverseTransformPoint(vertexPosGlobal); ;
        }
        watermesh.vertices = vertices;
        watermesh.RecalculateNormals();
    }
}
