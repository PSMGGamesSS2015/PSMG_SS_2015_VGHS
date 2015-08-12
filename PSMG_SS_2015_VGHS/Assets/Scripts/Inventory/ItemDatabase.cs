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
		items.Add (new Item ("daughter", "I have a daughter named Emily. She is 7 years old", 5));
		items.Add (new Item ("picture", "Emilies picture of first class", 6));
		items.Add (new Item ("dianesDaughter", "Emilies picture of first grade", 7));
		items.Add (new Item ("missingPicture", "Picture of second grade missing", 8));
		items.Add (new Item ("emilyWhereabout", "Emily is currently in Woods Hole", 9));
		items.Add (new Item ("pills", "Dr. Meloff prescribed pills", 10));
	}
}
