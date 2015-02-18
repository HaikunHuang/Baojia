using UnityEngine;
using System.Collections;

public class Unit_FPS_Show : MonoBehaviour {

	float current_time;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (current_time >= 1.0f)
		{
			current_time = 0.0f;
			gameObject.guiText.text = "FPS: " + Mathf.FloorToInt(1.0f / Time.deltaTime);
		}
		else
		{
			current_time += Time.deltaTime;
		}
	}
}
