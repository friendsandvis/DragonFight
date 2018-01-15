using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
	//basic Attributes of any spell
	public Dragon affecteddragon;			//the object of the dragont heat is affected by this spell

	public Spell(Dragon dragon)
	{
		affecteddragon = dragon;
	}
}
