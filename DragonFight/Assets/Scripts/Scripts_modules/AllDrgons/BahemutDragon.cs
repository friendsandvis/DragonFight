using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BahemutDragon: Dragon 
{
	public float superspecialdefense;


	public BahemutDragon()
	{
		superspecialdefense = 150.0f;
		defense = 50.0f;
		attack = 10.0f;
		maxhealth = 1000.0f;
		movementfredom = 1u;
		dragontype=DragonType.BAHEMUTDRAGON;

		//set moves
		moves=new List<Attack>();
		moves.Add(new TailSwipe(20.0f));
	}
}
