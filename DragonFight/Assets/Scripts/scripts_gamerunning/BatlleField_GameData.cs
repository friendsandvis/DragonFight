using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField_GameData{
	
	public Dragon_GameController[,] fielddragons;
	private int x,y;

	public BattleField_GameData(int nooftilesv,int nooftilesh)
	{
		x = nooftilesv;y = nooftilesh;
		fielddragons = new Dragon_GameController[nooftilesv, nooftilesh];
	}

	public void SetDragon(int x,int y,Dragon_GameController dragon)
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
					dragons.Add (fielddragons [i,i1].dragonattribs);
			}

		return dragons;
	}


	public List<Dragon> getAllDragonOfPlayer(uint playerindex)
	{
		List<Dragon> dragons = new List<Dragon> ();

		for (int i = 0; i < x; i++)
			for (int i1 = 0; i1 < y; i1++) {
				if (fielddragons [i, i1] != null && fielddragons[i,i1].belongingplayerindex==playerindex)
					dragons.Add (fielddragons [i,i1].dragonattribs);
			}

		return dragons;
	}

	public List<Dragon> getAllDragonNotOfPlayer(uint playerindex)
	{
		List<Dragon> dragons = new List<Dragon> ();

		for (int i = 0; i < x; i++)
			for (int i1 = 0; i1 < y; i1++) {
				if (fielddragons [i, i1] != null && fielddragons[i,i1].belongingplayerindex!=playerindex)
					dragons.Add (fielddragons [i,i1].dragonattribs);
			}

		return dragons;
	}

	public List<Dragon> getAllDragonNotOfPlayer(uint playerindex,int x,int y,int range)
	{
		//TO DO

		return null;
	}


	public Dragon getDragon(int x,int y)
	{
		if (fielddragons [x, y] == null)
			return null;
		
		return fielddragons[x,y].dragonattribs;
	}

	public Dragon getDragonOfPlayer(int x,int y,uint playerindex)
	{
		if (fielddragons [x, y] == null || fielddragons [x, y].belongingplayerindex!=playerindex)
			return null;

		return fielddragons[x,y].dragonattribs;
	}

	public Dragon getDragonNotOfPlayer(int x,int y,uint playerindex)
	{
		if (fielddragons [x, y] == null || fielddragons [x, y].belongingplayerindex==playerindex)
			return null;

		return fielddragons[x,y].dragonattribs;
	}

	public Dragon_GameController getDragonController(int x,int y)
	{
		return fielddragons[x,y];
	}

	public bool moveDragon(int prevx,int prevy,int newx,int newy,Vector3 newdragonposition)
	{
		Dragon_GameController dcontroller = getDragonController ((int)prevx,(int)prevy);

		//calculate a max range of motion for the given dragon
		int minx=prevx-(int)dcontroller.dragonattribs.movementfredom;
		int maxx=prevx+(int)dcontroller.dragonattribs.movementfredom;
		int miny=prevy-(int)dcontroller.dragonattribs.movementfredom;
		int maxy=prevy+(int)dcontroller.dragonattribs.movementfredom;


		//check if the given new pos is well within range
		if (!(newx >= minx && newx <= maxx && newy >= miny && newy <= maxy))
			return false;


		//reposition dragon in matrix and in its transforms
		fielddragons[newx,newy]=dcontroller;
		fielddragons[prevx,prevy]=null;

		Transform dtransform = dcontroller.gameObject.transform;

		dtransform.position = newdragonposition;

		return true;
	}
}
