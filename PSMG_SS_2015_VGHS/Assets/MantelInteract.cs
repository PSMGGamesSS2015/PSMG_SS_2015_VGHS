using UnityEngine;
using System.Collections;

public class MantelInteract : MonoBehaviour
{
    bool insideCollider = false;
    void Update()
    {
        if (insideCollider == true && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
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

        if (insideCollider)
        {
            GUI.Box(new Rect(10, 10, 150, 25), "Press \"E\" to interact!");
        }
    }
}
