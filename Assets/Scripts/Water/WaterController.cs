using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wave {
    public float waveLength { get; private set; }
    public float speed { get; private set; }
    public float amplitude { get; private set; }
    public float sharpness { get; private set; }
    public float frequency { get; private set; }
    public float phase { get; private set; }
    public Vector2 direction { get; private set; }

    public Wave(float waveLength, float speed, float amplitude, float sharpness, Vector2 direction)
    {
        this.waveLength = waveLength;
        this.speed = speed;
        this.amplitude = amplitude;
        this.sharpness = sharpness;
        this.direction = direction.normalized;
        frequency = (2 * Mathf.PI) / waveLength;
        phase = frequency * speed;
    }
};

public class WaterController : MonoBehaviour {

    public GameObject followObj;

    public bool isMoving;

    const int WAVE_COUNT = 5;

    public float offsetSpeed = .05f;

    private Transform transform;
    private Material material;

    private Wave[] waves;

    // Use this for initialization
    void Start() {
        transform = GetComponent<Transform>();
        material = GetComponent<MeshRenderer>().material;
        waves = new Wave[WAVE_COUNT]
        {
            new Wave(400,5.0f, 4.2f, 0.9f, new Vector2(1.0f, 0.2f)),
            new Wave(240,6.0f, 2.4f, 0.5f, new Vector2(1.0f, 3.0f)),
            new Wave(40,10.0f, 1.2f, 0.8f, new Vector2(2.0f, 4.0f)),
            new Wave(60,6.0f, 1.2f, 0.4f, new Vector2(-1.0f, 0.0f)),
            new Wave(30,9.0f, 0.15f, 0.9f, new Vector2(-1.0f, 1.2f))
        };
        //TODO: Periodically update the waves vector in the shader as well!
        var vectorWaves = new Vector4[WAVE_COUNT];
        var vectorWaveDir = new Vector4[WAVE_COUNT];
        for (int i = 0; i < waves.Length; i++)
        {
            vectorWaves[i] = new Vector4(waves[i].frequency, waves[i].amplitude, waves[i].phase, waves[i].sharpness);
            vectorWaveDir[i] = new Vector4(waves[i].direction.x, waves[i].direction.y);
        }
        material.SetVectorArray("waves", vectorWaves);
        material.SetVectorArray("wavedirs", vectorWaveDir);
    }

    // Update is called once per frame
    private void Update()
    {
        var followPos = followObj.GetComponent<Transform>().position;
        var transform = GetComponent<Transform>();
        transform.position = new Vector3(followPos.x, transform.position.y, followPos.z);
        var texturePos = new Vector2(followPos.x, followPos.z) * offsetSpeed;
        material.SetTextureOffset("_MainTex", texturePos);
    }
    
    // Calculate the Y offset of a particular wave
    // Note that this needs to match the logic used in the ocean shader
    public float CalculateWave(Vector3 position, float time, Wave wave) {
        var y = wave.amplitude * Mathf.Sin(Vector2.Dot(wave.direction, new Vector2(position.x, position.z)) * wave.frequency + Time.time * wave.phase);
        return y;
    }

    // Get the World Y of the waves given a world position
    public float GetWaveYPos(Vector3 position, float currTime)
    {
        float y = position.y;
        if (isMoving)
        {
            for (int i = 0; i < waves.Length; i++)
            {
                y += CalculateWave(position, currTime, waves[i]);
            }
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
