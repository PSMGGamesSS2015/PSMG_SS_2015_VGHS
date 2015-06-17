

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* GUI CONTROLLER
 * Handles anything that is needed to be shown on the canvas.
 */
public class GUIController : MonoBehaviour
{

    public GameObject subtitleObject;
    public GameObject interactionHintObject;
    public GameObject inventoryObject;
    public GameObject inventoryHint;

    bool subtlIsShown;
    bool sinkIsActive;
    int sinkCounter = 0;

    //show hint for Inventory
    public void showInventoryHint()
    {
        inventoryHint.SetActive(true);
    }

    //show a hint for possible 'E' interactions
    public void showInteractionHint()
    {
        interactionHintObject.SetActive(true);
    }

    //hide interaction hint for 
    public void unshowInteractionHint()
    {
        interactionHintObject.SetActive(false);
    }

    // show a subtitle on the GUI
    public void showSubtl(string key)
    {
        subtitleObject.SetActive(true);
        subtitleObject.GetComponent<Subtitle>().setKeyWord(key);
        subtlIsShown = true;
    }

    //disable subtitle on GUI
    public void unshowSubtl()
    {
        subtitleObject.SetActive(false);
        subtlIsShown = false;
    }

    //method that makes it able to check if an subtitle is shown currently
    public bool isShowing()
    {
        return subtlIsShown;
    }
}