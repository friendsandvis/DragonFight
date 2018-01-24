using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSterDragonController : MonoBehaviour {

	public SpeedSterDragon speedsterdragon;

	// Use this for initialization
	void Start () {
		speedsterdragon = new SpeedSterDragon ();
		this.gameObject.GetComponent<Dragon_GameController> ().setDragonAttribs(speedsterdragon as Dragon);
	}

	// Update is called once per frame
	void Update () {

	}

	//get dragon attribs
	public SpeedSterDragon getDrgonAttribs()
	{
		return speedsterdragon;
	}
}
