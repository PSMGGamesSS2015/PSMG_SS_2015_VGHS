using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// get alltime needed keyinteractions and control player movement
	void Update () {
		getKeyInteractions ();
		togglePlayer ();
	}

	// handle keyboard input here 
	void getKeyInteractions(){

		// open/close inventory with 'I'
		if (Input.GetKeyDown (KeyCode.I) && guiController.subtlShown != true && guiController.interactionPanelShown != true) {
			guiController.toggleInventory();
		}

		// close subtitles
		if (Input.GetKeyDown (KeyCode.Space) && guiController.isShowing ()) {
			guiController.toggleSubtl (null);
		}

		// toggle pause menu
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(GetComponent<SceneFader>().isFading == false){
				guiController.togglePauseMenu();
			}				
		}
	}

	// toggle player movement, player is only able to move if nothing is shown on the GUI or scene isnt fading
	void togglePlayer(){
		if (guiController.isShowing () || GetComponent<SceneFader> ().isFading) {
			player.GetComponent<FirstPersonController> ().enabled = false;
		} else {
			player.GetComponent<FirstPersonController> ().enabled = true;
		}
	}

	// handle interaction with continue button in pause menu
	public void OnContinueClicked(){
		guiController.togglePauseMenu ();
	}

	// handle interaction with back button in pause menu
	public void OnBackClicked(){
		GetComponent<SceneFader>().SwitchScene (0);
	}

    public void OnOptionsClicked()
    {
        guiController.GoToOptionsMenu();
    }

    public void OnOptionsBackClicked()
    {
        guiController.GoFromOptionsBackToPauseMenu();
    }

    public void OnQualityClicked()
    {
        guiController.GoToQualityMenu();
    }

    public void OnQualityBackClicked()
    {
        guiController.GoFromQualityToOptionsMenu();
    }

    public void SetQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
        OnQualityBackClicked();
    }
}
