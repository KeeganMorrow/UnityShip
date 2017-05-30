using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navtest : MonoBehaviour {

    public Vector3 goalPosition;
    private UnityEngine.AI.NavMeshAgent agent;
    
	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (! agent.hasPath)
        {
            agent.destination = goalPosition;
        }
	}
}
