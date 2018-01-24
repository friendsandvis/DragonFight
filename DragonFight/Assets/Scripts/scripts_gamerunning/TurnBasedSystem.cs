using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour {

	public int totalplayersplaying=2;
	public int currentplayer=1;

	//end turn(and set next player for turn)
	public void nextTurn()
	{
		currentplayer=currentplayer%totalplayersplaying+1;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
