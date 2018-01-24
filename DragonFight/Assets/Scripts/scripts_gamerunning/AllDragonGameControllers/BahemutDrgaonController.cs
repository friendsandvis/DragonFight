using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BahemutDrgaonController : MonoBehaviour {

	public BahemutDragon bahemutdragon;

	// Use this for initialization
	void Start () {
		bahemutdragon = new BahemutDragon ();
		this.gameObject.GetComponent<Dragon_GameController> ().setDragonAttribs(bahemutdragon as Dragon);
	}

	// Update is called once per frame
	void Update () {

	}

	//get dragon attribs
	public BahemutDragon getDrgonAttribs()
	{
		return bahemutdragon;
	}
}
