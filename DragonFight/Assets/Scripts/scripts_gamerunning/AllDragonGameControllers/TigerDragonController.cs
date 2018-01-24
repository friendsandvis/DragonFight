using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerDragonController : MonoBehaviour {

	public TigerDragon tigerdragon;

	// Use this for initialization
	void Start () {
		tigerdragon = new TigerDragon ();
		this.gameObject.GetComponent<Dragon_GameController> ().setDragonAttribs(tigerdragon as Dragon);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//get dragon attribs
	public TigerDragon getDrgonAttribs()
	{
		return tigerdragon;
	}
}
