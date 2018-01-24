using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDeployer{
	
	public Dragon deployDragon (GameObject dragonprefab, Dragon dragonattrib, Vector3 pos)
	{
		//instantiate dragon prefab
		GameObject dragon=GameObject.Instantiate (dragonprefab);
		dragon.GetComponent<Dragon_GameController> ().setDragonAttribs (dragonattrib);

		//set the transform 
		Transform tf = dragon.transform;
		tf.position = pos;


		//return the newly ceated dragon's dragon object(onluy the basic)
		return dragon.GetComponent<Dragon_GameController>().dragonattribs;
	}
}
