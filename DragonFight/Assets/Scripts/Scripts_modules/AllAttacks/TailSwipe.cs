using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSwipe : Attack {
	public TailSwipe(float damagedone)
	{
		this.damagedone = damagedone;
		attackid = AttackId.TAILWHIP;
		range = 1u;
		selfattacking = false;
		effectall = true;
		effecteddragoncount = 0u;
	}

}
