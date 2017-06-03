using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windController : MonoBehaviour {
    public string tagString = "wind";
    private WindZone windZone;
    // Use this for initialization
    void Start () {
        windZone = GetComponent<WindZone>();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        foreach (GameObject windUser in GameObject.FindGameObjectsWithTag(tagString))
        {
            //TODO(Keegan, "Make more generic")
            var userCloth = windUser.GetComponent<Cloth>();
            //var userTransform = windUser.GetComponent<Transform>();
            if (userCloth != null)
            {
                userCloth.externalAcceleration = transform.forward * windZone.windMain;
            }
            var userSail = windUser.GetComponent<Sail>();
            if (userSail != null)
            {
                userSail.windVector = transform.forward * windZone.windMain;
            }
        }
    }
}
