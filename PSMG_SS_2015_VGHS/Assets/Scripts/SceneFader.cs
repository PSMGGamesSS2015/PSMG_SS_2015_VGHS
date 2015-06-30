using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour {

    public Texture2D texture;
    public float fadeSpeed = 2;

    int nextLevel = 1;
    Rect screenRect;
    Color currentColor;
    bool isStarting = true;
    bool isEnding = false;



	// Use this for initialization
	void Start () {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        currentColor = Color.black;
	}

    void Update()
    {
        if (isStarting)
        {
            FadeIn();
        }
        if (isEnding)
        {
            FadeOut();
        }
    }

    void OnGUI()
    {
        GUI.depth = 0;
        GUI.color = currentColor;
        GUI.DrawTexture(screenRect, texture, ScaleMode.StretchToFill);
    }

    void FadeIn()
    {
        currentColor = Color.Lerp(currentColor, Color.clear, fadeSpeed * Time.deltaTime);

        if (currentColor.a <= 0.05f)
        {
            currentColor = Color.clear;
            isStarting = false;
        }
    }

    void FadeOut()
    {
        currentColor = Color.Lerp(currentColor, Color.black, fadeSpeed * Time.deltaTime);

        if (currentColor.a >= 0.95f)
        {
            currentColor.a = 1;
            Application.LoadLevel(nextLevel);
        }
    }

    public void SwitchScene(int nextSceneIndex)
    {
        nextLevel = nextSceneIndex;
        isEnding = true;
        isStarting = false;
    }
}
