using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//using a very hard codded way for loadng a matrix(NEED Another Way)
public class SpellEffectValueLoader 
{
	public float[,] attribvalue;

	//location string
	string datafilessubdirectory="/DataFiles/",filename="spelleffectdata.txt";

	public SpellEffectValueLoader(uint noofspells,uint noofdragons)
	{
		attribvalue=new float[noofspells,noofdragons];
		fillMatrix (filename);
	}

	public void fillMatrix(string filename)
	{
		StreamReader effectreader = null;
		FileInfo spelleffectfile = new FileInfo (Application.dataPath+datafilessubdirectory+filename);

		if (spelleffectfile == null) {
			Debug.Log ("Error loading file:\t"+filename);
			return;
		}

		effectreader = spelleffectfile.OpenText ();
		if (effectreader == null) {
			Debug.Log ("Error Reading file:\t"+filename);
			return;
		}


		//start reading
		string redline=null;
		//data used to fill the matrix
		uint spellindexcounter=0;

		while ((redline = effectreader.ReadLine ()) != null) {

			List<float> data=null;
			if (filterLine (redline, out data)) {

				for (int z = 0; z < data.Count; z++) 
					attribvalue [spellindexcounter, z] = data [z];
				
				spellindexcounter++;
			}
		}


	}


	public float getAttrib(uint spellindex,uint dragonindex)
	{
		return attribvalue [spellindex,dragonindex];
	}



	//utility function to filter redline(returns true for any line that is usable)
	private bool filterLine(string redline,out List<float> data)
	{
		redline.Trim ();

		if (redline.Length <=0 || redline [0] != 'i')
		{	
			data = null;
			return false;
		}

		data = getFloatsFromLine (redline.Substring(1,redline.Length-1));
		return true;
	}

	//retrive all floats as a list from the string
	private List<float> getFloatsFromLine(string line)
	{
		List<float> data=new List<float>();

		string store="";

		foreach (char ch in line) {
			if (ch == ',') {
				store.Trim ();
				data.Add (float.Parse(store));
				store = "";
			} else
				store += ch;
		}

		return data;
	}
}
