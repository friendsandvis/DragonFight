using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SeaDragon: Dragon 
{
	public float specialattack;


	public SeaDragon()
	{
		specialattack = 10.0f;
		defense = 20.0f;
		attack = 30.0f;
		maxhealth = 50.0f;
		movementfredom = 1u;
		dragontype=DragonType.SEADRAGON;

	}
}
