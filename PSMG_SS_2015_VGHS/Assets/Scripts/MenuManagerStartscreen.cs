using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManagerStartscreen : MonoBehaviour
{

    public Canvas exitMenu;
    public Button play;
    public Button options;
    public Button exit;
    public GameObject mainController;

    // Use this for initialization
    void Start()
    {
        exitMenu = exitMenu.GetComponent<Canvas>();
        play = play.GetComponent<Button>();
        options = options.GetComponent<Button>();
        exit = exit.GetComponent<Button>();
        exitMenu.enabled = false;
    }

    public void OnPlayButtonPressed()
    {
        mainController.GetComponent<StartScreenController>().ChangeLevel(1);
    }

    public void OnOptionsButtonPressed()
    {

    }

    public void OnExitButtonPressed()
    {
        play.enabled = false;
        options.enabled = false;
        exit.enabled = false;
        exitMenu.enabled = true;
    }

    public void OnYesButtonPressed()
    {
        Application.Quit();
    }

    public void OnNoButtonPressed()
    {
        play.enabled = true;
        options.enabled = true;
        exit.enabled = true;
        exitMenu.enabled = false;
    }
}
