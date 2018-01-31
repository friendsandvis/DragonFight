using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all drgans(holds common attribs)

public enum DragonType:uint
{
	TIGERDRAGON=0u,SEADRAGON=1u,BAHEMUTDRAGON=2u,SPEEDSTERDRAGON=3u
};

[System.Serializable]			//serialization is for debugging
public class Dragon
{
	//basic attribs of any dragon
	public float maxhealth=0.0f;
	public float attack = 0.0f;
	public float defense = 0.0f;
	public uint movementfredom=0u;
	public DragonType dragontype;
	public List<Attack> moves;

	//experience points
	public uint draglevel=0;
	public float experiencescore=0;


	//default constructor not doing anything right now
	public Dragon()
	{
		
	}

	//copy constructor
	public Dragon(Dragon dragondata)
	{
		if (dragondata == null)
			return;

		this.maxhealth = dragondata.maxhealth;
		this.defense = dragondata.defense;
		this.attack = dragondata.attack;
		this.dragontype = dragondata.dragontype;
		this.movementfredom = dragondata.movementfredom;

		this.moves = new List<Attack> ();
		for (int z = 0; z < dragondata.moves.Count; z++)
			this.moves.Add (dragondata.moves [z]);
	}
		
	//helper fuction to add experience to dragons
	public void addExperienceScore(float expscore)
	{
		experiencescore += expscore;
	}
}
