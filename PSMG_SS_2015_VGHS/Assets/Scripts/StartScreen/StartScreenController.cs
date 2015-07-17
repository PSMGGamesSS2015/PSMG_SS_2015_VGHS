using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/* MAIN CONTROLLER
 * This is the games main controlling Script. 
 * It receives event based data from other scripts (like where collisions are triggered).
 * Its task is to store and process this data and to communicate the results to other scripts.
 * So every logic of the game is handled here.
 * Interactions depending on the inventory like dragging & dropping hints are handled by the SlotScript and Inventory Script
 */

public class StartScreenController : MonoBehaviour {

	public Canvas exitMenu;
	public GameObject startScreenContent;
	public Button playButton;
	public Button optionsButton;
	public Button exitButton;
    public GameObject optionsMenu;
    public GameObject qualityMenu;
    public GameObject resolutionMenu;
    public Toggle checkbox;
    public GameObject startScreen;

	bool buttonPressed = false;
    bool windowed;


    /* Update is called once per frame
     * Toggle Button visibility here
     */

    void Start()
    {
        windowed = Screen.fullScreen;
        if (Screen.fullScreen == false)
        {
            checkbox.isOn = true;
        }
    }

    void Update(){
		if (buttonPressed) {
			startScreenContent.SetActive(false);
		}
		else{
			startScreenContent.SetActive(true);
		}
    }

	// start game when play button pressed
	public void OnPlayButtonPressed(){
		buttonPressed = true;
		GetComponent<SceneFader>().SwitchScene(1);
	}

	// handle anything belonging to option button here
	public void OnOptionsButtonPressed(){
        optionsMenu.SetActive(true);
        startScreen.SetActive(false);
	}

	// show exit menu
	public void OnExitButtonPressed(){
		buttonPressed = true;
		exitMenu.enabled = true;
	}

	// quit game if yes button in exit menu was clicked
	public void OnYesButtonPressed(){
		Application.Quit();
	}

	// close exit menu if no was clicked
	public void OnNoButtonPressed(){
		buttonPressed = false;
		exitMenu.enabled = false;
	}

    public void OnQualityButtonPressed()
    {
        optionsMenu.SetActive(false);
        qualityMenu.SetActive(true);

    }

    public void OnOptionsBackButtonPressed()
    {
        optionsMenu.SetActive(false);
        startScreen.SetActive(true);
    }

    public void OnQualityBackButtonPressed()
    {
        qualityMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SetQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
        OnQualityBackButtonPressed();
    }

    public void OnResolutionClicked()
    {
        optionsMenu.SetActive(false);
        resolutionMenu.SetActive(true);
    }

    public void OnResolutionBackClicked()
    {
        resolutionMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SetResolution(string res)
    {
        string[] array = res.Split(new char[] { 'x' });

        int width = int.Parse(array[0]);
        int height = int.Parse(array[1]);

        Screen.SetResolution(width, height, windowed);

        Debug.Log(Screen.currentResolution.width + "x" + Screen.currentResolution.height);
    }

    public void SetWindowedMode()
    {
        windowed = !windowed;
    }
}
