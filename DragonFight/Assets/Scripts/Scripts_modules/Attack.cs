using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackId:uint
{
	FIREBREADTH=0u,TAILWHIP=1u	
};

//represents a attck which is applied on a dragon
public class Attack {

	public AttackId attackid;
	public float damagedone;
	public uint range;
	public bool selfattacking;
	public bool effectall;
	public uint effecteddragoncount; 		//will be 0 when the case of effect all


	public virtual void applyEffect(Dragon dragon,float efficiency)
	{
		dragon.maxhealth -= efficiency*damagedone;
	}

	//default constructor
	public Attack(){}

	//copy constructor
	public Attack(Attack attack)
	{
		attackid = attack.attackid;
		damagedone = attack.damagedone;
		range = attack.range;
		selfattacking = attack.selfattacking;
		effectall = attack.effectall;
		effecteddragoncount = attack.effecteddragoncount;
	}
}
