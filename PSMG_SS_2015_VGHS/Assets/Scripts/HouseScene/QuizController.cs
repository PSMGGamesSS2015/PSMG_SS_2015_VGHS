using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuizController : MonoBehaviour {

	public GameObject guiController;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;


	public void setupButtonText(string button1text, string button2text, string button3text, string button4text){
		GameObject.FindGameObjectWithTag ("Button1").GetComponent<Text> ().text = button1text;
		GameObject.FindGameObjectWithTag ("Button2").GetComponent<Text> ().text = button2text;
		GameObject.FindGameObjectWithTag ("Button3").GetComponent<Text> ().text = button3text;
		GameObject.FindGameObjectWithTag ("Button4").GetComponent<Text> ().text = button4text;
	}

	public void button1Clicked(){
		guiController.GetComponent<GUIController> ().checkQuizAnswer (GameObject.FindGameObjectWithTag ("Button1").GetComponent<Text> ().text);
	}

	public void button2Clicked(){
		guiController.GetComponent<GUIController> ().checkQuizAnswer (GameObject.FindGameObjectWithTag ("Button2").GetComponent<Text> ().text);
	}

	public void button3Clicked(){
		guiController.GetComponent<GUIController> ().checkQuizAnswer (GameObject.FindGameObjectWithTag ("Button3").GetComponent<Text> ().text);
	}

	public void button4Clicked(){
		guiController.GetComponent<GUIController> ().checkQuizAnswer (GameObject.FindGameObjectWithTag ("Button4").GetComponent<Text> ().text);
	}
}
