using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour {

	private Transform contexttransform;
	public GameObject buttonobj;
	List<GameObject> currentbuttons;

	// Use this for initialization
	void Awake () {
		Debug.Log ("Awake");
		contexttransform = gameObject.GetComponent<Transform> ();
		currentbuttons = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addButtonsForList(List<string> data)
	{
		//will change
		deleteButtons ();

		foreach (string str in data) {
			addButton (str);
		}
	}

	public List<Button> getButtons()
	{
		List<Button> buttons=new List<Button>();

		foreach (GameObject obj in currentbuttons)
			buttons.Add (obj.GetComponent<Button> ());
		
		return buttons;
	}

	void addButton(string name)
	{
		GameObject newobj = Instantiate (buttonobj);
		newobj.GetComponent<Button_selfSetText> ().setText(name);
		newobj.GetComponent<Transform> ().SetParent (contexttransform);
		currentbuttons.Add (newobj);
	}

	void deleteButtons()
	{
		if (currentbuttons.Count <= 0)
			return;

		foreach (GameObject obj in currentbuttons)
			Destroy (obj);

		currentbuttons.Clear ();
	}
}
