using UnityEngine;
using System.Collections;

public class Weapon_Profile : MonoBehaviour {

	public string weapon_name;
	// the set of name
	public string set_name;
	public string comment;

	
	// type
	/*public enum TYPE
	{
		None,
		Dual
	}
	public TYPE type;
	*/
	public Character_Profile.Weapon_Mode weapon_mode;
	// class
	/*public enum CLAS
	{
		None,
		Elven_Assassin
	}
	public CLAS clas;
	*/

	public GameObject 	weapon_main_prefab, weapon_second_prefab;
	public GameObject	arrow_ready_prefab,arrow_shoot_prefab;
	public GameObject	weapon_attack_fx_prefab;

	// type of damage
	public int physics_min,physics_max;
	public int fire_mix,fire_max;
	public int ice_mix,ice_max;
	public int wood_mix,wood_max;
	public int earth_mix, earth_max;
	public int metal_mix, metal_max;

	public float radius = 2.0f;
	public float attack_force = 50.0f;

	public int def_physics;
	public int def_fire;
	public int def_ice;
	public int def_wood;
	public int def_earth;
	public int def_metal;

	//public float speed = 1.0f; // this value will effecr the animation speed. ex [anim.speed = 1.0f/speed];
	
	#region System Method
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void Reset()
	{
		weapon_name = gameObject.name;
	}
	#endregion System Method
}
