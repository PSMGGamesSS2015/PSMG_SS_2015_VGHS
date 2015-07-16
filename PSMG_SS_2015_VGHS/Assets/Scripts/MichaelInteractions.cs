using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class MichaelInteractions : MonoBehaviour {

	public TextAsset textAsset;


	public List <GameObject> iSlots = new List<GameObject> ();

	public GameObject interactionSlots;

	public int slotNum;
	public int slotDistY;
	public int iX;
	public int iY;

	public string triggeredInteraction = "";

	Dictionary<string,string> interactionContent = new Dictionary<string,string>();
	

	// Use this for initialization
	void Start () {
		setupXmlData ();
		gameObject.SetActive (false);
	}

	// setup the interaction panel
	public void setupInteractions(){

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
	public void setupInteraction(string key){
		for (int i = 0; i < slotNum; i ++) {
			if (iSlots[i].GetComponent<InteractionSlots>().isFilled == false){
				iSlots[i].SetActive(true);
				iSlots[i].GetComponent<InteractionSlots>().setupText(getText(key));
				break;
			}
		}
	}

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

	// called by interaction slots if user interacted with michael
	public void interactionAlert(string interaction){
		switch (interaction) {
		case "Narbe":
			triggeredInteraction = "scar1_";
			break;
		case "Woher kommt die Narbe?":
			triggeredInteraction = "scar2_";
			break;
		}
	}
}
