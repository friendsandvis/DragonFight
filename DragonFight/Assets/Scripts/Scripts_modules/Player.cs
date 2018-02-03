using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSpell
{
	public SpellID spellid;
	public uint turnsuntilavailable;

	public PlayersSpell(SpellID spellid)
	{
		this.spellid=spellid;
		turnsuntilavailable=0u;
	}

}

public class PlayersDragon
{
	public DragonType dragtype;
	public uint noofdragonsavailable;

	public PlayersDragon(DragonType dragtype,uint noofdrags)
	{
		this.dragtype=dragtype;
		noofdragonsavailable=noofdrags;
	}

}




//holds comlete data related to a specific player
public class Player {
	
	private class PlateIndex
	{
		uint x,y;
		public PlateIndex(uint x,uint y)
		{
			this.x=x; this.y=y;
		}

		public bool equalsTo(PlateIndex index)
		{
			return(index.x == x && index.y == y);
		}
	};


	uint playerindex;

	//transforms for the dragons
	public Quaternion dragonrotation;

	//spawn plates(array of 2)
	List<PlateIndex> spawnplates;

	//player Experience data
	uint experiencelevel;
	float experiencescore;

	//List of all spells available   as spell-reloading_turns pair
	List<PlayersSpell> playerspells;

	//List of dragons available with player as dragonid-noofdragon pair
	List<Dragon> playersdragons;

	//Load and store Spells



	public Player(uint playerindex,string playerdatafile,string playerdragondatafile)
	{
		this.playerindex = playerindex;
		spawnplates = new List<PlateIndex>();
		dragonrotation = Quaternion.Euler (new Vector3 (0.0f,0.0f,0.0f ));

		//set the player data
		PlayerDataLoader.getPlayerData (out playerspells, playerdatafile);

		//player dragon data
		DragonLoader playerdragonloader=new DragonLoader();
		playerdragonloader.startLoadingDragons (playerdragondatafile);

		//retrive the copy of dragons
		playersdragons = playerdragonloader.dragons;
	}


	public void addDragonToPlayer(Dragon_GameController dragon)
	{
		dragon.gameObject.transform.rotation=dragonrotation;
		dragon.setDragonsPlayer (playerindex);
	}


	public void addSpawnPlate(uint x,uint y)
	{
		spawnplates.Add (new PlateIndex(x,y));
	}


	public bool isPlateASpawn(uint x,uint y)
	{
		Debug.Log (x+"  "+y);
		foreach(PlateIndex plateindex in spawnplates)
		{
			if (plateindex.equalsTo (new PlateIndex (x, y)))
				return true;
		}

		return false;
	}


	public bool isSpellReadyForUse(SpellID spellid)
	{
		foreach (PlayersSpell pspell in playerspells) {
			if (pspell.spellid == spellid)
				return (pspell.turnsuntilavailable == 0u);
		}
		return false;
	}

	//the +1 is because the current turn is also counted (update of cooldown is very last of updates)
	public void setCoolDown(SpellID  spellid,uint cooldown)
	{
		foreach (PlayersSpell pspell in playerspells) {
			if (pspell.spellid == spellid)
				pspell.turnsuntilavailable = cooldown+1;
		}
	}


	//Decrement player psell Cooldown if needed
	public void decrementCoolDown(uint decrement)
	{
		foreach (PlayersSpell pspell in playerspells) {
			if (pspell.turnsuntilavailable > 0u)
			{
				pspell.turnsuntilavailable -= decrement;
			}
		}
	}
		

	//get names as a list for all the names of dragons with player
	public List<string> getPlayerDragonNames()
	{
		List<string> data=new List<string>();

		foreach (Dragon drag in playersdragons) {
			data.Add (drag.dragonname);
		}

		return data;
	}

	public Dragon getDragonAt(int index)
	{
		if(index>playersdragons.Count-1)
			return null;

		return playersdragons [index];
	}
}

