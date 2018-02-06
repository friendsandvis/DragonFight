using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIBUttonControl : MonoBehaviour {

	//set manually
	public MasterControls mastergamerunner;

	//Retrived addbutton scripts that serves as the builders for dynamic select lists
	private AddButton dynamicoptionpane_addbutton;

	//all the buttons(set manually to avoid confusions)
	public Button endturn,spawnbutton,spellbutton,deployspellbutton,movebutton,attackbutton;


	//-------------------------------UTILITY variables needed for functioning of this class------------------------
	//not sure if they are supposed to be another class or not.(this approce sseems resonable right now)
	private int utility_spellindex;//will remove later


	public GameObject buttonpane,spellbuttongroup,attackbuttongroup;

	// Use this for initialization
	void Start () {

		//set Components
		dynamicoptionpane_addbutton=buttonpane.GetComponentInChildren<AddButton>();

		//end turn listner
		endturn.onClick.AddListener (()=>{mastergamerunner.endTurn();});

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
		List<Button> spellbutlist=new List<Button>();

		for (uint z = 0; z < spellbuttons.Length; z++)
			spellbutlist.Add (spellbuttons [z]);

		addButtonListner (spellbutlist,(int data)=>activateSpell(data));

		//set spellbutton listeners
		Button[] attackbuttons = attackbuttongroup.GetComponentsInChildren<Button> ();
		attackbuttons [0].onClick.AddListener (delegate { prepareAttack(0);});
	}


	//Sets the state of game to spawn a character
	//for that also recieve the list of all the dragons that are spawnable
	private void setState_Spawn()
	{
		//dragonbuttongroup.SetActive(true);
		buttonpane.SetActive (true);

		//build buttons for names
		dynamicoptionpane_addbutton.addButtonsForList (mastergamerunner.getPlayerDragonNames());

		//add listners for buttons added
		addButtonListner(dynamicoptionpane_addbutton.getButtons(),((int data)=> setCurrentDragonIndex(data)));

		//finally set the spawn state
		mastergamerunner.setState (GameStates.SPAWN);
	}


	//Sets the state of game to spawn a character
	//for that also recieve the list of all the dragons that are spawnable
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
		buttonpane.SetActive(false);

		mastergamerunner.setCurrentDragonIndex (index);
	}

	private void activateSpell(int index)
	{
		if (!mastergamerunner.isSpellUsable (index))
		{
			Debug.Log ("Spell not usable");
			return;
		}

		//disable spell buttons
		spellbuttongroup.SetActive(false);

		//activate deploy spell button
		deployspellbutton.gameObject.SetActive(true);

		//just prepare the spell not deploy it
		mastergamerunner.prepareSpell (index);
		utility_spellindex = index;
	}

	private void deploySpell()
	{
		//deactivate deploy spell button
		deployspellbutton.gameObject.SetActive(false);

		//deploy spell
		mastergamerunner.deploySpell (utility_spellindex);
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
