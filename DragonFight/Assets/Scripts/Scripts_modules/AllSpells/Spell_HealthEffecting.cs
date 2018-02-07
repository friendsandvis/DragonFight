using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_HealthEffecting :Spell {

	public Spell_HealthEffecting()
	{
		spellid = SpellID.POISIONARRAOW;
		noofturnsforcooldown = 0;
		effectall = false;
		effecteddragoncount = 1u;
		selfeffecting = false;
		isnturneffect = false;
		effectvalue = 5.0f;
	}

	public override void effect (Dragon dragon)
	{
		dragon.maxhealth += getDragonEffect(dragon);
	}
}
