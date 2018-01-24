using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all drgans(holds common attribs)
[System.Serializable]			//serialization is for debugging
public class Dragon
{
	//basic attribs of any dragon
	public float maxhealth=0.0f;
	public string dragonname="No_Name";

	public Dragon(float maxhealth)
	{
		this.maxhealth = maxhealth;
	}

	//copy constructor
	public Dragon(Dragon dragondata)
	{
		if (dragondata == null)
			return;

		this.maxhealth = dragondata.maxhealth;
		this.dragonname = dragondata.dragonname;
	}
}
