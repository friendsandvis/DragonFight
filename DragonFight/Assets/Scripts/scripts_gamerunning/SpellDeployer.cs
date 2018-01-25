using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDeployer 
{
	public void deploySpell(List<Dragon> dragons,SpellID spellid)
	{
		Spell spell=null;

		//set apt spell
		switch(spellid)
		{
		case SpellID.POISIONBOMB:
				spell = new Spell2 ();
				break;
		case SpellID.POISIONARRAOW:
			spell = new Spell1 ();
			break;
		}

		//spply spell effect to each dragon
		foreach (Dragon dragon in dragons) 
			spell.effect (dragon);
	}


	public bool doesSpellEffectsAllDragons(SpellID spellid)
	{
		Debug.Log (spellid);
		switch (spellid) {

		case SpellID.POISIONBOMB:
			return Spell2.effectalldragons;

		case SpellID.POISIONARRAOW:
			return Spell1.effectalldragons;
		
		default:
			return true;
		}
	}

}
