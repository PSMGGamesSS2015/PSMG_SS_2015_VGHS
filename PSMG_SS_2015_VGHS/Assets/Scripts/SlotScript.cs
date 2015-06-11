using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerDownHandler{

	public Item item;
	Image itemImage;

	// Use this for initialization
	void Start () {
		itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	 	if (item != null) {
			itemImage.enabled = true;
			itemImage.sprite = item.itemIcon;
		} 
		else {
			itemImage.enabled = false;
		}
	}

	public void OnPointerDown(PointerEventData data){
		Debug.Log (transform.name);
	}
}
