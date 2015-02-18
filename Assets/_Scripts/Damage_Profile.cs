using UnityEngine;
using System.Collections;

// this file use to represent the damage form, and the values

public class Damage_Profile : MonoBehaviour {

	// type of damage
	public int physics_min,physics_max;
	public int fire_mix,fire_max;
	public int ice_mix,ice_max;
	public int wood_mix,wood_max;
	public int earth_mix, earth_max;
	public int metal_mix, metal_max;

	// heal
	public int heal_mix,heal_max;

	// dot timing
	public bool is_dot;
	public float dot_time;


	public float attack_force;

	// the damage owner and the damage source, represent where the damage comes from
	public GameObject owner, source;

	#region System Method

	public Damage_Profile Clone(float ratio)
	{
		Damage_Profile dp = new Damage_Profile();
		dp.physics_min = Mathf.FloorToInt(physics_min * ratio);
		dp.physics_max = Mathf.FloorToInt(physics_max * ratio);

		dp.fire_mix = Mathf.FloorToInt(fire_mix * ratio);
		dp.fire_max = Mathf.FloorToInt(fire_max * ratio);

		dp.ice_mix = Mathf.FloorToInt(ice_mix * ratio);
		dp.ice_max = Mathf.FloorToInt(ice_max * ratio);

		dp.wood_mix = Mathf.FloorToInt(wood_mix * ratio);
		dp.wood_max = Mathf.FloorToInt(wood_max * ratio);

		dp.earth_mix = Mathf.FloorToInt(earth_mix * ratio);
		dp.earth_max = Mathf.FloorToInt(earth_max * ratio);

		dp.metal_mix = Mathf.FloorToInt(metal_mix * ratio);
		dp.metal_max = Mathf.FloorToInt(metal_max * ratio);

		dp.heal_mix = Mathf.FloorToInt(heal_mix * ratio);
		dp.heal_max = Mathf.FloorToInt(heal_max * ratio);

		dp.attack_force = Mathf.Floor(attack_force * ratio);

		dp.is_dot = is_dot;
		dp.dot_time = dot_time;

		dp.owner = owner;
		dp.source = source;

		return dp;

	}

	public void CopyFrom(Damage_Profile dp)
	{
		physics_min = dp.physics_min;
		physics_max = dp.physics_max;
		
		fire_mix = dp.fire_mix;
		fire_max = dp.fire_max;
		
		ice_mix = dp.ice_mix;
		ice_max = dp.ice_max;
		
		wood_mix = dp.wood_mix;
		wood_max = dp.wood_max; 
		
		earth_mix = dp.earth_mix;
		earth_max = dp.earth_max;
		
		metal_mix = dp.metal_mix; 
		metal_max = dp.metal_max;
		
		heal_mix = dp.heal_mix;
		heal_max = dp.heal_max;
		
		attack_force = dp.attack_force;
		
		is_dot = dp.is_dot; 
		dot_time = dp.dot_time;
		
		owner = dp.owner;
		source = dp.source;
	}

	#endregion System Method

	#region Get Damage Method
	public int Get_Physics()
	{
		return Random.Range(physics_min, physics_max+1);
	}

	public int Get_Fire()
	{
		return Random.Range(fire_mix, fire_max+1);
	}

	public int Get_Ice()
	{
		return Random.Range(ice_mix, ice_max+1);
	}

	public int Get_Wood()
	{
		return Random.Range(wood_mix, wood_max+1);
	}
	public int Get_Earth()
	{
		return Random.Range(earth_mix, earth_max+1);
	}
	public int Get_Metal()
	{
		return Random.Range(metal_mix, metal_max+1);
	}

	public float Get_Attack_force()
	{
		return attack_force;
	}


	#endregion Get Damage Method

	#region Get Heal Method
	public int Get_Heal()
	{
		return Random.Range(heal_mix,heal_max);
	}
	#endregion Get Heal Method
}
