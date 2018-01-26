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
	SPAWN,MOVE,ATTACK,SPELL,SPELL_DRAGONSELECT,MOVEMENT_DRAGONSELECT,MOVEMENT_FINALTILESELECT,ATTACK_DRAGONSELECT,NONE	
};





//the class to act as a link to all the runners and modules
public class MasterControls : MonoBehaviour {

	//------------------preinitialized data--------------------------
	public UIBUttonControl buttoncontrols;

	//Dragon loader and deployer(instansiated in the start of script)
	//public DragonLoader dragonloader;//not needed right now(becase dragoons are hard coded specail classes)
	private DragonDeployer dragondeployer;

	//Spell deployer
	private SpellDeployer spelldeployer;

	//Attack Deployer
	private AttackDeployer attackdeployer;

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

	//current dragon's type in  prefabs list(used to get dragon prefab)
	public DragonType currentdragontype;


	//-----------------GameData----------------
	public uint noofdragons,noofspells;
	public Player[] players;

	//-------------------------------UTILITY variables needed for functioning of this class------------------------
	//not sure if they are supposed to be another class or not.(this approce seems resonable right now)
	//most are initialized only when needed and nullifid after use
	private List<Dragon> utility_listofdragons=null;
	private List<Dragon_GameController> utility_listofdragoncontrollers=null;	
	private Vector2 utility_tileindex;
	private SpellID utility_spellid;



	// Use this for initialization
	void Start () {
		//new Instanses
		/*Not Required right now for above rasons mentioned
		dragonloader = new DragonLoader ();
		dragonloader.startLoadingDragons();
		*/
		dragondeployer = new DragonDeployer ();

		//spelldeployer
		spelldeployer=new SpellDeployer();

		//attack deployer
		attackdeployer=new AttackDeployer();

		//load the spell effect matrix
		Spell.initSpellEffectValueLoader(noofspells,noofdragons);

		//loading sibling scripts
		turnmanager=this.gameObject.GetComponent<TurnBasedSystem>();

		//initialize players
		initPlayers();

		//default value for gstate
		gstate = GameStates.NONE;

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
				if ((uint)currentdragontype >= 0 && (uint)currentdragontype < dragonprefab.dragons.Length) {

					//the hit point of grid
					Vector3 mousehitpoint = battlefield.getRayCastHitPoint ();
					Vector2 gridindex = battlefield.getGridIndex (mousehitpoint);

					//check if the selected plate is part of spawn plates for player
					if (!players [turnmanager.currentplayer - 1].isPlateASpawn ((uint)gridindex.x, (uint)gridindex.y))
						break;

					//if correct then deploy the dragon
					Dragon_GameController dragoncontroller = dragondeployer.deployDragon (dragonprefab.dragons [(uint)currentdragontype],  currentdragontype, battlefield.getMouseWorldposonBoard ());

					//add dragon to the player
					players [turnmanager.currentplayer - 1].addDragonToPlayer (dragoncontroller);

					//update chancges to the battlefield data
					battlefieldgamedata.SetDragon ((int)gridindex.x, (int)gridindex.y, dragoncontroller);
				}
			}
			break;

		case GameStates.SPELL_DRAGONSELECT:
			if (battlefield.isMouseInBoard ()) {

				//get the dragon at the clicked point on board(nothing if null)
				//not checking for repition right now
				Vector3 mousehitpoint = battlefield.getRayCastHitPoint ();
				Vector2 gridindex = battlefield.getGridIndex (mousehitpoint);
				Dragon dragon = battlefieldgamedata.getDragonOfPlayer ((int)gridindex.x, (int)gridindex.y,(uint)(turnmanager.currentplayer-1));

				if (dragon != null) {
					utility_listofdragons.Add (dragon);

					//terminate spell dragon selection proocess
					if (utility_listofdragons.Count >= spelldeployer.getExpectedEffectedDragonCount ()) {
						gstate = GameStates.NONE;
					}
				}
			
			}
			break;

		case GameStates.MOVEMENT_DRAGONSELECT:
			if (battlefield.isMouseInBoard ()) {

				//select a single dragon on board
				Vector3 mousehitpoint = battlefield.getRayCastHitPoint ();
				Vector2 gridindex = battlefield.getGridIndex (mousehitpoint);
				Dragon dragon = battlefieldgamedata.getDragonOfPlayer ((int)gridindex.x, (int)gridindex.y,(uint)(turnmanager.currentplayer-1));
				if (dragon != null) {
					utility_tileindex = gridindex;
					gstate = GameStates.MOVEMENT_FINALTILESELECT;
				}

			}
			break;

