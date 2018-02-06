using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heaalth_DragonEffect : DragonEffects {

	public float healthreduction;

	public Heaalth_DragonEffect(float healthreduction,uint noofturnstoworkfor):base()
	{
		this.healthreduction = healthreduction;
		this.effectfornumberofturns = noofturnstoworkfor;
		isEffectPerTurn = false;
	}

	public void sethealthredectionvalue(float hvalue)
	{
		this.healthreduction = hvalue;
	}
	
}
