using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

    public GameObject followObj;

    public bool isMoving;

    //Wave height and speed
    public float scale = 0.1f;
    public float speed = 1.0f;
    //The width between the waves
    public float waveDistance = 1f;
    //Noise parameters
    public float noiseStrength = 1f;
    public float noiseWalk = 1f;

    public float offsetSpeed = .05f;

    private Transform transform;
    private Material material;

    // Use this for initialization
    void Start() {
        transform = GetComponent<Transform>();
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        var followPos = followObj.GetComponent<Transform>().position;
        var transform = GetComponent<Transform>();
        transform.position = new Vector3(followPos.x, transform.position.y, followPos.z);
        var texturePos = new Vector2(followPos.x, followPos.z) * offsetSpeed;
        material.SetTextureOffset("_MainTex", texturePos);
        material.SetFloat("_geoSpeed", speed);
        material.SetFloat("_geoDistance", waveDistance);
        material.SetFloat("_GeoScale", scale);
        material.SetFloat("_geoWaveTime", Time.time);
    }
    public float SineXWave(Vector3 position, float currTime) {
        float x = position.x;
        float y = 0f;
        float z = position.z;

        float waveType = z;

        y += Mathf.Sin((currTime * speed + waveType) / waveDistance) * scale;

        //Add noise to make it more realistic
        //y += Mathf.PerlinNoise(x + noiseWalk, y + Mathf.Sin(currTime * 0.1f)) * noiseStrength;

        return y;
    }

    // Get the World Y of the waves given a world position
    public float GetWaveYPos(Vector3 position, float currTime)
    {
        // TODO(Keegan, add waves here)
        float y = position.y;
        if (isMoving)
        {
            y += SineXWave(position, currTime);
        }
        return y;
    }
    
    // TODO(Keegan, rename this function to make sense)
    public float DistanceToWater(Vector3 position, float currTime)
    {
        float waterHeight = GetWaveYPos(new Vector3(position.x, transform.position.y, position.z), currTime);
        return waterHeight;
    }
}
