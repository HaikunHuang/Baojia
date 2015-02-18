using UnityEngine;
using System.Collections;

public class Armor_Profile : MonoBehaviour {

	public string armor_name;
	// the set of name
	public string set_name;

	public string comment;

	// type
	public enum TYPE
	{
		None,
		Boots,
		Brasers,
		Chest,
		Body,
		Hair,
		Leg,
		Panties,
		Shoulder,
		Mask,
		Earring,
		Belt
	}
	public TYPE type;


	// class
	public enum CLAS
	{
		None,
		Elven_Assassin,
		Gnome_Male_worker,
		Elven_Hunter,
		Gnome_Warrior,
		Gnome_Archer
	}
	public CLAS clas;


	public int HP;
	int HP_MAX;

	public int physics;
	public int fire;
	public int ice;
	public int wood;
	public int earth;
	public int metal;

	public AudioClip destory_audio;



	#region System Method

	public Armor_Profile Clone()
	{
		Armor_Profile ret = new Armor_Profile();

		ret.armor_name = armor_name;
		// the set of name
		ret.set_name = set_name;
		
		ret.comment = comment;

		ret.type = type;
		ret.clas = clas;

		ret.HP = HP;
		ret.HP_MAX = HP_MAX;

		ret.physics = physics;
		ret.fire = fire;
		ret.ice = ice;
		ret.wood = wood;
		ret.earth = earth;
		ret.metal = metal;

		ret.destory_audio = destory_audio;
		
		return ret;
	}

	// Use this for initialization
	void Awake () 
	{
		HP_MAX = HP;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void Reset()
	{
		armor_name = gameObject.name;
	}
	#endregion
}
