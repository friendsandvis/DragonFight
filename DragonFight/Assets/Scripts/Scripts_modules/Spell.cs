using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellID:uint
{
	POISIONBOMB=0u,
	POISIONARRAOW=1u
};

//a spell is just a base class
public abstract class Spell
{
	public SpellID spellid;
	public int noofturnsforcooldown;

	private static SpellEffectValueLoader spelleffectdataloader=null;

	public static void initSpellEffectValueLoader(uint noofspells,uint noofdragons)
	{
		spelleffectdataloader = new SpellEffectValueLoader (noofspells,noofdragons);
	}

	public virtual void effect (Dragon dragon){}

	//gets the respective dragons effect attrib value from the spellbinder
	protected virtual float getDragonEffect(Dragon dragon)
	{
		return spelleffectdataloader.getAttrib ((uint)this.spellid,(uint)dragon.dragontype);
	}
}
