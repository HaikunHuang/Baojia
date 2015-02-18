using UnityEngine;
using System.Collections;

public class UI_Preview_Profile : MonoBehaviour {

	public UITexture model_viewer;
	public Transform model_trans;
	public UILabel label;


	// Use this for initialization
	void Start () 
	{
		if (model_viewer)
		{
			//model_viewer.mainTexture = null;
		}

		if (label)
		{
			label.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
