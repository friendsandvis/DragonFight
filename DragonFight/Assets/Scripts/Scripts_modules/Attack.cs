using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackId
{
	FIREBREADTH,TAILWHIP	
};

//represents a attck which is applied on a dragon
public class Attack {

	public AttackId attackid;
	public float damagedone;
	public uint range;
	public bool selfattacking;
	public bool effectall;
	public uint effecteddragoncount; 		//will be 0 when the case of effect all


	public virtual void applyEffect(Dragon dragon)
	{dragon.maxhealth -= damagedone;}
}
