using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public GameObject slots;

	public int slotNumX;
	public int slotNumY;
	public int slotDistX;
	public int slotDistY;
	public int x;
	public int y;
	public int xReset;


	// Use this for initialization
	void Start () {
		for(int i = 0; i < slotNumY; i++){
			for(int k = 0; k < slotNumX; k++){
				GameObject slot = (GameObject) Instantiate(slots);
				slot.transform.parent = this.gameObject.transform;
				slot.GetComponent<RectTransform>().localPosition = new Vector3 (x, y, 0);
				slot.name = "Slot"+i+"."+k;
				x += slotDistX;
				if(k == slotNumX-1){
					x = xReset;
					y -= slotDistY;
				}
			}
		}
	}
}
