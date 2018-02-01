using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDeployer {
	private Attack attack=null;

	public void prepareAttack(Attack attack)
	{
		Debug.Log (attack.attackid);
		this.attack=attack;
	}

	public void deployAttack(List<Dragon> affecteddragons,float efficiency)
	{
		foreach (Dragon drag in affecteddragons) {
			attack.applyEffect (drag,efficiency);
		}

		//safety
		attack=null;
	}

	public bool doesEffectAllinrange()
	{
		if (attack == null) {
			Debug.Log ("Attack not choosen");
			return false;
		}

		return attack.effectall;
	}

	public uint getRange()
	{
		if (attack == null) {
			Debug.Log ("Attack not choosen");
			return 0u;
		}

		return attack.range;
	}

	public uint getAffectedDragonCount()
	{
		if (attack == null) {
			Debug.Log ("Attack not choosen");
			return 0u;
		}

		return attack.effecteddragoncount;
	}
}
