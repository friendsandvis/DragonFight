using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDeployer{
	
	public Dragon deployDragon (GameObject dragonprefab,Dragon dragondata, Vector3 pos)
	{

		GameObject dragon=GameObject.Instantiate (dragonprefab);
		dragon.GetComponent<Dragon_GameController> ().setDragonAttribs(dragondata);

		Transform tf = dragon.transform;
		tf.position = pos;


		//return the newly ceated dragon's dragon object
		return dragon.GetComponent<Dragon_GameController>().dragonattribs;
	}
}
