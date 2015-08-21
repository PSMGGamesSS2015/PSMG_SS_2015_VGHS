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
		keyNodeMap.Add ("paulaIntroduction1", "jane_house_2_2");
		keyNodeMap.Add ("paulaIntroduction2", "paula_house_2_2");
		keyNodeMap.Add ("paulaIntroduction3", "jane_house_2_3");
		keyNodeMap.Add ("paulaIntroduction4", "paula_house_2_3");
		keyNodeMap.Add ("paulaIntroduction5", "jane_house_2_4");
		keyNodeMap.Add ("paulaIntroduction6", "paula_house_2_4");
		keyNodeMap.Add ("paulaIntroduction7", "jane_house_2_5");
		keyNodeMap.Add ("paulaIntroduction8", "paula_house_2_5");
		keyNodeMap.Add ("paulaIntroduction9", "jane_house_2_6");
		keyNodeMap.Add ("paulaPhoneCall1", "jane_house_2_7");
		keyNodeMap.Add ("paulaPhoneCall2", "paula_house_2_6");
		keyNodeMap.Add ("paulaPhoneCall3", "jane_house_2_8");
		keyNodeMap.Add ("paulaPhoneCall4", "paula_house_2_7");
		keyNodeMap.Add ("paulaPhoneCall5", "jane_house_2_9");
		keyNodeMap.Add ("paulaPhoneCall6", "paula_house_2_8");
		keyNodeMap.Add ("paulaPhoneCall7", "jane_house_2_10");
		keyNodeMap.Add ("paulaIntroduction2_1", "jane_house_2_11");
		keyNodeMap.Add ("paulaIntroduction2_2", "paula_house_2_9");
		keyNodeMap.Add ("paulaIntroduction2_3", "jane_house_2_12");
		keyNodeMap.Add ("paulaIntroduction2_4", "paula_house_2_10");
		keyNodeMap.Add ("paulaIntroduction2_5", "jane_house_2_13");
		keyNodeMap.Add ("scare1", "jane_house_2_14");
		keyNodeMap.Add ("scare2", "michael_house_2_1");
		keyNodeMap.Add ("scare3", "jane_house_2_15");
		keyNodeMap.Add ("scare4", "michael_house_2_2");
		keyNodeMap.Add ("father1", "jane_house_2_16");
		keyNodeMap.Add ("father2", "michael_house_2_3");
		keyNodeMap.Add ("father3", "jane_house_2_17");
		keyNodeMap.Add ("father4", "michael_house_2_4");
		keyNodeMap.Add ("father5", "jane_house_2_18");
		keyNodeMap.Add ("mother1", "jane_house_2_19");
		keyNodeMap.Add ("mother2", "michael_house_2_5");
		keyNodeMap.Add ("mother3", "jane_house_2_20");
		keyNodeMap.Add ("mother4", "michael_house_2_6");
		keyNodeMap.Add ("mother5", "jane_house_2_21");
		keyNodeMap.Add ("mother6", "michael_house_2_7");
		keyNodeMap.Add ("mother7", "jane_house_2_22");
		keyNodeMap.Add ("mother8", "michael_house_2_8");
		keyNodeMap.Add ("mother9", "jane_house_2_23");
		keyNodeMap.Add ("bedroom3", "jane_house_2_24");
		keyNodeMap.Add ("adressbookLost", "jane_house_2_25");
		keyNodeMap.Add ("bedtime1", "michael_house_2_9");
		keyNodeMap.Add ("bedtime2", "jane_house_2_26");
		keyNodeMap.Add ("bedtime3", "michael_house_2_10");
		keyNodeMap.Add ("lostAdressBook1", "jane_house_2_27");
		keyNodeMap.Add ("lostAdressBook2", "michael_house_2_11");
		keyNodeMap.Add ("lostAdressBook3", "jane_house_2_28");
		keyNodeMap.Add ("lostAdressBook4", "michael_house_2_12");
		keyNodeMap.Add ("lostAdressBook5", "jane_house_2_29");
		keyNodeMap.Add ("lostAdressBook6", "michael_house_2_13");
		keyNodeMap.Add ("lostAdressBook7", "jane_house_2_30");
		keyNodeMap.Add ("lostAdressBook8", "michael_house_2_14");
		keyNodeMap.Add ("lostAdressBook9", "jane_house_2_31");
		keyNodeMap.Add ("lostAdressBook10", "michael_house_2_15");
		keyNodeMap.Add ("lostAdressBook11", "jane_house_2_32");
		//text house 3
		keyNodeMap.Add ("dizzy2", "jane_house_3_1");
		keyNodeMap.Add ("theory3", "jane_house_3_2");
		keyNodeMap.Add ("pills1", "jane_house_3_3");
		keyNodeMap.Add ("pills2", "michael_house_3_1");
		keyNodeMap.Add ("pills3", "jane_house_3_4");
		keyNodeMap.Add ("pills4", "michael_house_3_2");
		keyNodeMap.Add ("pills5", "jane_house_3_5");
		keyNodeMap.Add ("pills6", "michael_house_3_3");
		keyNodeMap.Add ("pills7", "jane_house_3_6");
		keyNodeMap.Add ("pills8", "michael_house_3_4");
		keyNodeMap.Add ("callDoc", "jane_house_3_7");
		keyNodeMap.Add ("information1", "jane_house_3_8");
		keyNodeMap.Add ("information2", "information_1");
		keyNodeMap.Add ("information3", "jane_house_3_9");
		keyNodeMap.Add ("information2_1", "information_2");
		keyNodeMap.Add ("information2_2", "jane_house_3_10");
		keyNodeMap.Add ("meloffCall1", "information_3");
		keyNodeMap.Add ("meloffCall2", "meloff_1");
		keyNodeMap.Add ("meloffCall3", "jane_house_3_11");
		keyNodeMap.Add ("meloffCall4", "meloff_2");
		keyNodeMap.Add ("meloffCall5", "jane_house_3_12");
		keyNodeMap.Add ("meloffCall6", "meloff_3");
		keyNodeMap.Add ("meloffCall7", "jane_house_3_13");
		keyNodeMap.Add ("meloffCall8", "meloff_4");
		keyNodeMap.Add ("meloffCall9", "jane_house_3_14");
		keyNodeMap.Add ("meloffCall10", "meloff_5");
		keyNodeMap.Add ("meloffCall11", "jane_house_3_15");
		keyNodeMap.Add ("meloffCall12", "meloff_6");
		keyNodeMap.Add ("scene3Ending1", "michael_house_3_5");		
		keyNodeMap.Add ("scene3Ending2", "michael_house_3_6");		
		keyNodeMap.Add ("scene3Ending3", "jane_house_3_16");
		keyNodeMap.Add ("scene3Ending4", "michael_house_3_7");		
		keyNodeMap.Add ("scene3Ending5", "jane_house_3_17");
		//text house 4
		keyNodeMap.Add ("dizzy3", "jane_house_4_1");
		keyNodeMap.Add ("paulaPhoneCall2_1", "paula_house_4_1");
		keyNodeMap.Add ("paulaPhoneCall2_2", "jane_house_4_2");
		keyNodeMap.Add ("paulaPhoneCall3_1", "paula_house_4_2");
		keyNodeMap.Add ("paulaPhoneCall3_2", "paula_mum_1");
		keyNodeMap.Add ("paulaPhoneCall3_3", "paula_house_4_3");
		keyNodeMap.Add ("paulaPhoneCall3_4", "paula_mum_2");
		keyNodeMap.Add ("paulaPhoneCall3_5", "paula_house_4_4");
		keyNodeMap.Add ("amnesia1", "jane_house_4_3");
		keyNodeMap.Add ("amnesia2", "jane_house_4_4");
		keyNodeMap.Add ("amnesia3", "jane_house_4_5");
		keyNodeMap.Add ("amnesia4", "jane_house_4_6");
		keyNodeMap.Add ("paulaPills1", "paula_house_4_5");
		keyNodeMap.Add ("paulaPills2", "jane_house_4_7");
		keyNodeMap.Add ("paulaPills3", "paula_house_4_6");
		keyNodeMap.Add ("paulaPills4", "jane_house_4_8");
		keyNodeMap.Add ("paulaPills5", "jane_house_4_9");
		keyNodeMap.Add ("paulaPills6", "jane_house_4_10");
		keyNodeMap.Add ("hidePills", "jane_house_4_11");
		keyNodeMap.Add ("freshWater1", "paula_house_4_7");
		keyNodeMap.Add ("freshWater2", "jane_house_4_12");
		keyNodeMap.Add ("freshWater3", "paula_house_4_8");
		keyNodeMap.Add ("freshWater4", "jane_house_4_13");
		keyNodeMap.Add ("box", "jane_house_4_14");
		keyNodeMap.Add ("ativan", "jane_house_4_15");
		keyNodeMap.Add ("haldol", "jane_house_4_16");
		keyNodeMap.Add ("wrongPills", "jane_house_4_17");
		keyNodeMap.Add ("box2", "jane_house_4_18");
		keyNodeMap.Add ("theory4", "jane_house_4_19");
		keyNodeMap.Add ("phoneDefault", "jane_house_4_20");
		keyNodeMap.Add ("information3_1", "jane_house_4_21");
		keyNodeMap.Add ("information3_2", "information_4");
		keyNodeMap.Add ("information3_3", "jane_house_4_22");
		keyNodeMap.Add ("warderobe", "jane_house_4_23");
		keyNodeMap.Add ("brotherCall1", "information_3");
		keyNodeMap.Add ("brotherCall2", "eleanor_1");
		keyNodeMap.Add ("brotherCall3", "jane_house_4_24");
		keyNodeMap.Add ("brotherCall4", "eleanor_2");
		keyNodeMap.Add ("brotherCall5", "jane_house_4_25");
		keyNodeMap.Add ("brotherCall6", "eleanor_3");
		keyNodeMap.Add ("brotherCall7", "jane_house_4_26");
		keyNodeMap.Add ("brotherCall8", "eleanor_4");
		keyNodeMap.Add ("brotherCall9", "jane_house_4_27");
		keyNodeMap.Add ("brotherCall10", "eleanor_5");
		keyNodeMap.Add ("brotherCall11", "michael_house_4_1");
		keyNodeMap.Add ("janeCought1", "michael_house_4_2");
		keyNodeMap.Add ("janeCought2", "jane_house_4_28");
		keyNodeMap.Add ("janeCought3", "michael_house_4_3");
		keyNodeMap.Add ("janeCought4", "jane_house_4_29");
		keyNodeMap.Add ("janeCought5", "michael_house_4_4");
		keyNodeMap.Add ("haldol2_1", "jane_house_4_30");
		keyNodeMap.Add ("haldol2_2", "michael_house_4_5");
		keyNodeMap.Add ("haldol2_3", "jane_house_4_31");
		keyNodeMap.Add ("haldol2_4", "michael_house_4_6");
		keyNodeMap.Add ("haldol2_5", "jane_house_4_32");
		keyNodeMap.Add ("haldol2_6", "michael_house_4_7");
		keyNodeMap.Add ("haldol2_7", "jane_house_4_33");
		keyNodeMap.Add ("haldol2_8", "michael_house_4_8");
		keyNodeMap.Add ("haldol2_9", "jane_house_4_34");
		keyNodeMap.Add ("haldol2_10", "michael_house_4_9");
		keyNodeMap.Add ("haldol2_11", "jane_house_4_35");
		keyNodeMap.Add ("visit1", "jane_house_4_36");
		keyNodeMap.Add ("visit2", "michael_house_4_10");
		keyNodeMap.Add ("theory5", "jane_house_4_37");
		keyNodeMap.Add ("daughterDead1", "jane_house_4_38");
		keyNodeMap.Add ("daughterDead2", "michael_house_4_11");
		keyNodeMap.Add ("daughterDead3", "jane_house_4_39");
		keyNodeMap.Add ("daughterDead4", "michael_house_4_12");
		keyNodeMap.Add ("daughterDead5", "jane_house_4_40");
		keyNodeMap.Add ("daughterDead6", "michael_house_4_13");
		keyNodeMap.Add ("daughterDead7", "jane_house_4_41");
		keyNodeMap.Add ("daughterDead8", "michael_house_4_14");
		keyNodeMap.Add ("daughterDead9", "jane_house_4_42");
		keyNodeMap.Add ("daughterDead10", "michael_house_4_15");
		keyNodeMap.Add ("daughterDead11", "jane_house_4_43");
		keyNodeMap.Add ("janeDespaired1", "jane_house_5_1");
		keyNodeMap.Add ("janeDespaired2", "jane_house_5_2");
		keyNodeMap.Add ("janeDespaired3", "jane_house_5_3");
		keyNodeMap.Add ("janeDespaired4", "jane_house_5_4");
		keyNodeMap.Add ("janeDespaired5", "jane_house_5_5");
		keyNodeMap.Add ("janeDespaired6", "jane_house_5_6");
		keyNodeMap.Add ("michaelPaula1", "michael_house_5_1");
		keyNodeMap.Add ("michaelPaula2", "paula_house_5_1");
		keyNodeMap.Add ("michaelPaula3", "michael_house_5_2");
		keyNodeMap.Add ("michaelPaula4", "paula_house_5_2");
		keyNodeMap.Add ("michaelPaula5", "michael_house_5_3");
		keyNodeMap.Add ("michaelPaula6", "paula_house_5_3");
		keyNodeMap.Add ("michaelPaula7", "michael_house_5_4");
		keyNodeMap.Add ("michaelPaula8", "paula_house_5_4");
		keyNodeMap.Add ("michaelPaula9", "michael_house_5_5");
		keyNodeMap.Add ("michaelPaula10", "jane_house_5_7");
		keyNodeMap.Add ("lie", "jane_house_5_8");
	}

	// Setter for subtitle that needs to be shown
	public void setKeyWord(string keyString){
		key = keyString ;
	}
}
