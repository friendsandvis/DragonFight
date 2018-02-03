using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_selfSetText : MonoBehaviour {

	public Text text;

	public void setText(string str)
	{
		text.text = str;
	}
}
