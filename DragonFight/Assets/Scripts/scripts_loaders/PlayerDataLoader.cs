using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataLoader {

	private static string datafilessubdirectory="/DataFiles/";

	public static void getPlayerData(out List<PlayersDragon> playersdragon,out List<PlayersSpell> playerspell,string filename)
	{
		playersdragon = null;
		playerspell = null;
		StreamReader effectreader = null;
		FileInfo spelleffectfile = new FileInfo (Application.dataPath+datafilessubdirectory+filename);

		if (spelleffectfile == null) {
			Debug.Log ("Error loading file:\t"+filename);
			return;
		}

		effectreader = spelleffectfile.OpenText ();
		if (effectreader == null) {
			Debug.Log ("Error Reading file:\t"+filename);
			return;
		}


		//start reading
		string redline=null;

		while ((redline = effectreader.ReadLine ()) != null) {
			
			redline.Trim ();

			if (redline == "" || redline [0] != 'i')
				continue;

			switch (redline [1]) {
			case 's':
				playerspell = getPlayersSpells (redline.Substring (2, redline.Length - 2));
				break;
			case 'd':
				playersdragon = getPlayersDragons (redline.Substring (2, redline.Length - 2));
				break;
			}
		}
	}


	private static List<PlayersDragon> getPlayersDragons(string data)
	{
		string dragoncountpair = "";
		List<PlayersDragon> playersdragons=new List<PlayersDragon>();

		foreach (char ch in data) {
			switch (ch) {
				
			case ',':
				dragoncountpair.Trim ();
				playersdragons.Add (getPlayerDragon (dragoncountpair));
				dragoncountpair = "";
				break;

			default:
				dragoncountpair += ch;
				break;

			}
		}

		return playersdragons;
	}

	private static PlayersDragon getPlayerDragon(string data)
	{
		data = data.Trim ();
		DragonType dragtype=DragonType.SEADRAGON;
		uint count = 0;
		string util="";
		data+=' ';


		foreach (char ch in data) {
			switch (ch) {

			case '-':
				util=util.Trim ();
				dragtype = (DragonType)(uint)int.Parse (util);
				util = "";
				break;

			case ' ':
				util = util.Trim ();
				count = (uint)int.Parse (util);
				util = "";
				break;

			default:
				util += ch;
				break;

			}
		}
			
		return (new PlayersDragon (dragtype, count));
	}


	private static List<PlayersSpell> getPlayersSpells(string data)
	{
		string spellid = "";
		List<PlayersSpell> playerspells=new List<PlayersSpell>();

		foreach (char ch in data) {
			switch (ch) {

			case ',':
				spellid.Trim ();
				playerspells.Add (new PlayersSpell ((SpellID)(uint)int.Parse (spellid)));
				spellid = "";
				break;

			default:
				spellid += ch;
				break;

			}
		}

		return playerspells;
	}

}
