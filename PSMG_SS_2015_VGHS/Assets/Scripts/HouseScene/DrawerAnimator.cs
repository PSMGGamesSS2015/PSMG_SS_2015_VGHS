using UnityEngine;
using System.Collections;

public class DrawerAnimator : MonoBehaviour {

	public void openDrawer(){

		transform.position += transform.TransformDirection (Vector3.right)*0.7f;

	}

	public void closeDrawer(){

		transform.position -= transform.TransformDirection (Vector3.right)*0.7f;

	}
}
