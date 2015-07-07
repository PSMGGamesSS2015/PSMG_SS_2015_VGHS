using UnityEngine;
using System.Collections;

/* GAME CONTROLLER
 * This Controller is managing anything that happens all over the Game.
 * It communicates with the Controllers of all Scenes and manages the communication with the UI.
 */
public class GameController {
	int scene;

	//constructor that recieves current scene by initialisation
	public GameController(int sceneNum){
		scene = sceneNum;
		whatScene();
	}

	void whatScene(){
		Debug.Log (scene);
	}


}
