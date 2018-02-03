﻿using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIBUttonControl : MonoBehaviour {

	//set manually
	public MasterControls mastergamerunner;

	//Retrived components 
	private AddButton dragon_addbutton;

	//all the buttons(set manually to avoid confusions)
	public Button endturn;
	public Button spawnbutton,spellbutton,deployspellbutton,movebutton,attackbutton;


	//-------------------------------UTILITY variables needed for functioning of this class------------------------
	//not sure if they are supposed to be another class or not.(this approce sseems resonable right now)
	private SpellID utility_spellid;


	public GameObject dragonbuttonpane,spellbuttongroup,attackbuttongroup;

	// Use this for initialization
	void Start () {

		//set Components
		dragon_addbutton=dragonbuttonpane.GetComponentInChildren<AddButton>();

		//end turn listner
		endturn.onClick.AddListener (endTurn);

		//spawn button listner
		spawnbutton.onClick.AddListener (setState_Spawn);

		//spell button listner
		spellbutton.onClick.AddListener (setState_Spell);

		//deploy spell button listner
		deployspellbutton.onClick.AddListener (deploySpell);

		//move button listner
		movebutton.onClick.AddListener (initDragonMovement);

		//attack button listner
		attackbutton.onClick.AddListener (selectDragonToAttack);

		//set spellbutton listeners
		Button[] spellbuttons = spellbuttongroup.GetComponentsInChildren<Button> ();
		spellbuttons [0].onClick.AddListener (delegate {activateSpell (SpellID.POISIONARRAOW);});
		spellbuttons [1].onClick.AddListener (delegate {activateSpell (SpellID.POISIONBOMB);});
		spellbuttons [2].onClick.AddListener (delegate {activateSpell (SpellID.HEALTHUP);});

		//set spellbutton listeners
		Button[] attackbuttons = attackbuttongroup.GetComponentsInChildren<Button> ();
		attackbuttons [0].onClick.AddListener (delegate { prepareAttack(0);});
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void endTurn()
	{
		mastergamerunner.endTurn ();
	}

	private void setState_Spawn()
	{
		//dragonbuttongroup.SetActive(true);
		dragonbuttonpane.SetActive (true);

		//build buttons for names
		dragon_addbutton.addButtonsForList (mastergamerunner.getPlayerDragonNames());

		//add listners for buttons added
		addButtonListner(dragon_addbutton.getButtons(),((int data)=> setCurrentDragonIndex(data)));

		mastergamerunner.setState (GameStates.SPAWN);
	}

	private void setState_Spell()
	{
		//enable spell buttons
		spellbuttongroup.SetActive(true);

		mastergamerunner.setState (GameStates.SPELL);
	} 


	//sends the index of player's dragon list which needs to be used 
	public void setCurrentDragonIndex(int index)
	{
		//disable the dragon buttons
		//dragonbuttongroup.SetActive(false);
		dragonbuttonpane.SetActive(false);

		mastergamerunner.setCurrentDragonIndex (index);
	}

	private void activateSpell(SpellID spellid)
	{
		if (!mastergamerunner.isSpellUsable (spellid))
		{
			Debug.Log ("Spell not usable");
			return;
		}
		
		//disable spell buttons
		spellbuttongroup.SetActive(false);

		//activate deploy spell button
		deployspellbutton.gameObject.SetActive(true);

		//just prepare the spell not deploy it
		mastergamerunner.prepareSpell (spellid);
		utility_spellid = spellid;
	}

	private void deploySpell()
	{
		//deactivate deploy spell button
		deployspellbutton.gameObject.SetActive(false);

		//deploy spell
		mastergamerunner.deploySpell (utility_spellid);
	}

	private void initDragonMovement()
	{
		//set the initializers for starting the movement of a dragon
		mastergamerunner.initDragonMovement();
	}

	private void selectDragonToAttack()
	{
		mastergamerunner.selectDragonToAttack ();
	}

	public void showAttackPane()
	{
		attackbuttongroup.SetActive (true);
	}

	private void prepareAttack(int index)
	{
		attackbuttongroup.SetActive (false);

		mastergamerunner.prepAttack (index);
	}

	//lambda using function to add functionality to given button set
	private void addButtonListner(List<Button> buttons,Action<int> functionality)
	{
		for (int z = 0; z < buttons.Count; z++) {
			int zcopy = z;
			buttons [z].onClick.AddListener (() => functionality (zcopy));
		}
	}
}
