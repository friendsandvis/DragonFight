using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellID:uint
{
	POISIONBOMB=0u,
	POISIONARRAOW=1u,
	HEALTHUP=2u
};

public enum SpellIDBasic:uint
{
	HEALTHEFFECTING=0u
};

//a spell is just a base class
public abstract class Spell
{
	public SpellID spellid;
	public SpellIDBasic spellbasicid;
	public bool isnturneffect;
	public uint noofturnsforcooldown;
	public bool effectall;
	public bool selfeffecting;
	public uint effecteddragoncount;
	public float effectvalue;
	public float spellexppoints;

	private static SpellEffectValueLoader spelleffectdataloader=null;

	public static void initSpellEffectValueLoader(uint noofspells,uint noofdragons)
	{
		spelleffectdataloader = new SpellEffectValueLoader (noofspells,noofdragons);
	}



	public virtual void effect (Dragon dragon){}

	//gets the respective dragons effect attrib value from the spellbinder
	//fuction used to get attrib value in the effect function
	protected virtual float getDragonEffect(Dragon dragon)
	{
		return spelleffectdataloader.getAttrib ((uint)this.spellid,(uint)dragon.dragontype);
	}

	//default constructor
	public Spell()
	{
	}

	//copy constructor(usable is spELL IS not abstract)
	/*
	public Spell(Spell sp)
	{
		spellid=sp.spellid;
		spellbasicid=sp.spellbasicid;
		isnturneffect=sp.isnturneffect;
		noofturnsforcooldown=sp.noofturnsforcooldown;
		effectall=sp.effectall;
		selfeffecting=sp.selfeffecting;
		effecteddragoncount=sp.effecteddragoncount;
		effectvalue=sp.effectvalue;
		spellexppoints=sp.spellexppoints;
	}
	*/
}
