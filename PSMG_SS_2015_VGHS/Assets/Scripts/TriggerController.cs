using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	public string triggerTag = "";

	public void setTriggerTag(string tag){
		triggerTag = tag;
	}

	public string getTriggerTag(){
		return triggerTag;
	}
}
