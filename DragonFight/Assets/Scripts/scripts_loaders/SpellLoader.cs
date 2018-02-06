using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpellLoader {

	public List<PlayersSpell> spells;
	private string datafilessubdirectory="/DataFiles/";

	//constructor
	public SpellLoader()
	{
		spells = new List<PlayersSpell> ();
	}

	// Use this for initialization
	public void startLoadingSpells(string sdatafile) {
		spells = new List<PlayersSpell> ();
		loadSpells (sdatafile);

		foreach (PlayersSpell spell in spells) {
			Debug.Log (spell.spellid);
		}
	}


	private void loadSpells(string filename)
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
		Spell newspell=null;
		bool fetchspell = false;


		while ((fileline = filereader.ReadLine ()) != null) {

			//get the name value pair
			string name;string value;
			getStringValuePair (fileline,out name,out value);

			//fetch a dragon from data
			if (name.CompareTo ("Spell") == 0)
				SpellFetchRoutine (filereader);

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
	public void SpellFetchRoutine(StreamReader filereader)
	{
		string fileline = null;
		Spell newspell = null;
		uint dataindex = 0;

		while ((fileline = filereader.ReadLine ()) != null) {

			//get the name value pair
			string name;
			string value;
			getStringValuePair (fileline, out name, out value);

			//break point(add dragon to list)
			if (name.CompareTo ("EndSpell") == 0) {

				//as a s new players spell object which has some gameplay data
				PlayersSpell spell = new PlayersSpell (newspell.spellid);
				spell.spellobj = newspell;
				spells.Add (spell);
				return;
			}


			switch (dataindex) {
			//spell basic id
			case 0:
				SpellIDBasic spellbasictype = (SpellIDBasic)(uint)int.Parse (value);
				newspell = getSpellShell (spellbasictype);
				break;

				//spell id
			case 1:
				newspell.spellid = (SpellID)(uint)int.Parse (value);
				break;

				//cool down
			case 2:
				newspell.noofturnsforcooldown = (uint)int.Parse (value);
				break;

				//spell exp points
			case 3:
				newspell.spellexppoints = float.Parse (value);
				break;

				//effectvalue
			case 4:
				newspell.effectvalue = float.Parse (value);
				break;

				//turn effecting
			case 5:
				newspell.isnturneffect = (1==int.Parse (value));
				break;

				//effect all
			case 6:
				newspell.effectall = (1==int.Parse (value));
				break;

				//self effecting
			case 7:
				newspell.selfeffecting = (1==int.Parse (value));
				break;

				//effected dragon count
			case 8:
				newspell.effecteddragoncount = (uint)int.Parse (value);
				break;

			default:
				Debug.Log ("Error in Dragon Data");
				break;
			}

			dataindex++;

		}
	}


	//get the respective dragon's attrib object as per the dragon choice index
	public Spell getSpellShell(SpellIDBasic spellidbasic)
	{
		switch (spellidbasic) {
		case SpellIDBasic.HEALTHEFFECTING:
			return new Spell_HealthEffecting ();
		}
		return null;
	}

}
