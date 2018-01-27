using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a singleton class
public class SpellDeployer 
{
	Spell currentspell;


	public void prepareSpell(SpellID spellid)
	{
		Debug.Log (spellid);
		switch(spellid)
		{
		case SpellID.POISIONBOMB:
			currentspell = new Spell2 ();
			break;
		case SpellID.POISIONARRAOW:
			currentspell = new Spell1 ();
			break;
		}
	}

	public void deploySpell(List<Dragon> dragons)
	{
		//spply spell effect to each dragon
		foreach (Dragon dragon in dragons) 
			currentspell.effect (dragon);

		//safety first
		currentspell = null;
	}


	public bool doesSpellEffectsAllDragons()
	{
		if (currentspell == null)
		{
			Debug.Log ("No selected spell");
			return false;
		}
			
		return currentspell.effectall;
	}

	public bool isSpellSelfEffecting()
	{
		if (currentspell == null)
		{
			Debug.Log ("No selected spell");
			return false;
		}

		return currentspell.selfeffecting;
	}

	public uint getExpectedEffectedDragonCount()
	{
		if (currentspell == null)
		{
			Debug.Log ("No selected spell");
			return 0;
		}

		return currentspell.effecteddragoncount;
	}

	public uint getCoolDown()
	{
		if (currentspell == null)
		{
			Debug.Log ("No selected spell");
			return 0;
		}

		return currentspell.noofturnsforcooldown;
	}
}
