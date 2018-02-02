using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spell to reduce all dragon's health by 50
public class Spell3:Spell
{
	Effect effectobj;

	public Spell3()
	{
		spellid = SpellID.HEALTHUP;
		noofturnsforcooldown = 2u;
		effectall = true;
		effecteddragoncount = 0u;
		selfeffecting = true;
		isnturneffect = true;

		effectobj = new Heaalth_DragonEffect (10.0f,2u);
	}

	public Effect getEffect()
	{
		return effectobj;
	}
}