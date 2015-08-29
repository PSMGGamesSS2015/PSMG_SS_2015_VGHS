using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

	public GameObject backgroundMusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		toggleBackgroundMusic();
	}

	// manage backgroundmusic
	void toggleBackgroundMusic(){
		switch (Application.loadedLevel) {
		case 0:

			break;
		case 1:

			break;
		case 2:

			break;
		}
	}
}
