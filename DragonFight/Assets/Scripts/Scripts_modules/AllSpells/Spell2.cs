using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spell to reduce all dragon's health by 50
public class Spell2:Spell
{
	public static bool effectalldragons=true;

	public Spell2()
	{
		spellid = SpellID.POISIONBOMB;
		noofturnsforcooldown = 1;
	}
	public override void effect (Dragon dragon)
	{
		dragon.maxhealth -= getDragonEffect(dragon);
	}
}