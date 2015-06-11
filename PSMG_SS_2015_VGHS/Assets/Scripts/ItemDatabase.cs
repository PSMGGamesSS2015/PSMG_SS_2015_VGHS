using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		items.Add (new Item ("dress", "a bloody dress", 0));
		items.Add (new Item ("note", "a strange note", 1));
	}
	

}
