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

	public void deployAttack(List<Dragon> affecteddragons)
	{
		foreach (Dragon drag in affecteddragons) {
			attack.applyEffect (drag);
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
}
