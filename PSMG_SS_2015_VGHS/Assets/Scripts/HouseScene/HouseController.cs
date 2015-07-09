using UnityEngine;
using System.Collections;

public class HouseController : MonoBehaviour {

	public GUIController guiController;

	// Setup Inventory when House Scene starts
	void Start () {
		guiController.addHint("dressHint");
		guiController.addHint("noteHint");
		guiController.forceThSetup();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
