using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour {

    bool isPaused = false;
    public GameObject player;
    Rect menu = new Rect(0, 0, Screen.width, Screen.height);
    public Texture2D texture;
    public Font font;
    SceneFader sceneFader;

	// Use this for initialization
	void Start () {
        sceneFader = GetComponent<SceneFader>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleTimeScale();
        }
	}

    void OnGUI()
    {
        if (isPaused)
        {
            GUI.depth = 1;
            GUI.DrawTexture(menu, texture);
            GUI.depth = 0;
            GUI.skin.button.font = font;
            GUI.skin.button.fontSize = 40;
            GUI.backgroundColor = Color.clear;
            


            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 50), "Continue"))
            {
                ToggleTimeScale();
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 10, 400, 50), "Back to Main Menu"))
            {
                ToggleTimeScale();
                sceneFader.SwitchScene(0);
            }
        }
    }

    private void ToggleTimeScale()
    {
        if (!isPaused)
        {
            player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
        }
        else
        {
            player.GetComponent<FirstPersonController>().enabled = true;
            Cursor.visible = false;
        }
        isPaused = !isPaused;
    }
}
