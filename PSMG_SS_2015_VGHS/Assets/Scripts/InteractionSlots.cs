using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractionSlots : MonoBehaviour, IPointerDownHandler {

	public GameObject text;

	public int slotNumber;
	public bool isFilled = false;

	MichaelInteractions interactionPanel;

	// Use this for initialization
	void Start () {
		interactionPanel = GameObject.FindGameObjectWithTag ("InteractionPanel").GetComponent<MichaelInteractions>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// setup text for this slot
	public void setupText(string content){
		text.GetComponent<Text> ().text = content;
		isFilled = true;
	}

	// Do sth. when clicked on slot
	public void OnPointerDown(PointerEventData data){
		interactionPanel.interactionAlert (text.GetComponent<Text> ().text);
		gameObject.SetActive (false);
	}

	// give feedback about the actual content on this slot
	public string getActualContent(){
		return text.GetComponent<Text> ().text;
	}
}
