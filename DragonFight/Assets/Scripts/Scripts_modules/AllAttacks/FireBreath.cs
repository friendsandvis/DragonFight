using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBredth : Attack {

	public FireBredth(float damagedone)
	{
		this.damagedone = damagedone;
		attackid = AttackId.FIREBREADTH;
		range = 5u;
		selfattacking = false;
		effectall = false;
		effecteddragoncount = 1u;
	}

	public override void applyEffect (Dragon dragon)
	{
		base.applyEffect (dragon);
	}

}
