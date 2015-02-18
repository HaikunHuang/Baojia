using UnityEngine;
using System.Collections;

public class Accessory_Profile : MonoBehaviour {

	public string accessory_name;
	public string comment;

	void Reset()
	{
		accessory_name = gameObject.name;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
