using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDeployer{

	//the dragon attribs to use fo pseploying the dragon
	public Dragon dragonattrib;

	//deploy the dragons
	public Dragon_GameController deployDragon (GameObject dragonprefab, Vector3 pos)
	{
		//instantiate dragon prefab
		GameObject dragon=GameObject.Instantiate (dragonprefab);

		//set the transform 
		Transform tf = dragon.transform;
		tf.position = pos;

		//set dragon attribs
		dragon.GetComponent<Dragon_GameController> ().setDragonAttribs (dragonattrib);

		//return the newly ceated dragon's dragon_controller object
		return dragon.GetComponent<Dragon_GameController>();
	}
}
