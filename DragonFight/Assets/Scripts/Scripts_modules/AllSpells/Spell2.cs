using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spell to reduce all dragon's health by 50
public class Spell2:Spell
{
	public static bool effectalldragons=true;

	public Spell2()
	{
		name = "PoisionBomb";
		noofturnsforcooldown = 1;
	}
	public override void effect (Dragon dragon)
	{
		dragon.maxhealth -= getDragonEffect(dragon);
	}

	private float getDragonEffect(Dragon dragon)
	{
		switch (dragon.dragontype) {
		case DragonType.SEADRAGON:
			return 50.0f;
		default:
			return 10.0f;
		}
	}
}