using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//effects are spells with some special data which are exicuted at the end of turn
public class Effect {

	public Spell spell;
	public uint effectfornumberofturns;
	public bool isDragonEffecting;
	public bool isEffectPerTurn;

	public bool isComplete()
	{
		return effectfornumberofturns <= 0;
	}

	public void setTurnsofEffect(uint turns)
	{
		this.effectfornumberofturns = turns;
	}

	public virtual void effect(){}
}
