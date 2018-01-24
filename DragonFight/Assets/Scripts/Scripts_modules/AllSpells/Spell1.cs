using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spell to reduce all dragon's health by 5
public class Spell1:Spell
{
	public static bool effectalldragons=false;

	public Spell1()
	{
		name = "PoisionArrow";
		noofturnsforcooldown = 0;
	}
	public override void effect (Dragon dragon)
	{
		dragon.maxhealth -= 5.0f;
	}
}
