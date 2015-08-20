using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighlightObject : MonoBehaviour {

    Color startColor;
    Material startMaterial;
    public Material glow;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("FPSController");
        startMaterial = gameObject.GetComponent<Renderer>().material;
        startColor = gameObject.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 3)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            gameObject.GetComponent<Renderer>().material = glow;
        }
        else
        {
            //gameObject.GetComponent<Renderer>().material.color = startColor;
            gameObject.GetComponent<Renderer>().material = startMaterial;
        }
	}

    public Material GetObjectStartMaterial()
    {
        return startMaterial;
    }
}
