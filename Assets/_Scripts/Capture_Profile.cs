using UnityEngine;
using System.Collections;

public class Capture_Profile : MonoBehaviour {


	public GameObject release_prefab;

	public GameObject release_point;

	public GameObject way_point;

	public GameObject smoke;

	// if all the task null of destory, then release
	public GameObject[] tasks; 

	// Use this for initialization
	void Start () 
	{
		if (!release_point)
		{
			release_point = gameObject;
		}
		else
		{
			release_point.renderer.enabled = false;
		}

		if (!way_point)
			way_point = gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (tasks.Length == 0)
			return;

		bool task_done = true;
		foreach( GameObject go in tasks)
		{
			if (go)
			{
				task_done = false;
			}
		}

		if (task_done)
		{
			if (release_prefab)
			{
				GameObject go = Instantiate(release_prefab,release_point.transform.position,release_point.transform.rotation) as GameObject;
				AI_Profile ai = go.GetComponent<AI_Profile>();
				if (ai)
					ai.way_point = way_point;
			}

			if (smoke)
				Instantiate(smoke,release_point.transform.position,release_point.transform.rotation);

			gameObject.transform.parent = null;
			Destroy(gameObject);
		}
	}
}
