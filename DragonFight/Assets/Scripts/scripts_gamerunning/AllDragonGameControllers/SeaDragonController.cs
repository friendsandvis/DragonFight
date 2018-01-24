using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaDragonController : MonoBehaviour {

	public SeaDragon seadragon;

	// Use this for initialization
	void Start () {
		seadragon = new SeaDragon ();
		this.gameObject.GetComponent<Dragon_GameController> ().setDragonAttribs(seadragon as Dragon);
	}

	// Update is called once per frame
	void Update () {

	}

	//get dragon attribs
	public SeaDragon getDrgonAttribs()
	{
		return seadragon;
	}
}
