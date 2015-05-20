using UnityEngine;
using System.Collections;

public class WaschbeckenInteract : MonoBehaviour
{
    bool insideCollider = false;
    bool mirror = false;
    void Update()
    {
        if (insideCollider == true && Input.GetKeyDown(KeyCode.E))
        {
            mirror = true;
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
            mirror = false;
        }
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "My Text");

        if (insideCollider)
        {
            GUI.Box(new Rect(10, 10, 150, 25), "Press \"E\" to interact!");
            if (mirror)
            {
                GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 12, 300, 25), "You interacted with the mirror!");
            }
        }
    }
}