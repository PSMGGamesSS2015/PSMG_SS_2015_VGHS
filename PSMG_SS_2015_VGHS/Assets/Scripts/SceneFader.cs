using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneFader : MonoBehaviour {

    public Texture2D texture;
    public float fadeSpeed = 2;
	public bool isFading = true;

    int nextLevel = 1;
    Rect screenRect;
    Color currentColor;
    bool isStarting = true;
    bool isEnding = false;



	// setup rectange  in-/ and outfadiing
	void Start () {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        currentColor = Color.black;
	}

	// handle fading here
    void Update(){
        if (isStarting){
            FadeIn();
        }
        if (isEnding){
            FadeOut();
        }
    }

	// update GUI
    void OnGUI(){
        GUI.depth = 0;
        GUI.color = currentColor;
        GUI.DrawTexture(screenRect, texture, ScaleMode.StretchToFill);
    }

	// fade in by fading rectangle out
    void FadeIn(){
        currentColor = Color.Lerp(currentColor, Color.clear, fadeSpeed * Time.deltaTime);

        if (currentColor.a <= 0.05f){
            currentColor = Color.clear;
            isStarting = false;
			isFading = false;
        }
    }

	// fade out by fading rectangle in
    void FadeOut(){
        currentColor = Color.Lerp(currentColor, Color.black, fadeSpeed * Time.deltaTime);
		isFading = true;
        if (currentColor.a >= 0.95f){
            currentColor.a = 1;
            Application.LoadLevel(nextLevel);
        }
    }

	// method for other classes to initialize level change
    public void SwitchScene(int nextSceneIndex){
        nextLevel = nextSceneIndex;
        isEnding = true;
        isStarting = false;
    }
}
