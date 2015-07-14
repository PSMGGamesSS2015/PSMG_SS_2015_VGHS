using UnityEngine;
using System.Collections;

public class HouseController : MonoBehaviour {

	public GUIController guiController;

	int dialogCount = 0;
	bool welcomeDialog = true;

	// Setup Inventory when House Scene starts
	void Start () {
		guiController.addHint("dressHint");
		guiController.addHint("noteHint");
		guiController.forceThSetup();
		guiController.toggleSubtl ("welcome");
	}
	
	// Update is called once per frame
	void Update () {
		// during welcoming
		if(welcomeDialog){
			toggleWelcomeDialog ();
		}

		getKeyInteractions ();

	}

	// handle key interactions here
	void getKeyInteractions(){

		// handle dialog counting for each Dialog
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(welcomeDialog != false){
				dialogCount++;
			} else{
				dialogCount = 0;
			}
		}
	}

	// handle procedure of welcoming dialog here
	void toggleWelcomeDialog(){
		switch (dialogCount) {
		case 0:
			guiController.toggleSubtl ("welcome1");
			break;
		case 1:
			guiController.toggleSubtl ("welcome2");
			break;
		case 2:
			guiController.toggleSubtl ("welcome3");
			break;
		case 3:
			guiController.toggleSubtl ("welcome4");
			break;
		case 4:
			guiController.toggleSubtl ("welcome5");
			break;
		}
	}
}
