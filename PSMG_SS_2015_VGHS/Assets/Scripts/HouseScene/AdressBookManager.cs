using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;

public class AdressBookManager : MonoBehaviour {

	public GameObject name;
	public GameObject town;
	public GameObject street;
	public GameObject phone;
	public GameObject backButton;
	public GameObject nextButton;
	public TextAsset textAsset;
	public GameObject guiController;

	Dictionary<string,string> adresses = new Dictionary<string,string>();

	int page = 1;
	string nameText;
	string townText;
	string streetText;
	string phoneNumber;

	// Use this for initialization
	void Start () {
		setupXmlData ();
	}
	
	// Update is called once per frame
	void Update () {
		togglePageContent ();
		toggleButtonVisibility ();
	}

	// recieve text data from xml file
	void setupXmlData(){
		XmlDocument xmlDoc = new XmlDocument(); 
		xmlDoc.LoadXml (textAsset.text);
		XmlNodeList xmlList = xmlDoc.GetElementsByTagName("adresses");
		
		//extract child nodes into new xml node list and prepare dictionary
		foreach(XmlNode node in xmlList){
			XmlNodeList stringList = node.ChildNodes;
			
			//extraxt strings of child nodes and insert into dictionary
			foreach(XmlNode childNode in stringList){
				if(childNode.Name.ToString().Equals("#comment") == false){
					adresses.Add(childNode.Name.ToString(), childNode.InnerText);
				}
			}
		}
	}

	// manage the content shown on each page
	void togglePageContent(){
		adresses.TryGetValue ("name"+page, out nameText);
		name.GetComponent<Text> ().text = nameText;

		adresses.TryGetValue ("town"+page, out townText);
		town.GetComponent<Text> ().text = townText;

		adresses.TryGetValue ("street"+page, out streetText);
		street.GetComponent<Text> ().text = streetText;

		adresses.TryGetValue ("phone"+page, out phoneNumber);
		phone.GetComponent<Text> ().text = phoneNumber;
	}

	// toggle button visibility
	void toggleButtonVisibility(){
		// toggle back button visibility
		if (page == 1) {
			backButton.SetActive (false);
		} else {
			backButton.SetActive (true);
		}
		// toggle next button visibility
		if (page == 3) {
			nextButton.SetActive (false);
		} else {
			nextButton.SetActive (true);
		}
	}

	// react to next button and change page
	public void OnNextButtonClicked(){
		if (page < 3) {
			page++;
		}
	}

	// react to back button and change page
	public void OnBackButtonClicked(){
		if (page > 1) {
			page--;
		}
	}

	public void OnCloseClicked(){
		guiController.GetComponent<GUIController> ().toggleAdressBook ();
	}


}
