using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* SUBTITLES
 * Handle style of Subtitle Text here.
 */ 
public class Subtitle : MonoBehaviour {

	string show;
	public Text subtl;
	public string entryText;
	public string mirrorText;
	public string bloodyDressText;
	public string paperText;
	

	/* Update is called once per frame
	 * Change shown subtitles
	 */
	void Update () {
		switch(show){
		case "entry":  
			subtl.GetComponent<Text>().text = entryText;
			break;
		case "mirror1": 
			subtl.GetComponent<Text>().text = mirrorText;
			break;
		case "mirror2": 
			subtl.GetComponent<Text>().text = bloodyDressText;
			break;
		case "paper": 
			subtl.GetComponent<Text>().text = paperText;
			break;
		default: 
			Debug.Log ("Subtitle/Update: couldnt show subtitle");
			break;
		}
	}

	// Setter for subtitle that needs to be shown
	public void setKeyWord(string key){
		show = key;
	}






}
