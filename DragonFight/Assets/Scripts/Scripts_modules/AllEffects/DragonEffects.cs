using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a basic effect group that can be applied to a list of dragons
public class DragonEffects : Effect {

	//list of effected dragons
	public List<Dragon> effecteddragons;

	public DragonEffects()
	{
		isDragonEffecting = true;
		isEffectPerTurn = true;
	}

	public override void effect ()
	{
		if (effectfornumberofturns <= 0)
			return;

		foreach (Dragon dragon in effecteddragons) {
			spell.effect (dragon);
		}

		effectfornumberofturns--;
	}

	public void setDragons(List<Dragon> dragons)
	{
		this.effecteddragons = dragons;
	}

}
