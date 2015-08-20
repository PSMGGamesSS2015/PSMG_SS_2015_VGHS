using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MedicinebookController : MonoBehaviour {

	public GameObject guiController;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;

	public void onAspirinClicked(){
		guiController.GetComponent<GUIController>().pillQuizAnswer = "Aspirin";
	}

	public void onAtivanClicked(){
		guiController.GetComponent<GUIController>().pillQuizAnswer = "Ativan";
	}

	public void onHaldolClicked(){
		guiController.GetComponent<GUIController>().pillQuizAnswer = "Haldol";
	}

	public void onRitalinClicked(){
		guiController.GetComponent<GUIController>().pillQuizAnswer = "Ritalin";
	}

}
