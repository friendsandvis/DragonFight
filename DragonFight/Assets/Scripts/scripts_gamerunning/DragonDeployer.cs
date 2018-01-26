using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDeployer{
	
	public Dragon_GameController deployDragon (GameObject dragonprefab,DragonType dtype , Vector3 pos)
	{
		Dragon dragonattrib = getDragonObject (dtype);
		
		//instantiate dragon prefab
		GameObject dragon=GameObject.Instantiate (dragonprefab);
		dragon.GetComponent<Dragon_GameController> ().setDragonAttribs (dragonattrib);

		//set the transform 
		Transform tf = dragon.transform;
		tf.position = pos;


		//return the newly ceated dragon's dragon_controller object
		return dragon.GetComponent<Dragon_GameController>();
	}




	//get the respective dragon's attrib object as per the dragon choice index
	public Dragon getDragonObject(DragonType dragontype)
	{
		switch (dragontype) {
		case DragonType.BAHEMUTDRAGON:
			return new BahemutDragon ();
		case DragonType.SPEEDSTERDRAGON:
			return new SpeedSterDragon ();
		case DragonType.SEADRAGON:
			return new SeaDragon ();
		case DragonType.TIGERDRAGON:
			return new TigerDragon ();
		}
		return null;
	}
}
