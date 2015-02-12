using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MyFileWritingClass : MonoBehaviour {
	public class Item {
		public string name;
		public string description;

		public Item(string n, string d){
			this.name = n;
			this.description = d;
		}
	}
	List<Item> items = new List<Item>();


	void Start(){
		//WriteList ();
		ReadList();
		string savedString = ReadListFromFile();
		//string savedString = SaveList ();

		LoadList (savedString);
		ReadList ();
		LoadAllItems();


	}

	string ReadListFromFile(){
		items.Clear ();
		string fileName = "itemListFile.txt";
		StreamReader sr = File.OpenText (fileName);
		string s = sr.ReadLine ();
		Debug.Log ("itemListFile.txt has the following: " + s);
		return s;
	}

	void WriteList(){
		Item i = new Item("1.0,1.111,1.30", "Red");
		items.Add (i);
		i = new Item("1,2,1", "Blue");
		items.Add (i);
		i = new Item("0,0,0", "Green");
		items.Add (i);
	}

	void LoadList(string loadString){
		string[] itemSet = loadString.Split('/');
		Debug.Log ("____________");
		foreach(string i in itemSet){

			if(i.Length<=0) return;

			string[] subItemSet = i.Split('_');

			//subItemSet = ["1,1,1", "Red"]
			//1,1,1 IS 0 ELEMENT
			//RED IS 1 ELEMENT

			string currentName = subItemSet[0];
			string currentDescription = subItemSet[1];

			Item justinIsCool = new Item(currentName, currentDescription);
			items.Add (justinIsCool);
		}


	}

	void LoadAllItems(){
		foreach(Item i in items){
			string name = i.name; //actually this is a location
			string description = i.description; //actually this is a name

			string[] xyz = name.Split(',');
			float x = float.Parse(xyz[0]);
			float y = float.Parse(xyz[1]);
			float z = float.Parse(xyz[2]);
			Vector3 loc = new Vector3(x,y,z);

			GameObject instanceItem = (GameObject) Instantiate (Resources.Load (description), loc, Quaternion.identity);
		}
	}

	void ReadList(){
		foreach(Item i in items){
			Debug.Log (i.name + ", " + i.description);
		}
	}

	string SaveList(){ //save the list to a local file
		string writeToFileString = "";

		/* in case you get too many lines for one string 
			List<string> writeToFileStrings = new List<string>();
			int currentItem = 0;
		*/

		foreach(Item i in items){
			/* in case you get too many locations for one string 
				if(writeToFileStrings[currentItem].Length > 100000){
					currentItem+=1;
					writeToFileStrings.Add ("");
				}
				writeToFileStrings[currentItem] += "line of content";
			*/
			writeToFileString += i.name + "_" + i.description + "/";
		}
		Debug.Log (writeToFileString);
		//s.Split("," 0);

		string fileName = "itemListFile.txt";
		StreamWriter sw = File.CreateText (fileName);
		sw.WriteLine(writeToFileString);
		sw.Close ();

		return writeToFileString;
	}
}
