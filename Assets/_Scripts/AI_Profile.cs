using UnityEngine;
using System.Collections;

public class AI_Profile : MonoBehaviour {

	public float serching_radius = 15.0f;
	float serching_radius_old;

	public GameObject target_emeny;

	public GameObject way_point;
	public GameObject[] boss_way_point;

	public GameObject current_way_point;

	[Range(0,100)]
	public float throw_will, attack_will, combo_will, violent_will, parry_will, dodge_will, chase_will,boss_move_to_way_point_will;

	// Use this for initialization
	void Start () 
	{
		serching_radius_old = serching_radius;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Next_Way_Point()
	{
		if (way_point)
		{
			current_way_point = way_point;
		}
	}

	public void Next_Boss_Way_Point()
	{
		if (boss_way_point.Length >0)
		{
			current_way_point = boss_way_point[Random.Range(0, boss_way_point.Length)];
		}
	}

	public void Reset_Search_Radius()
	{
		serching_radius = serching_radius_old;
	}

	public void Double_Search_Radius()
	{
		serching_radius = serching_radius_old * 2.0f;
	}

	public void Copy_From(AI_Profile ai)
	{
		serching_radius = ai.serching_radius;
		target_emeny = ai.target_emeny;
		way_point = ai.way_point;
		current_way_point = ai.current_way_point;

		throw_will = ai.throw_will;
		attack_will = ai.attack_will;
		combo_will = ai.combo_will;
		violent_will = ai.violent_will;
		parry_will = ai.parry_will;
		dodge_will = ai.dodge_will;
		chase_will = ai.chase_will;

		boss_move_to_way_point_will = ai.boss_move_to_way_point_will;
	}

}
