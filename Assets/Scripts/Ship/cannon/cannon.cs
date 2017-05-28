using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {
    public GameObject toFire;
    public float velocityFire = 100f;
    public Vector3 positionFire = new Vector3(0, 0, 0);
    private int trajVertex = 200;
	// Use this for initialization
	void Start () {
	}

    // Ignoring drag for now (and probably forever)
    void drawTrajectory()
    {
        var lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(trajVertex);
        for(var i = 0; i < trajVertex; i++)
        {
            float x = -1 * (positionFire.x + (velocityFire * Time.fixedDeltaTime) * i);
            float y = 0f;
            float z =  -1f*(transform.position.y - (Physics.gravity.y * Time.fixedDeltaTime * i * i / 2f));
            lineRenderer.SetPosition(i, transform.TransformPoint(new Vector3(x, y, z)));
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Space)){
            drawTrajectory();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            var projectile = Instantiate(toFire, transform.TransformPoint(positionFire), transform.rotation);
            projectile.GetComponent<Rigidbody>().velocity = transform.TransformVector(new Vector3(-velocityFire, 0, 0));
            var projectiledata = projectile.GetComponent<ProjectileData>();
            projectiledata.source = transform.parent.parent.gameObject;
            projectiledata.sourceLocalPos = positionFire;
        }
    }
}