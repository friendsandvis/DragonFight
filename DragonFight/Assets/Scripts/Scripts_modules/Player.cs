using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

	public Player(uint playerindex)
	{
		this.playerindex = playerindex;
		spawnplates = new List<PlateIndex>();
		dragonrotation = Quaternion.Euler (new Vector3 (0.0f,0.0f,0.0f ));
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
}
