using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//a spell is just a base class
public abstract class Spell
{
	public string name;
	public int noofturnsforcooldown;
	public virtual void effect (Dragon dragon){}
}
