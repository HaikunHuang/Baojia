using UnityEngine;
using System.Collections;

public class Destory_By_Time : MonoBehaviour {

	public float time = 300.0f;
	// Use this for initialization
	bool del = false;
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		if (!del)
		{
			DestroyObject(gameObject,time);
			del = true;
		}
	}
}
