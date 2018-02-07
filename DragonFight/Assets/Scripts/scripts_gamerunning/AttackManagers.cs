using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds a master copy of attacks that the complete game can have
//use this one to get copies of atttck objects for dragons
public class AttackManagers 
{
	public List<Attack> attackmasterinstances;

	public AttackManagers()
	{
		attackmasterinstances = null;
	}

	public AttackManagers(List<Attack> attacks)
	{
		attackmasterinstances = attacks;
	}


	public Attack getAttackCopy(AttackId reqattackid)
	{
		foreach(Attack attck in attackmasterinstances)
		{
			if (attck.attackid == reqattackid)
				return new Attack (attck);
		}

		return null;
	}
}
