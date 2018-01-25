using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spell to reduce all dragon's health by 5
public class Spell1:Spell
{
	public static bool effectalldragons=false;

	public Spell1()
	{
		spellid = SpellID.POISIONARRAOW;
		noofturnsforcooldown = 0;
	}

	public override void effect (Dragon dragon)
	{
		dragon.maxhealth -= getDragonEffect(dragon);
	}

	protected override float getDragonEffect(Dragon dragon)
	{
		return 5.0f;
	}
}
