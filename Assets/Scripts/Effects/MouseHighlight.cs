using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlight : MonoBehaviour {
    public Material highlightMaterial;
    private int materialIndex;

    private void Start()
    {
        var materials = GetComponent<Renderer>().materials;
        materialIndex = materials.GetLength(0);
        var newMaterials = new Material[materialIndex + 1];
        for (var i = 0; i < materialIndex; i++){
            newMaterials[i] = materials[i];
        }
        GetComponent<Renderer>().materials = newMaterials;
    }

    private void OnMouseEnter()
    {
        var materials = GetComponent<Renderer>().materials;
        materials[materialIndex] = highlightMaterial;
        GetComponent<Renderer>().materials = materials;
    }

    private void OnMouseExit()
    {
        var materials = GetComponent<Renderer>().materials;
        materials[materialIndex] = null;
        GetComponent<Renderer>().materials = materials;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
