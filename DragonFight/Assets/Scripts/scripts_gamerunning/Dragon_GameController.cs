using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a script associated with every dragon as a component
public class Dragon_GameController : MonoBehaviour {

	//the dragon attribs this prefab will hold(given at time of instantiating)
	public Dragon dragonattribs=null;

	public void setDragonAttribs(Dragon dragondata)
	{
		dragonattribs = new Dragon (dragondata);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
