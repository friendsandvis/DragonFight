using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AttckLoader {
	public List<Attack> attacks;
	private string datafilessubdirectory="/DataFiles/";

	//constructor
	public AttckLoader()
	{
		attacks = new List<Attack> ();
	}

	// Use this for initialization
	public void startLoadingAttacks(string ddatafile) {
		attacks = new List<Attack> ();
		loadAttacks (ddatafile);

		foreach (Attack attack in attacks) {
			Debug.Log (attack.attackid);
		}
	}


	private void loadAttacks(string filename)
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
		Attack newattack=null;
		bool fetchattack = false;


		while ((fileline = filereader.ReadLine ()) != null) {

			//get the name value pair
			string name;string value;
			getStringValuePair (fileline,out name,out value);

			//fetch a dragon from data
			if (name.CompareTo ("Attack") == 0)
				AttackFetchRoutine (filereader);

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
	public void AttackFetchRoutine(StreamReader filereader)
	{
		string fileline = null;
		Attack newattack = null;
		uint dataindex = 0;

		while ((fileline = filereader.ReadLine ()) != null) {

			//get the name value pair
			string name;
			string value;
			getStringValuePair (fileline, out name, out value);

			//break point(add dragon to list)
			if (name.CompareTo ("EndAttack") == 0) {
				attacks.Add (newattack);
				return;
			}


			switch (dataindex) {
			//attack id
			case 0:
				AttackId attacktype = (AttackId)(uint)int.Parse (value);
				newattack = getAttackShell (attacktype);
				break;

				//attack damage
			case 1:
				newattack.damagedone = float.Parse (value);
				break;

				//range
			case 2:
				newattack.range = (uint)int.Parse (value);
				break;

				//self attaking?
			case 3:
				newattack.selfattacking = (0==int.Parse (value));
				break;

				//effectall?
			case 4:
				newattack.effectall =(0==int.Parse (value));
				break;

				//effected dragon count
			case 5:
				newattack.effecteddragoncount = (uint)int.Parse (value);
				break;

			default:
				Debug.Log ("Error in Attack Data");
				break;
			}

			dataindex++;

		}
	}


	//get the respective dragon's attrib object as per the dragon choice index
	public Attack getAttackShell(AttackId attacktype)
	{
		switch (attacktype) {
		case AttackId.FIREBREADTH:
			return new FireBredth (0.0f);
		case AttackId.TAILWHIP:
			return new TailSwipe (0.0f);
		}
		return null;
	}
}
