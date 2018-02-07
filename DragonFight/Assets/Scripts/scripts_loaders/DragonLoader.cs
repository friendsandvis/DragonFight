using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//this class servers to load all dragons from file to memory
public class DragonLoader{

	//the attack manager is unique
	public static AttackManagers attackmanager;

	public List<Dragon> dragons;
	private string datafilessubdirectory="/DataFiles/";

	//constructor
	public DragonLoader()
	{
		dragons = new List<Dragon> ();
	}

	// Use this for initialization
	public void startLoadingDragons(string ddatafile) {
		dragons = new List<Dragon> ();
		loadDragons (ddatafile);

		foreach (Dragon drag in dragons) {
			Debug.Log (drag.dragontype);
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
		bool fetchdragon = false;


		while ((fileline = filereader.ReadLine ()) != null) {

			//get the name value pair
			string name;string value;
			getStringValuePair (fileline,out name,out value);

			//fetch a dragon from data
			if (name.CompareTo ("Dragon") == 0)
				dragonFetchRoutine (filereader);
			
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


	//dragon fetch routine
	public void dragonFetchRoutine(StreamReader filereader)
	{
		string fileline = null;
		Dragon newdragon = null;
		uint dataindex = 0;

		while ((fileline = filereader.ReadLine ()) != null) {

			//get the name value pair
			string name;
			string value;
			getStringValuePair (fileline, out name, out value);

			//break point(add dragon to list)
			if (name.CompareTo ("EndDragon") == 0) {
				dragons.Add (newdragon);
				return;
			}


			switch (dataindex) {
			//dragontype id
			case 0:
				DragonType dragtype = (DragonType)(uint)int.Parse (value);
				newdragon = getDragonShell (dragtype);
				break;

			//dragon movement
			case 1:
				newdragon.movementfredom = (uint)int.Parse (value);
				break;

			//dragon dragonlevel
			case 2:
				newdragon.draglevel = (uint)int.Parse (value);
				break;

			//dragon experience points
			case 3:
				newdragon.experiencescore = float.Parse (value);
				break;

			//dragon helath
			case 4:
				newdragon.maxhealth = float.Parse (value);
				break;

			//dragon attack
			case 5:
				newdragon.attack = float.Parse (value);
				break;

			//dragon defense
			case 6:
				newdragon.defense = float.Parse (value);
				break;

			//dragon dragonname
			case 7:
				newdragon.dragonname = value;
				break;

			//attacks
			case 8:
				//start attack load cycle
				if (name == "Attack") {
					newdragon.moves = new List<Attack> ();
					dataindex--;
					break;
				}
				//end attack load cycle
				if (name == "EndAttack")
					break;

				//load an attack
				AttackId attid = (AttackId)(uint)int.Parse (value);
				newdragon.moves.Add(attackmanager.getAttackCopy (attid));

				//prevent counter update
				dataindex--;
				break;
			
			default:
				Debug.Log ("Error in Dragon Data");
				break;
			}

			dataindex++;

		}
	}


	//get the respective dragon's attrib object as per the dragon choice index
	public Dragon getDragonShell(DragonType dragontype)
	{
		switch (dragontype) {
		case DragonType.BAHEMUTDRAGON:
			return new BahemutDragon ();
		case DragonType.SPEEDSTERDRAGON:
			return new SpeedSterDragon ();
		case DragonType.SEADRAGON:
			return new SeaDragon ();
		case DragonType.TIGERDRAGON:
			return new TigerDragon ();
		}
		return null;
	}
}
