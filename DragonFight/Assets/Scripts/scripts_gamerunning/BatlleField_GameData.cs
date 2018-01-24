using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField_GameData{
	public Dragon[,] fielddragons;
	private int x,y;

	public BattleField_GameData(int nooftilesv,int nooftilesh)
	{
		x = nooftilesv;y = nooftilesh;
		fielddragons = new Dragon[nooftilesv, nooftilesh];
	}

	public void SetDragon(int x,int y,Dragon dragon)
	{
		
		//checking if the index is well within range
		if (x >= 0 && y >= 0 && x < this.x && y < this.y) 
		{
			//if occupied then show message
			if (fielddragons [x, y] == null)
				fielddragons [x, y] = dragon;
			else
				Debug.Log ("Will Not overwrite dragon at same place");
		}
		 else
			Debug.Log ("BattleField_GameData: Index Out of Bound");
	}


	public List<Dragon> getAllDragon()
	{
		List<Dragon> dragons = new List<Dragon> ();

		for (int i = 0; i < x; i++)
			for (int i1 = 0; i1 < y; i1++) {
				if (fielddragons [i, i1] != null)
					dragons.Add (fielddragons [i,i1]);
			}

		return dragons;
	}

	public Dragon getDragon(int x,int y)
	{
		return fielddragons[x,y];
	}
}
