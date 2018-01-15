using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class hold the complete data needed to represent a Inmagenary tile based battlefield
public class BattleField : MonoBehaviour
{
	//matrix to represent all tiles are occupied or not
	protected bool[,] presencematrix;


	//noof tiles vertically and horizontally
	public int nooftiles_vertical=1,nooftiles_horizontal=1;

	//size of board and hence the size of cell(aditionallly a autodetect feature toadjest the size of board as per the current scale)
	public float sizeofboard_vertical=1,sizeofboard_horizontal=1;
	public bool autodetectsizeofboard = false;
	protected float sizeofcell_vertical, sizeofcell_horizontal;


	//main Renderer for the Object
	private MeshRenderer mainrenderer;

	//Other Prefabs required
	public GameObject selectiontile;

	// Use this for initialization
	void Start () {

		//setting the gameobject specif attributes
		mainrenderer=GetComponent<MeshRenderer>();


		//set the initial presence of cells in the board
		presencematrix=new bool[nooftiles_vertical,nooftiles_horizontal];


		//set the sizeofboard if autodetect is enabled
		if(autodetectsizeofboard)
		setSize_BasedOnScale();

		//set the size of cell depending on the noof cells and size of board
		sizeofcell_vertical=sizeofboard_vertical/nooftiles_vertical;
		sizeofcell_horizontal=sizeofboard_horizontal/nooftiles_horizontal;

		//set the renderer tiling value for object
		mainrenderer.material.SetTextureScale("_MainTex",new Vector2(nooftiles_vertical,nooftiles_horizontal));

		//set the scale for selection tile
		selectiontile.transform.localScale=new Vector3(sizeofcell_vertical,sizeofcell_horizontal,2.0f);
	}


	void Update()
	{
		//--------------------------Temperary debugging function-START
		int layermask=1<<8;
		Ray screenray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (screenray.origin, screenray.direction, out hit, Mathf.Infinity, layermask)) {
			Vector2 gp = getGridPosition (hit.point);
			Vector3 tem=selectiontile.transform.position;
			selectiontile.transform.position = new Vector3 (gp.x, tem.y, gp.y);
		}
		//--------------------------Temperary debugging function-FINISH
	}


	public Vector2 getGridPosition(Vector3 point)
	{
		point += new Vector3 (sizeofboard_vertical,0.0f,sizeofboard_horizontal)/2.0f;
		point.x = point.x-(point.x%sizeofcell_vertical);
		point.z = point.z-(point.z%sizeofcell_horizontal);
		point.x += sizeofcell_vertical / 2.0f;
		point.z += sizeofcell_horizontal / 2.0f;
		point -= new Vector3 (sizeofboard_vertical,0.0f,sizeofboard_horizontal)/2.0f;

		return new Vector2 (point.x, point.z);
	}


	/*
	 * set the current vertical and horizontal size depending on the current scale of the attached gameobject
	*Expected game object is Cube
	*The size is the scale
	*/
	private void setSize_BasedOnScale()
	{
		Transform transform=this.gameObject.GetComponent<Transform> ();
		sizeofboard_horizontal = transform.localScale.z ;
		sizeofboard_vertical = transform.localScale.x;
	}
}
