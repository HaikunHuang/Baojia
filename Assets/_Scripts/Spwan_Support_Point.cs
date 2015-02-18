using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Spwan_Support_Point : MonoBehaviour {


	public GameObject spwan_object_prefab;
	
	public GameObject spwan_point;
	public GameObject spwan_smoke_prefab;
	
	public GameObject way_point;
	
	public float	spwan_delay = 120.0f;
	float			ct_time;


	Character_Profile player_cp, my_cp;
	
	// get the target which camera following
	Transform should_be_player_target;
	
	
	// Use this for initialization
	void Start () 
	{
		if (spwan_point)
			spwan_point.renderer.enabled = false;
		if (way_point)
			way_point.renderer.enabled = false;
		
		ct_time = 0.0f;

		if (!should_be_player_target)
		{
			GameObject player = GameObject.FindObjectOfType<Player_Controller>().gameObject;
			if (!player)
			{
				Debug.Log("PLayer need a Player Controller");
				should_be_player_target = transform;
			}
			else
			{
				should_be_player_target = player.transform;
			}
		}
		else
		{
			
		}
		
		player_cp = should_be_player_target.gameObject.GetComponent<Character_Profile>();
		if (!player_cp)
		{
			Debug.Log("Player Need A Character_Profile");
		}
		// spwan
		//Spwan();
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider other) 
	{
		if (! other.gameObject.transform.Equals(should_be_player_target))
			return;

		if (!should_be_player_target)
		{
			GameObject player = GameObject.FindObjectOfType<Player_Controller>().gameObject;
			if (!player)
			{
				Debug.Log("PLayer need a Player Controller");
				should_be_player_target = transform;
			}
			else
			{
				should_be_player_target = player.transform;
			}
		}
		else
		{
			
		}

		if (!player_cp)
		{
			player_cp = should_be_player_target.gameObject.GetComponent<Character_Profile>();
		}

		if (player_cp.Get_Is_Death())
		{
			Destroy(gameObject);
		}
		else
		{
			// spwan
			if ((my_cp && my_cp.Get_Is_Death()) 
			    || !my_cp)
			{
				if (ct_time <= 0.0f)
				{
					ct_time = spwan_delay;
					Spwan();

				}
				else
				{
					ct_time -= Time.deltaTime;
				}
			}
		}
		
		
	}
	
	
	void Spwan()
	{
		if (spwan_object_prefab && spwan_point)
		{
			GameObject go = Instantiate(spwan_object_prefab, spwan_point.transform.position, spwan_point.transform.rotation) as GameObject;
			if (spwan_smoke_prefab)
				Instantiate(spwan_smoke_prefab,spwan_point.transform.position, spwan_point.transform.rotation);
			
			// remove the crop
			if (my_cp)
			{
				//if (spwan_smoke_prefab)
				//	Instantiate(spwan_smoke_prefab,my_cp.gameObject.transform.position, my_cp.gameObject.transform.rotation);
				Destroy(my_cp.gameObject);
			}
			
			go.GetComponent<AI_Profile>().way_point = way_point;
			my_cp = go.GetComponent<Character_Profile>();
		}
	}
}
