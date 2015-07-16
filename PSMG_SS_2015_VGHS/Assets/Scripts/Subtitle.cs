using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;

/* SUBTITLES
 * Handle style of Subtitle Text here.
 */ 
public class Subtitle : MonoBehaviour {

	public TextAsset textAsset;
	public Text subtl;

	string show;
	string text = "";

	Dictionary<string,string> lyrics = new Dictionary<string,string>();


	// setup xml file first 
	void Start(){
		setupXmlData ();
	}

	/* Update is called once per frame
	 * Change shown subtitles
	 */
	void Update () {
		switch(show){
		// text of bath scene
		case "entry":
			lyrics.TryGetValue("jane_bath_1", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "mirror1":
			lyrics.TryGetValue("jane_bath_2", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "mirror2":
			lyrics.TryGetValue("jane_bath_3", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "paper":
			lyrics.TryGetValue("jane_bath_4", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "theory1":
			lyrics.TryGetValue("jane_bath_5", out text);
			subtl.GetComponent<Text>().text = text;
			break;

		// text of first house scene
		case "welcome1": 
			lyrics.TryGetValue("michael_house_1_1", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "welcome2": 
			lyrics.TryGetValue("jane_house_1_1", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "welcome3": 
			lyrics.TryGetValue("michael_house_1_2", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "welcome4": 
			lyrics.TryGetValue("jane_house_1_2", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "welcome5": 
			lyrics.TryGetValue("michael_house_1_3", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "diningRoom": 
			lyrics.TryGetValue("michael_house_1_4", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "scar1_1":
			lyrics.TryGetValue("jane_house_1_3", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "scar1_2":
			lyrics.TryGetValue("michael_house_1_5", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "scar2_1":
			lyrics.TryGetValue("michael_house_1_6", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "scar2_2":
			lyrics.TryGetValue("jane_house_1_5", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "scar2_3":
			lyrics.TryGetValue("michael_house_1_7", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "scar2_4":
			lyrics.TryGetValue("jane_house_1_6", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "theory2":
			lyrics.TryGetValue("jane_house_1_4", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "friends1":
			lyrics.TryGetValue("jane_house_1_7", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "friends2":
			lyrics.TryGetValue("michael_house_1_8", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "family1":
			lyrics.TryGetValue("jane_house_1_8", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "family2":
			lyrics.TryGetValue("michael_house_1_9", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "family3":
			lyrics.TryGetValue("jane_house_1_9", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "family4":
			lyrics.TryGetValue("michael_house_1_10", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "daughter1":
			lyrics.TryGetValue("jane_house_1_10", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "daughter2":
			lyrics.TryGetValue("michael_house_1_11", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "daughter3":
			lyrics.TryGetValue("jane_house_1_11", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "daughter4":
			lyrics.TryGetValue("michael_house_1_12", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "daughter5":
			lyrics.TryGetValue("jane_house_1_12", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "daughter6":
			lyrics.TryGetValue("michael_house_1_13", out text);
			subtl.GetComponent<Text>().text = text;
			break;
		case "daughter7":
			lyrics.TryGetValue("jane_house_1_13", out text);
			subtl.GetComponent<Text>().text = text;
			break;

		default: break;
		}
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
				if(childNode.Name.ToString().Equals("#comment") == false){
					lyrics.Add(childNode.Name.ToString(), childNode.InnerText);
				}
			}
		}
	}

	// Setter for subtitle that needs to be shown
	public void setKeyWord(string key){
		show = key;
	}
}
