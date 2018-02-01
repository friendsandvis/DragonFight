using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spell to reduce all dragon's health by 50
public class Spell3:Spell
{

	public Spell3()
	{
		spellid = SpellID.HEALTHUP;
		noofturnsforcooldown = 2u;
		effectall = true;
		effecteddragoncount = 0u;
		selfeffecting = true;
	}
	public override void effect (Dragon dragon)
	{
		dragon.maxhealth += getDragonEffect(dragon);
	}

	protected override float getDragonEffect(Dragon dragon)
	{
		return 10.0f;
	}
}