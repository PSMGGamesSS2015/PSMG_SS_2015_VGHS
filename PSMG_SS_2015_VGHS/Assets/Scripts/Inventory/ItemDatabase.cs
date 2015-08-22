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
		items.Add (new Item ("dress", "Blutiges Kleid", 0));
		items.Add (new Item ("note", "Seltsame Notiz mit dem Namen 'Pat Rutherford'", 1));
		items.Add (new Item ("scar", "Michael trägt eine Narbe am Kopf", 3));
		items.Add (new Item ("family", "Überraschungsbesuch beim Bruder", 4));
		items.Add (new Item ("daughter", "Tochter Emily ist 7 Jahre alt", 5));
		items.Add (new Item ("picture", "Emily in der ersten Klasse", 6));
		items.Add (new Item ("missingPicture", "Bild von der zweiten Klasse fehlt", 8));
		items.Add (new Item ("pills", "Dr. Meloff hat Tabletten verschrieben", 10));
		items.Add (new Item ("crash", "Mutter verstarb durch einen Autounfall", 12));
		items.Add (new Item ("dizzy", "Schwindelgefühle", 13));
		items.Add (new Item ("nightmares", "Paulas Tochter hat Alpträume", 14));
		items.Add (new Item ("amnesia", "Seit dem 24. Juni keine Erinnerungen mehr", 15));
		items.Add (new Item ("personalStuff", "Persönliche Sachen befanden sich in einer Box", 17));
		items.Add (new Item ("death", "Meine Mutter und meine Tochter verstarben am 23rd June", 18));
	}
}
