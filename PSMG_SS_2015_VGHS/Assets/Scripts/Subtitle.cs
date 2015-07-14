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
	Dictionary<string,string> obj;

	// setup xml file first 
	void Start(){
		setupXmlData ();
	}

	/* Update is called once per frame
	 * Change shown subtitles
	 */
	void Update () {
		switch(show){
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
			obj = new Dictionary<string, string>();
			
			//extraxt strings of child nodes and insert into dictionary
			foreach(XmlNode childNode in stringList){
				if(childNode.Name.ToString() != "#comment"){
					//obj.Add(childNode.Name.ToString(), childNode.InnerText);
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
