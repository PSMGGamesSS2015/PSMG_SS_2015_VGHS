using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* ITEM DATABASE
 * This class registers all ingame hints
 */
public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		items.Add (new Item ("dress", "a bloody dress", 0));
		items.Add (new Item ("note", "a strange note", 1));
		items.Add (new Item ("scar", "a scar on michaels head", 3));
		items.Add (new Item ("family", "Diane was my best friend", 4));
	}
}
