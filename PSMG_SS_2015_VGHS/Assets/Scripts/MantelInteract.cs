using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MantelInteract : MonoBehaviour
{
    bool insideCollider = false;
    GameObject mantelBadewanne;
    bool interacted = false;

    void Start() {
        mantelBadewanne = GameObject.Find("Mantel_ueber_Badewanne");
    }
    void Update()
    {
        if (insideCollider == true && Input.GetKeyDown(KeyCode.E) && interacted == false)
        {
            interacted = true;
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
        //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "My Text");

        if (insideCollider && interacted == false)
        {
            GUI.Box(new Rect(10, 10, 150, 25), "Press \"E\" to interact!");
        }

        if (insideCollider && interacted)
        {
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
            GUI.Box(new Rect(Screen.width / 2 - 550, Screen.height / 2 - 12, 1100, 25), "Pat Rutherford. Z.31. 12:30 Uhr… Wer ist Pat Rutherford? War ich um 12:30 bei dieser oder diesem Pat? War das heute? Was war heute? Was war gestern? WER BIN ICH? (press 'Space' to go on)");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
                GameObject.Find("MantelTrigger").GetComponent<SphereCollider>().enabled = false;
                insideCollider = false;
            }
        }
    }
}
