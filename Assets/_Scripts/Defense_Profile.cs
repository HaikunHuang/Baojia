using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Defense_Profile : MonoBehaviour {

	public int def_physics;
	public int def_fire;
	public int def_ice;
	public int def_wood;
	public int def_earth;
	public int def_metal;

	// trans to Armor profile
	public class ArmorInfo
	{
		public GameObject prefab;
		public GameObject go;
		public Armor_Profile ap;
	}

	#region System Method
	// a list store the all armors;
	public List<ArmorInfo> armors = new List<ArmorInfo>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	#endregion

	#region Calculate Damage 
	public ArmorInfo Calculate_Damage(Damage_Profile dp )
	{
		// if any armor broken, return it, other wise null
		if (!Any_Armor())
			return null;

		
		// each time randomly choose one armor to defense
		ArmorInfo info = armors[Random.Range(0,armors.Count)];

		Armor_Profile ap = info.ap;

		int physics = dp.Get_Physics();
		physics = Mathf.FloorToInt( physics * 1.0f / (physics + ap.physics + def_physics == 0 ? 1 : physics + ap.physics + def_physics ) * physics );
		ap.HP -= physics;

		int fire = dp.Get_Fire();
		fire = Mathf.FloorToInt( fire * 1.0f / (fire + ap.fire + def_fire == 0 ? 1 : fire + ap.fire + def_fire ) * fire );
		ap.HP -= fire;

		int ice = dp.Get_Ice();
		ice = Mathf.FloorToInt( ice * 1.0f / (ice + ap.ice + def_ice == 0 ? 1 : ice + ap.ice + def_ice) * ice );
		ap.HP -= ice;

		int wood = dp.Get_Wood();
		wood = Mathf.FloorToInt( wood * 1.0f / (wood + ap.wood + def_wood == 0 ? 1 : wood + ap.wood + def_wood ) * wood);
		ap.HP -= wood;

		int earth = dp.Get_Earth();
		earth = Mathf.FloorToInt( earth * 1.0f / (earth + ap.earth + def_earth == 0 ? 1 : earth + ap.earth + def_earth ) * earth );
		ap.HP -= earth;

		int metal = dp.Get_Metal();
		metal = Mathf.FloorToInt( metal * 1.0f / (metal + ap.metal + def_metal == 0 ? 1 : metal + ap.metal + def_metal) * metal );
		ap.HP -= metal;

		// destory
		if (ap.HP <=0)
		{
			//GameObject go = info.go;
			armors.Remove(info);
			return info;
		}

		return null;
	}

	// is still have any armor
	public bool Any_Armor()
	{
		return armors.Count > 0 ? true : false;
	}
	#endregion Calculate Damage 

}
