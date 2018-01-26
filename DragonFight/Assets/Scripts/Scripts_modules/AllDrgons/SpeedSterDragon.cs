using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SpeedSterDragon: Dragon 
{
	public float speedup;


	public SpeedSterDragon()
	{
		speedup = 50.0f;
		defense = 5.0f;
		attack = 50.0f;
		maxhealth = 10.0f;
		movementfredom = 2u;
		dragontype=DragonType.SPEEDSTERDRAGON;

		//add moves
		moves=new List<Attack>();
		moves.Add(new FireBredth(2.0f));
	}
}
