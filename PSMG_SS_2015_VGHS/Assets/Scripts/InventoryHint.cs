using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryHint : MonoBehaviour {

	public Text hint;

	
	
	/* Update is called once per frame
	 * Change alpha and hide
	 */
	void Update () {
		if (gameObject.activeSelf) {
			gameObject.GetComponent<Image>().CrossFadeAlpha(0f, 1.5f, true);
			hint.GetComponent<Text>().CrossFadeAlpha(0f, 1.5f, true);
		}
		if (gameObject.GetComponent<Image> ().canvasRenderer.GetAlpha () < 0.1f) {
			gameObject.GetComponent<Image>().canvasRenderer.SetAlpha(255);
			hint.GetComponent<Text>().canvasRenderer.SetAlpha(255);
			gameObject.SetActive(false);
		}

	}


}
