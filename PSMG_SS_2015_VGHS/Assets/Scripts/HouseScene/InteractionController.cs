using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using System.Linq;

public class InteractionController : MonoBehaviour {

	public TextAsset textAsset;


	public List <GameObject> iSlots = new List<GameObject> ();
	Dictionary<string,string> interactionDialogMap = new Dictionary<string,string>();

	public GameObject interactionSlots;

	public int slotNum;
	public int slotDistY;
	public int iX;
	public int iY;

	public string triggeredInteraction = "";

	string interactedWith = "Michael";

	List<string> openMichaelInteractions = new List<string>();
	List<string> openPaulaInteractions = new List<string>();

	Dictionary<string,string> interactionContent = new Dictionary<string,string>();
	

	// Use this for initialization
	void Start () {
		setupXmlData ();
		setupInteractionDialogMap ();
		gameObject.SetActive (false);
	}

	// setup the interaction panel
	public void setupInteractionSlots(){

		int slotAmount = 0;

		for(int i = 0; i < slotNum; i++){
			GameObject slot = (GameObject) Instantiate(interactionSlots);
			slot.GetComponent<InteractionSlots>().slotNumber = slotAmount;
			slot.transform.parent = this.gameObject.transform;
			slot.GetComponent<RectTransform>().localPosition = new Vector3 (iX, iY, 0);
			slot.name = "iSlot"+i;
			iSlots.Add(slot);
			iY -= slotDistY;
			slotAmount++;
		}
		for (int i = 0; i < slotNum; i++) {
			iSlots[i].SetActive(false);
		}
	}

	// set new interaction to slot
	public void setupInteraction(string key, string person){

		interactedWith = person;
		switch (person) {
		case "Michael":
			openMichaelInteractions.Add(key);
			break;
		case "Paula":
			openPaulaInteractions.Add (key);
			break;
		}
		updateInteractionPanel (person);
	}

	public void updateInteractionPanel(string person){
		// first clear all slots
		for (int j = 0; j < slotNum; j ++) {
			iSlots [j].GetComponent<InteractionSlots>().isFilled = false;
		}
		switch (person) {
		case "Michael":
			for (int i = 0; i < openMichaelInteractions.Count; i++) {
				if (!iSlots.Exists (x => x.name.Equals (openMichaelInteractions[i]))) {
					
					for (int j = 0; j < slotNum; j ++) {
						if (!iSlots [j].GetComponent<InteractionSlots> ().isFilled) {
							iSlots [j].SetActive (true);
							iSlots [j].GetComponent<InteractionSlots> ().setupText (getText (openMichaelInteractions[i]));
							iSlots [j].name = openMichaelInteractions[i];
							iSlots [j].tag = "MichaelSlot";
							break;
						}
					}
				}
			}
			break;
		case "Paula":
			for (int i = 0; i < openPaulaInteractions.Count; i++) {
				if (!iSlots.Exists (x => x.name.Equals (openPaulaInteractions[i]))) {
					
					for (int j = 0; j < slotNum; j ++) {
						if (!iSlots [j].GetComponent<InteractionSlots> ().isFilled) {
							iSlots [j].SetActive (true);
							iSlots [j].GetComponent<InteractionSlots> ().setupText (getText (openPaulaInteractions[i]));
							iSlots [j].name = openPaulaInteractions[i];
							iSlots [j].tag = "PaulaSlot";
							break;
						}
					}
				}
			}
			break;
		}
	}

	// get description for interaction by key
	string getText(string key){
		string content = "";
		interactionContent.TryGetValue(key, out content);
		return content;
	}

	// recieve text data from xml file
	void setupXmlData(){
		XmlDocument xmlDoc = new XmlDocument(); 
		xmlDoc.LoadXml (textAsset.text);
		XmlNodeList xmlList = xmlDoc.GetElementsByTagName("ger");
		
		//extract child nodes into new xml node list and prepare dictionary
		foreach(XmlNode node in xmlList){
			XmlNodeList stringList = node.ChildNodes;
			
			//extraxt strings of child nodes and insert into dictionary
			foreach(XmlNode childNode in stringList){
				if(childNode.Name.ToString() != "#comment"){
					interactionContent.Add(childNode.Name.ToString(), childNode.InnerText);
				}
			}
		}
	}

	// called by interaction slots if user triggered an interaction
	public void interactionAlert(string interaction, string slotName, string slotTag){
		interactionDialogMap.TryGetValue (interaction, out triggeredInteraction);
		switch (slotTag) {
		case "MichaelSlot":
			openMichaelInteractions.Remove(slotName);
			break;
		case "PaulaSlot":
			openPaulaInteractions.Remove(slotName);
			break;
		}
	}

	// setup list that connects interaction keys with dialog keys
	void setupInteractionDialogMap (){
		interactionDialogMap.Add ("Narbe", "scar1_");
		interactionDialogMap.Add ("Woher kommt die Narbe?", "scar2_");
		interactionDialogMap.Add ("Meine Freunde", "friends");
		interactionDialogMap.Add ("Meine Familie", "family");
		interactionDialogMap.Add ("Kinder?", "daughter");
		interactionDialogMap.Add ("Fehlendes Foto", "picture");
		interactionDialogMap.Add ("Wer bist du?", "paulaIntroduction");
		interactionDialogMap.Add ("Wer war am Telephon?", "paulaPhoneCall");
		interactionDialogMap.Add ("Wie oft bist du hier?", "paulaIntroduction2_");
		interactionDialogMap.Add ("Wer ist die Frau?", "mother");
		interactionDialogMap.Add ("Wer ist der Mann?", "father");
		interactionDialogMap.Add ("Wo ist mein Adressbuch?", "lostAdressBook");
		interactionDialogMap.Add ("Tabletten?", "pills");
		interactionDialogMap.Add ("Haldol", "haldol2_");
		interactionDialogMap.Add ("Besuch war eine Lüge!", "visit");
	}
}
