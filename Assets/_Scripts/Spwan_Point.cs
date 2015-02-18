using UnityEngine;
using System.Collections;

public class Spwan_Point : MonoBehaviour {

	public GameObject team_leader_spwan_point;

	public GameObject spwan_object_prefab;

	GameObject spwan_point;
	public GameObject spwan_smoke_prefab;

	public GameObject way_point;
	public GameObject[] boss_Way_point;

	public float	spwan_delay = 120.0f;
	float			ct_time;

	public float	f_player_insight = 20.0f;

	GameObject object_instance;

	Character_Profile my_cp;

	public bool is_one_shot;
	bool shoted = false;

	// get the target which camera following
	Transform should_be_player_target;


	// Use this for initialization
	void Start () 
	{
		gameObject.renderer.enabled = false;

		if (way_point)
			way_point.renderer.enabled = false;

		spwan_point = gameObject;

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

		if (!team_leader_spwan_point)
			team_leader_spwan_point = gameObject;

		if (!way_point)
			way_point = gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
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


		if (team_leader_spwan_point)
		{
			// spwan
			if ((my_cp && my_cp.Get_Is_Death()) 
			    || !my_cp)
			{
				if (ct_time <= 0.0f)
				{
					// player insight 
					float dis_from_player = Vector3.Distance(transform.position, should_be_player_target.position);
					if (dis_from_player <= f_player_insight)
					{
						ct_time = spwan_delay;
						Spwan();
					}
				}
				else
				{
					ct_time -= Time.deltaTime;
				}
			}
		}
		else
		{
			gameObject.transform.parent=null;
			Destroy(gameObject);
		}

		if (is_one_shot && shoted)
		{
			if ((my_cp && my_cp.Get_Is_Death()) 
			    || !my_cp)
			{
				gameObject.transform.parent=null;
				Destroy(gameObject);
			}
		}

	}


	void Spwan()
	{
		if (spwan_object_prefab && !shoted)
		{
			GameObject go = Instantiate(spwan_object_prefab, spwan_point.transform.position, spwan_point.transform.rotation) as GameObject;
			if (spwan_smoke_prefab)
				Instantiate(spwan_smoke_prefab,spwan_point.transform.position, spwan_point.transform.rotation);

			// remove the crop
			if (my_cp)
			{
				//if (spwan_smoke_prefab)
					//Instantiate(spwan_smoke_prefab,my_cp.gameObject.transform.position, my_cp.gameObject.transform.rotation);
				Destroy(my_cp.gameObject);
			}

			go.GetComponent<AI_Profile>().way_point = way_point;
			go.GetComponent<AI_Profile>().boss_way_point = boss_Way_point;
			my_cp = go.GetComponent<Character_Profile>();

			if (is_one_shot)
			{
				shoted = true;
			}
		}
	}
}
