using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a singleton class
public class SpellDeployer 
{
	Spell currentspell;

	//set the current speell as a refrence to the spell comming from the main controlls 
	//assuming this is the spell which is selected now
	public void prepareSpell(Spell spell)
	{
		currentspell = (spell);
		Debug.Log (currentspell.spellid);
	}

	public void deploySpell(List<Dragon> dragons)
	{
		//spply spell effect to each dragon
		foreach (Dragon dragon in dragons) 
			currentspell.effect (dragon);

		//safety first
		currentspell = null;
	}

	public Effect getEffect(List<Dragon> dragons)
	{
		//considering dragon effects only

		DragonEffects eff = new DragonEffects ();
		eff.spell = currentspell;
		eff.effecteddragons = dragons;
		return eff as Effect;
	}



	//-------------------------------------utility functins to retrive data from current sppelll--------------------------------


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

	public bool isTurnEffecting()
	{
		if (currentspell == null)
		{
			Debug.Log ("No selected spell");
			return false;
		}

		return currentspell.isnturneffect;
	}
}
