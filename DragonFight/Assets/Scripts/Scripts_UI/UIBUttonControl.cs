using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIBUttonControl : MonoBehaviour {

	//set manually
	public MasterControls mastergamerunner;

	//all the buttons(set manually to avoid confusions)
	public Button endturn;
	public Button spawnbutton,spellbutton,deployspellbutton,movebutton;


	//-------------------------------UTILITY variables needed for functioning of this class------------------------
	//not sure if they are supposed to be another class or not.(this approce seems resonable right now)
	private SpellID utility_spellid;

	public GameObject dragonbuttongroup,spellbuttongroup;

	// Use this for initialization
	void Start () {
		//end turn listner
		endturn.onClick.AddListener (endTurn);

		//spawn button listner
		spawnbutton.onClick.AddListener (setState_Spawn);

		//spell button listner
		spellbutton.onClick.AddListener(setState_Spell);

		//deploy spell button listner
		deployspellbutton.onClick.AddListener(deploySpell);

		//move button listner
		movebutton.onClick.AddListener(initDragonMovement);

		//spell

		//set dragonbutton listeners
		Button[] dragonbuttons = dragonbuttongroup.GetComponentsInChildren<Button> ();
		dragonbuttons [0].onClick.AddListener (delegate {setCurrentDragonIndex (DragonType.BAHEMUTDRAGON);});
		dragonbuttons [1].onClick.AddListener (delegate {setCurrentDragonIndex (DragonType.SEADRAGON);});
		dragonbuttons [2].onClick.AddListener (delegate {setCurrentDragonIndex (DragonType.SPEEDSTERDRAGON);});
		dragonbuttons [3].onClick.AddListener (delegate {setCurrentDragonIndex (DragonType.TIGERDRAGON);});

		//set spellbutton listeners
		Button[] spellbuttons = spellbuttongroup.GetComponentsInChildren<Button> ();
		spellbuttons [0].onClick.AddListener (delegate {activateSpell (SpellID.POISIONARRAOW);});
		spellbuttons [1].onClick.AddListener (delegate {activateSpell (SpellID.POISIONBOMB);});
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
		//enable dragon buttons
		dragonbuttongroup.SetActive(true);


		mastergamerunner.setState (GameStates.SPAWN);
	}

	private void setState_Spell()
	{
		//enable spell buttons
		spellbuttongroup.SetActive(true);


		mastergamerunner.setState (GameStates.SPELL);
	} 

	private void setCurrentDragonIndex (DragonType dragontype)
	{
		//disable the dragon buttons
		dragonbuttongroup.SetActive(false);

		mastergamerunner.setCurrentDragonIndex (dragontype);
	}

	private void activateSpell(SpellID spellid)
	{
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

}
