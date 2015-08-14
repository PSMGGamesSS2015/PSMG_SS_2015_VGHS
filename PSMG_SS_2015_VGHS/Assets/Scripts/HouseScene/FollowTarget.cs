using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        agent.SetDestination(target.position);
		transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

	}

}
