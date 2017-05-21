using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {
    private Transform transform;

    public bool isMoving;

    //Wave height and speed
    public float scale = 0.1f;
    public float speed = 1.0f;
    //The width between the waves
    public float waveDistance = 1f;
    //Noise parameters
    public float noiseStrength = 1f;
    public float noiseWalk = 1f;

    // Use this for initialization
    void Start() {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {

    }
    public float SineXWave(Vector3 position, float currTime) {
        float x = position.x;
        float y = 0f;
        float z = position.z;

        float waveType = z;

        y += Mathf.Sin((currTime * speed + waveType) / waveDistance) * scale;

        //Add noise to make it more realistic
        y += Mathf.PerlinNoise(x + noiseWalk, y + Mathf.Sin(currTime * 0.1f)) * noiseStrength;

        return y;
    }

    // Get the World Y of the waves given a world position
    public float GetWaveYPos(Vector3 position, float currTime)
    {
        // TODO(Keegan, add waves here)
        float y = transform.position.y;
        if (isMoving)
        {
            y += SineXWave(position, currTime);
        }
        return y;
    }

    public float DistanceToWater(Vector3 position, float currTime)
    {
        float waterHeight = GetWaveYPos(position, currTime);

        float distanceToWater = position.y - waterHeight;

        return distanceToWater;
    }
}
