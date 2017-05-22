using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGeometry : MonoBehaviour
{
    public int widthSegments = 10;
    public int lengthSegments = 10;
    public float width = 10f;
    public float length = 10f;
    private Mesh watermesh;
    private WaterController waterController;
    private float baseheight = 0;
    private Transform transform;

    // Use this for initialization
    void Start()
    {
        PlaneGenerator generator = new PlaneGenerator();
        GetComponent<MeshFilter>().mesh = generator.CreatePlaneMesh(widthSegments, lengthSegments, width, length, false, true, false);
        watermesh = GetComponent<MeshFilter>().mesh;
        waterController = GetComponent<WaterController>();
        transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Vector3[] vertices = watermesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexPos = watermesh.vertices[i];
            vertexPos.y = baseheight;
            Vector3 vertexPosGlobal = transform.TransformPoint(vertexPos);
            vertexPosGlobal.y = waterController.GetWaveYPos(vertexPosGlobal, Time.fixedTime);
            vertices[i] = transform.InverseTransformPoint(vertexPosGlobal); ;
        }
        watermesh.vertices = vertices;
        watermesh.RecalculateNormals();
    }
}
