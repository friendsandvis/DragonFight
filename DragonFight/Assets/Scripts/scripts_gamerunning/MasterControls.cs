using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DragonPrefabs
{
	public GameObject[] dragons;
}

//the state Points while gameis running
public enum GameStates
{
	SPAWN,MOVE,ATTACK,SPELL,CELLSELECT,NONE	
};





//the class to act as a link to all the runners and modules
public class MasterControls : MonoBehaviour {

	//Dragon loader and deployer(instansiated in the start of script)
	public DragonLoader dragonloader;
	private DragonDeployer dragondeployer;

	//Spell deployer
	private SpellDeployer spelldeployer;

	//must be set by inspector(all the dragon prefabs)
	public DragonPrefabs dragonprefab;

	//sibling component scripts
	public TurnBasedSystem turnmanager;

	//battlefield instance(Manual assignment)
	public BattleField battlefield;
	//battlefield gamedata
	public BattleField_GameData battlefieldgamedata;

	//Game State Runner
	GameStates gstate;

	//current dragon's index in the dragonloader list and prefabs list(used to get dragon prefab and dragon data from loader)
	public int currentdragonindex;




	//-------------------------------UTILITY variables needed for functioning of this class------------------------
	//not sure if they are supposed to be another class or not.(this approce seems resonable right now)
	private List<Dragon> utility_listofdragons=null;			
	private SpellID utility_spellid;



	// Use this for initialization
	void Start () {
		//new Instanses
		dragonloader = new DragonLoader ();
		dragonloader.startLoadingDragons();
		dragondeployer = new DragonDeployer ();

		//spelldeployer
		spelldeployer=new SpellDeployer();

		//loading sibling scripts
		turnmanager=this.gameObject.GetComponent<TurnBasedSystem>();

		//default value for gstate
		gstate = GameStates.NONE;

		//default index of current dragon
		currentdragonindex=-1;

		//initialize battlefield data from the attribs of battlefield object(Very important that battlefield is allready a valid object)
		battlefieldgamedata=new BattleField_GameData(battlefield.nooftiles_vertical,battlefield.nooftiles_horizontal);
	}


	// Update is called once per frame
	void Update () {
		
		//do mouse click based actions
		poolMouseClicks ();
			
	}







	private void poolMouseClicks()
	{
		//detect mouse click(for spawn)
		if (!Input.GetMouseButtonDown (0))
			return;
		
		//act upon the current state of the play
		switch (gstate) {

		case GameStates.SPAWN:
				if (battlefield.isMouseInBoard ()) {

					//check if the current dragon index is a valid index
					if (currentdragonindex >= 0 && currentdragonindex < dragonprefab.dragons.Length && currentdragonindex < dragonloader.dragons.Count) {

						//if correct then deploy the dragon
						Dragon dragonattrib=dragondeployer.deployDragon (dragonprefab.dragons [currentdragonindex], dragonloader.dragons [currentdragonindex], battlefield.getMouseWorldposonBoard ());

						//update chancges to the battlefield data
						Vector3 mousehitpoint=battlefield.getRayCastHitPoint();
						Vector2 gridindex = battlefield.getGridIndex (mousehitpoint);
						battlefieldgamedata.SetDragon ((int)gridindex.x,(int)gridindex.y,dragonattrib);
					}
				}
			break;

		case GameStates.CELLSELECT:
			if (battlefield.isMouseInBoard ()) {

				//get the dragon at the clicked point on board(nothing if null)
				//not checking for repition right now
				Vector3 mousehitpoint=battlefield.getRayCastHitPoint();
				Vector2 gridindex = battlefield.getGridIndex (mousehitpoint);
				Dragon dragon=battlefieldgamedata.getDragon((int)gridindex.x,(int)gridindex.y);
				if (dragon != null)
					utility_listofdragons.Add (dragon);
			
			}
			break;

		}
	}


	//turn ending function
	public void endTurn()
	{
		//update turn manager
		turnmanager.nextTurn ();

		//set state to none
		gstate=GameStates.NONE;


		//reset currentdragon
		currentdragonindex=-1;
	}

	//set appropriate state based on the incomming string
	public void setState(GameStates state)
	{
		gstate = state;
	}


	//set the index of current dragon choice
	public void setCurrentDragonIndex(int dragonindex)
	{
		currentdragonindex = dragonindex;
	}


	//deploy the spell or start a cell selection procedure if needed
	public void prepareSpell(SpellID spellid)
	{
		if (spelldeployer.doesSpellEffectsAllDragons (spellid))
			utility_listofdragons = battlefieldgamedata.getAllDragon ();

		//selected dragon spell(Start selection procedure)
		else {
			setState (GameStates.CELLSELECT);
			utility_spellid = spellid;
			utility_listofdragons = new List<Dragon> ();
		}
	}

	public void deploySpell(SpellID spellid)
	{
		//deploy the spell with the list of dragons in utility list of dragos
		spelldeployer.deploySpell (utility_listofdragons, spellid);

		//set the utility list to null (just for safety and garbage collection)
		utility_listofdragons = null;
	}

}
