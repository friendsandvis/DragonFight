using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {

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
