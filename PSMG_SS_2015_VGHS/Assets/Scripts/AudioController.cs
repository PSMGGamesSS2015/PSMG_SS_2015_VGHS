using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public GameObject backgroundMusic;
	public GameObject voiceOutput;

	public Dictionary<string, string> keyAudioList = new Dictionary<string, string>();


	// Use this for initialization
	void Start () {
		setupKeyAudioList ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setupVoice(string key){
		string audioSource;
		keyAudioList.TryGetValue (key, out audioSource);
		AudioClip clip = Resources.Load ("Audio/Voice/"+audioSource)as AudioClip;

		voiceOutput.GetComponent<AudioSource> ().clip = clip;
		voiceOutput.GetComponent<AudioSource>().Play();
	}

	void setupKeyAudioList(){
		keyAudioList.Add("entry", "jane_bath_1");
		keyAudioList.Add("mirror1", "jane_bath_2");
		keyAudioList.Add("mirror2", "jane_bath_3");
		keyAudioList.Add("paper", "jane_bath_4");
		keyAudioList.Add("theory1", "jane_bath_5");
	}
}
