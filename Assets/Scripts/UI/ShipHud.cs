using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHud : MonoBehaviour {

    private Rigidbody rigidbody;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnGUI()
    {
        var velstring = string.Format("Speed {0}", rigidbody.velocity.magnitude.ToString("F2"));
        GUI.Box(new Rect(0, 0, 100, 20), velstring);
    }

    
}
