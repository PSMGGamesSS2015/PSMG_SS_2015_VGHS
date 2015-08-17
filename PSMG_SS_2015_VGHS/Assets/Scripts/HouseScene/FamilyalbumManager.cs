using UnityEngine;
using System.Collections;

public class FamilyalbumManager : MonoBehaviour {
	
	public GameObject guiController;

	public void OnCloseClicked(){
		guiController.GetComponent<GUIController> ().toggleFamilyalbum ();
	}
}
