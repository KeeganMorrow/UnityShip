using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGeometry : MonoBehaviour
{
    private Mesh watermesh;
    public WaterController waterController;
    private Transform transform;

    // Use this for initialization
    void Start()
    {
        watermesh = GetComponent<MeshFilter>().mesh;
        transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Vector3[] vertices = watermesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexPos = watermesh.vertices[i];
            vertexPos.y = 0;
            Vector3 vertexPosGlobal = transform.TransformPoint(vertexPos);
            vertexPosGlobal.y = waterController.GetWaveYPos(vertexPosGlobal, Time.fixedTime);
            vertices[i] = transform.InverseTransformPoint(vertexPosGlobal); ;
        }
        watermesh.vertices = vertices;
        watermesh.RecalculateNormals();
    }
}
