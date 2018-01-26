using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spell to reduce all dragon's health by 50
public class Spell2:Spell
{

	public Spell2()
	{
		spellid = SpellID.POISIONBOMB;
		noofturnsforcooldown = 1;
		effectall = true;
		effecteddragoncount = 0u;
		selfeffecting = false;
	}
	public override void effect (Dragon dragon)
	{
		dragon.maxhealth -= getDragonEffect(dragon);
	}
}