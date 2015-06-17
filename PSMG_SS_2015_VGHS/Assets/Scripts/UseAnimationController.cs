using UnityEngine;
using System.Collections;

public class UseAnimationController : MonoBehaviour {

    private Animator anim; 

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        anim.SetFloat("Speed", 1);
        anim.SetTrigger("GrabAnimation");
	}
}
