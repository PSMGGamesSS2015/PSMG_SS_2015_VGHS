using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/* MAIN CONTROLLER
 * This is the games main controlling Script. 
 * It receives event based data from other scripts (like where collisions are triggered).
 * Its task is to store and process this data and to communicate the results to other scripts.
 * So every logic of the game is handled here.
 * Interactions depending on the inventory like dragging & dropping hints are handled by the SlotScript and Inventory Script
 */

public class HausController : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    public void ChangeLevel(int level)
    {
        //Application.LoadLevel(level);
        GetComponent<SceneFader>().SwitchScene(level);
    }
}