		case GameStates.MOVEMENT_FINALTILESELECT:
			if (battlefield.isMouseInBoard ()) {

				//select a final tile to move the previous dragon to on board
				Vector3 mousehitpoint = battlefield.getRayCastHitPoint ();
				Vector2 gridindex = battlefield.getGridIndex (mousehitpoint);


				if (battlefieldgamedata.moveDragon((int)utility_tileindex.x,(int)utility_tileindex.y,(int)gridindex.x,(int)gridindex.y,battlefield.getMouseWorldposonBoard ())) {
					gstate = GameStates.NONE;
				}

			}
			break;

		case GameStates.ATTACK_DRAGONSELECT:
			if (battlefield.isMouseInBoard ()) {

				//select a single dragon on board
				Vector3 mousehitpoint = battlefield.getRayCastHitPoint ();
				Vector2 gridindex = battlefield.getGridIndex (mousehitpoint);
				Dragon dragon = battlefieldgamedata.getDragonOfPlayer ((int)gridindex.x, (int)gridindex.y,(uint)(turnmanager.currentplayer-1));
				if (dragon != null) {
					utility_tileindex = gridindex;
					gstate = GameStates.NONE;
					Debug.Log (utility_tileindex.x+"   "+utility_tileindex.y);
					//pass the message to ui controls
					buttoncontrols.showAttackPane();
				}

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
	}

	//set appropriate state based on the incomming string
	public void setState(GameStates state)
	{
		gstate = state;
	}


	//set the index of current dragon choice
	public void setCurrentDragonIndex(DragonType dragontype)
	{
		currentdragontype = dragontype;
	}


	//deploy the spell or start a cell selection procedure if needed
	public void prepareSpell(SpellID spellid)
	{
		//prepareSpell the Spell int the Spell deployer
		spelldeployer.prepareSpell (spellid);

		//set effected dragons if all is range of effect
		if (spelldeployer.doesSpellEffectsAllDragons ()) {
			if(spelldeployer.isSpellSelfEffecting())
				utility_listofdragons = battlefieldgamedata.getAllDragonOfPlayer ((uint)(turnmanager.currentplayer-1));
			else
				utility_listofdragons = battlefieldgamedata.getAllDragonNotOfPlayer ((uint)(turnmanager.currentplayer-1));
		}

		//selected dragon spell(Start selection procedure)
		else {
			setState (GameStates.SPELL_DRAGONSELECT);
			utility_spellid = spellid;
			utility_listofdragons = new List<Dragon> ();
		}
	}

	public void deploySpell(SpellID spellid)
	{
		//deploy the spell with the list of dragons in utility list of dragos
		spelldeployer.deploySpell (utility_listofdragons);

		//set the utility list to null (just for safety and garbage collection)
		utility_listofdragons = null;
	}

	public void initDragonMovement()
	{
		//set the initializers for starting the movement of a dragon
		gstate=GameStates.MOVEMENT_DRAGONSELECT;
	}
		
	public void selectDragonToAttack()
	{
		gstate = GameStates.ATTACK_DRAGONSELECT;
	}

	public void prepAttack(int attackindex)
	{
		Dragon drag = battlefieldgamedata.getDragon ((int)utility_tileindex.x,(int)utility_tileindex.y);
		attackdeployer.prepareAttack (drag.moves [attackindex]);

		//deploy attack imidiately if attacking all
		if (attackdeployer.doesEffectAllinrange ()) {
			utility_listofdragons = battlefieldgamedata.getAllDragonNotOfPlayer ((uint)(turnmanager.currentplayer-1));
		}
	}


	//--------------------------------------------Initialization support-----------------------------------------
	//----------------WARNING::hard coded data--------------------------
	//----must change plate positions when grid size changes
	private void initPlayers()
	{
		players=new Player[2];

		//player 1
		players[0]=new Player(0);
		players [0].dragonrotation = Quaternion.Euler (new Vector3 (0.0f,180.0f,0.0f ));
		players [0].addSpawnPlate (2,8);

		//player 2
		players[1]=new Player(1);
		players [1].dragonrotation = Quaternion.Euler (new Vector3 (0.0f,0.0f,0.0f ));
		players [1].addSpawnPlate (2,1);
	}
}
