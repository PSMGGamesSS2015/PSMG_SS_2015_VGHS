using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class WaschbeckenInteract : MonoBehaviour
{
	int interactTime = 0;
    bool insideCollider = false;
    GameObject mantel;
    GameObject mantelAn;
    GameObject mantelTrigger;
	public Canvas inventory;

    void Start()
    {
        mantel = GameObject.Find("Mantel_ueber_Badewanne");
        mantelAn = GameObject.Find("Mantel");
        mantelTrigger = GameObject.Find("MantelTrigger");
        mantelTrigger.GetComponent<SphereCollider>().enabled = false;
		inventory.enabled = false;



    }


    void Update()
    {
		if (Input.GetKeyDown (KeyCode.I)) {
			if(inventory.enabled == false){
				inventory.enabled = true;
				GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
			}
			else{
				inventory.enabled = false;
				GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
			}
		}

		//react to interaction with sink
		if (insideCollider == true && Input.GetKeyDown(KeyCode.E))
		{
			switch (interactTime){
			case 0: 
				interactTime = 1;
				break;
			case 2:
				interactTime = 3;
				break;

			default: break;
			}
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "PlayerCharacter")
        {
            insideCollider = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
		if (other.tag == "PlayerCharacter")
		{
			insideCollider = false;
		}
	}
	
	void OnGUI()
	{
        
		//show possible interaction when colliding with sink
        if (insideCollider && (interactTime == 0 || interactTime == 2)) 
		{

			GUI.Box (new Rect (10, 10, 150, 25), "Press \"E\" to interact!");
		}

		//react to first interaction
		if (interactTime == 1) 
		{
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
			Time.timeScale = 0;
			GUI.Box(new Rect(Screen.width / 2 - 275, Screen.height / 2 - 27, 550, 55), "Okay, ganz ruhig bleiben. Jetzt bloß keine Panik. Wer bist du, Frau im Spiegel? Bist du ich? \n \nVerdammt warum erkenne ich dich nicht?! Mir ist so heiß... (press 'Space' to go on)");
			if(Input.GetKeyDown(KeyCode.Space))
			{
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
				Time.timeScale = 1;
				setupJacket();
				interactTime = 2;
			}
		}

		//react to second interaction
		if (interactTime == 3) 
		{
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
			Time.timeScale = 0;
			GUI.Box(new Rect(Screen.width / 2 - 275, Screen.height / 2 - 27, 550, 55), "Was zum…?! O-Oh mein… Nein, das ist… das ist Blut!!! Nicht mein Blut! \n \nWas ist denn hier los - oh Gott was habe ich getan?! (press 'Space' to go on)");
			if(Input.GetKeyDown(KeyCode.Space))
			{
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
				Time.timeScale = 1;
				interactTime = 4;
			}
		}
    }

	//handle taking off and rendering the jacket
	void setupJacket()
	{
        mantelAn.GetComponent<MeshRenderer>().enabled = false;
        mantel.GetComponent<MeshRenderer>().enabled = true;
        mantelTrigger.GetComponent<SphereCollider>().enabled = true;
	}



}