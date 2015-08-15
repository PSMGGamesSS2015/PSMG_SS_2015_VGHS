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

	string key;
	string node;
	string text = "";

	Dictionary<string,string> lyrics = new Dictionary<string,string>();
	Dictionary<string,string> keyNodeMap = new Dictionary<string,string>();


	// setup xml file first 
	void Start(){
		setupXmlData ();
		setupKeyNodeMap ();
	}

	/* Update is called once per frame
	 * Change shown subtitles
	 */
	void Update () {
		keyNodeMap.TryGetValue (key, out node);
		lyrics.TryGetValue(node, out text);
		subtl.GetComponent<Text>().text = text;
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

	// link key strings and xml nodes
	void setupKeyNodeMap(){
		//text bath
		keyNodeMap.Add ("entry", "jane_bath_1");
		keyNodeMap.Add ("mirror1", "jane_bath_2");
		keyNodeMap.Add ("mirror2", "jane_bath_3");
		keyNodeMap.Add ("paper", "jane_bath_4");
		keyNodeMap.Add ("theory1", "jane_bath_5");
		//text house 1
		keyNodeMap.Add ("welcome1", "michael_house_1_1");
		keyNodeMap.Add ("welcome2", "jane_house_1_1");
		keyNodeMap.Add ("welcome3", "michael_house_1_2");
		keyNodeMap.Add ("welcome4", "jane_house_1_2");
		keyNodeMap.Add ("welcome5", "michael_house_1_3");
		keyNodeMap.Add ("diningRoom", "michael_house_1_4");
		keyNodeMap.Add ("scar1_1", "jane_house_1_3");
		keyNodeMap.Add ("scar1_2", "michael_house_1_5");
		keyNodeMap.Add ("scar2_1", "michael_house_1_6");
		keyNodeMap.Add ("scar2_2", "jane_house_1_5");		
		keyNodeMap.Add ("scar2_3", "michael_house_1_7");		
		keyNodeMap.Add ("scar2_4", "jane_house_1_6");		
		keyNodeMap.Add ("theory2", "jane_house_1_4");		
		keyNodeMap.Add ("friends1", "jane_house_1_7");		
		keyNodeMap.Add ("friends2", "michael_house_1_8");		
		keyNodeMap.Add ("family1", "jane_house_1_8");		
		keyNodeMap.Add ("family2", "michael_house_1_9");		
		keyNodeMap.Add ("family3", "jane_house_1_9");		
		keyNodeMap.Add ("family4", "michael_house_1_10");		
		keyNodeMap.Add ("daughter1", "jane_house_1_10");		
		keyNodeMap.Add ("daughter2", "michael_house_1_11");		
		keyNodeMap.Add ("daughter3", "jane_house_1_11");		
		keyNodeMap.Add ("daughter4", "michael_house_1_12");		
		keyNodeMap.Add ("daughter5", "jane_house_1_12");		
		keyNodeMap.Add ("daughter6", "michael_house_1_13");		
		keyNodeMap.Add ("daughter7", "jane_house_1_13");		
		keyNodeMap.Add ("piano1", "michael_house_1_14");		
		keyNodeMap.Add ("piano2", "jane_house_1_14");		
		keyNodeMap.Add ("piano3", "michael_house_1_15");		
		keyNodeMap.Add ("daughter2_1", "jane_house_1_15");		
		keyNodeMap.Add ("daughter2_2", "michael_house_1_16");		
		keyNodeMap.Add ("daughter2_3", "michael_house_1_17");		
		keyNodeMap.Add ("daughter2_4", "jane_house_1_16");		
		keyNodeMap.Add ("daughter2_5", "michael_house_1_18");		
		keyNodeMap.Add ("daughter2_6", "jane_house_1_17");		
		keyNodeMap.Add ("daughter2_7", "michael_house_1_19");		
		keyNodeMap.Add ("missingPictureFound", "jane_house_1_18");		
		keyNodeMap.Add ("picture1", "jane_house_1_19");		
		keyNodeMap.Add ("picture2", "michael_house_1_20");		
		keyNodeMap.Add ("picture3", "jane_house_1_20");
		keyNodeMap.Add ("picture4", "michael_house_1_21");
		keyNodeMap.Add ("familyAlbum1_1", "jane_house_1_21");		
		keyNodeMap.Add ("familyAlbum1_2", "michael_house_1_22");		
		keyNodeMap.Add ("noteMonolog", "jane_house_1_22");		
		keyNodeMap.Add ("conservatory1", "jane_house_1_23");		
		keyNodeMap.Add ("conservatory2", "michael_house_1_23");		
		keyNodeMap.Add ("conservatory3", "jane_house_1_24");		
		keyNodeMap.Add ("glassTable1", "jane_house_1_25");		
		keyNodeMap.Add ("glassTable2", "michael_house_1_24");		
		keyNodeMap.Add ("glassTable3", "jane_house_1_26");		
		keyNodeMap.Add ("glassTable4", "michael_house_1_25");		
		keyNodeMap.Add ("glassTable5", "jane_house_1_27");		
		keyNodeMap.Add ("childsroom1", "michael_house_1_26");		
		keyNodeMap.Add ("childsroom2", "jane_house_1_28");		
		keyNodeMap.Add ("childsroom3", "michael_house_1_27");		
		keyNodeMap.Add ("guestroom1", "michael_house_1_28");		
		keyNodeMap.Add ("guestroom2", "jane_house_1_29");		
		keyNodeMap.Add ("guestroom3", "michael_house_1_29");		
		keyNodeMap.Add ("guestroom4", "jane_house_1_30");		
		keyNodeMap.Add ("workroom", "michael_house_1_30");		
		keyNodeMap.Add ("bookshelf", "jane_house_1_31");		
		keyNodeMap.Add ("bedroom1", "michael_house_1_31");		
		keyNodeMap.Add ("bedroom2", "jane_house_1_32");		
		keyNodeMap.Add ("adressBook1", "jane_house_1_33");		
		keyNodeMap.Add ("adressBook2", "michael_house_1_32");		
		keyNodeMap.Add ("scene1Ending1", "michael_house_1_33");		
		keyNodeMap.Add ("scene1Ending2", "jane_house_1_34");		
		keyNodeMap.Add ("scene1Ending3", "michael_house_1_34");
		//text house 2
		keyNodeMap.Add ("dizzy1", "jane_house_2_1");		
		keyNodeMap.Add ("paulaPhoneDialog1", "paula_house_2_1");
	}

	// Setter for subtitle that needs to be shown
	public void setKeyWord(string keyString){
		key = keyString ;
	}
}
