using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TigerDragon: Dragon 
{
	public float specialdefense;


	public TigerDragon()
	{
		specialdefense = 50.0f;
		defense = 10.0f;
		attack = 20.0f;
		maxhealth = 100.0f;
		movementfredom = 1u;
		dragontype=DragonType.TIGERDRAGON;

	}
}
