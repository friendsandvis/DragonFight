using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//this class servers to load all dragon tpes from file to memory
public class DragonLoader{

	public List<Dragon> dragons;
	private string datafilessubdirectory="/DataFiles/";

	// Use this for initialization
	public void startLoadingDragons() {
		dragons = new List<Dragon> ();
		loadDragons ("DragonData.txt");

		foreach (Dragon drag in dragons) {
			Debug.Log (drag.dragonname);
		}
	}


	private void loadDragons(string filename)
	{
		StreamReader filereader=null;
		FileInfo fileinf = null;

		fileinf = new FileInfo (Application.dataPath+datafilessubdirectory+filename);
		if (fileinf == null) {
			Debug.Log ("Error loading file:\t"+filename);
			return;
		}

		filereader = fileinf.OpenText ();
		if (filereader == null) {
			Debug.Log ("Error Reading file:\t"+filename);
			return;
		}


		string fileline=null;
		Dragon newdragon=null;
		while ((fileline = filereader.ReadLine ()) != null) {
			string name;string value;
			getStringValuePair (fileline,out name,out value);

			if (name.CompareTo ("Dragon") == 0)
				newdragon = new Dragon (0.0f);
			else if (name.CompareTo ("Health") == 0)
				newdragon.maxhealth = float.Parse (value);
			else if (name.CompareTo ("Name") == 0)
				newdragon.dragonname = value;
			else if (name.CompareTo ("EndDragon") == 0) {
				dragons.Add (newdragon);newdragon = null;
			}
		}
	}


	//utility function not very important
	public void getStringValuePair(string data,out string name,out string value)
	{
		int colonindex = -1;
		name = "";
		value = "";

		for(int z=0;z<data.Length;z++)
		{
			if (data [z] == ':') {
				colonindex = z;
				break;
			}
		}

		if (colonindex == -1)
			return;

		name = data.Substring (0, colonindex);
		value = data.Substring (colonindex+1,data.Length-(colonindex+1));
	}
}
