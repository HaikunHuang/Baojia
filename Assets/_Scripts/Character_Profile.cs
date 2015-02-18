using UnityEngine;
using System.Collections;

// This file use attack to the character game object to descript the character
// also to manager the character's states and updata the animation automatically

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Damage_Profile))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Backpack_Profile))]
[RequireComponent(typeof(Defense_Profile))]
[RequireComponent(typeof(NavMeshAgent))]
public class Character_Profile : MonoBehaviour {
	
	public Name_System.Name_Type game_name_type;
	public string game_name, game_title;
	public GameObject texter_prefab;
	public float f_texter_fix_y = 2.2f;
	GameObject texter_instance;
	public bool is_show_texter = true;

	// if tough, that the charactor will not play hurt when get hurt.
	public bool is_tough;

	// if trample when get up, damge around
	public bool is_trample;
	// if boss, it will attack atound when it move at the footsetp event
	public bool is_boss_move_to_way_point {set;get;}


	public enum Weapon_Mode
	{
		Off,
		Main,
		SS, // Sword_Shield
		Dual,
		Range
	};
	Weapon_Mode current_weapon_mode;
	public Weapon_Mode weapon_class;
	public bool weapon_mode_force_weapon_ready;
	// **********************************************************//
	#region Base
	public float walk_speed, run_speed;
	//float	current_speed;
	#endregion Base

	#region Body Part 
	// *** only use the body trans to represented armor gameobject *** //
	public GameObject 	boots,brasers,chest,body,hair,leg_armor,panties,shoulder_armor,mask,earrings,belt;
	public Transform 	follow_position;
	public Transform	left_foot_trans,right_foot_trans;
	#endregion Body Part

	// **********************************************************//
	#region Weapon Part
	GameObject 	weapon_main_prefab, weapon_second_prefab;
	public Transform	weapon_main_back_trans, weapon_second_back_trans;
	public Transform	weapon_main_ready_trans, weapon_second_ready_trans;
	public AudioClip 	weapon_audio_back, weapon_audio_ready;
	#endregion Weapon Part

	// **********************************************************//
	#region Actions Part
	public AudioClip	motion_audio_left_footsetp, motion_audio_right_footsetp;
	public AudioClip	action_audio_drinking, action_audio_making,
						action_audio_picking, action_audio_looking,
						action_audio_speaking;
	#endregion Actions Part

	#region FX
	public GameObject	footsetp_dust_fx_prefab;
	public bool 		is_allow_footsetp_fx;
	public GameObject	attack_move_dust_fx_prefab, dodge_dust_fx_prefab;
	public GameObject	fight_knock_down_fx_prefab;
	public GameObject	blood_fx_prefab;
	public GameObject[]	blood_fx_on_ground_prefab;
	public Transform	blood_fx_trans;
	GameObject	weapon_attack_fx_prefab;
	public GameObject	weapon_parry_fx_prefab;
	public GameObject	armor_destory_fx_prefab;
	#endregion FX

	// **********************************************************//
	#region Fight Part
	public AudioClip[]	fight_attack_audios;
	public AudioClip	fight_attack_hit_body_audio;
	public AudioClip[]	fight_attack_critical_hit_audios;
	[Range(0.0f,100.0f)]
	float		fight_attack_critical_hit_audios_precent = 5.0f;
	public AudioClip[]	fight_charing_audios;
	[Range(0.0f,100.0f)]
	float		fight_charing_audios_precent = 5.0f;
	public AudioClip[]	fight_get_kill_aduios;
	public AudioClip[]	fight_hurt_audios;
	public AudioClip[]	fight_death_audios;
	public AudioClip	fight_roar_audios_1,fight_roar_audios_2,fight_roar_audios_3;
	public AudioClip	fight_parry_weapon_audio, weapon_audio_parry_apply;
	//public AudioClip	fight_dodge_audio;
	public float		f_fight_dodge_force = 360.0f;
	public GameObject	throw_object_prefab;
	public float		f_throw_direction_up_fix = 0.3f; 
	public float		f_throw_force = 10.0f;
	public AudioClip	fight_knock_down_impact_to_ground_audio;

	public AudioClip	fight_attack_weapon_audio_1,fight_attack_weapon_audio_2,fight_attack_weapon_audio_3,
						fight_attack_weapon_audio_4,fight_attack_weapon_audio_5,fight_attack_weapon_audio_6,
	fight_attack_weapon_audio_7,fight_attack_weapon_audio_8,fight_run_attack_weapon_audio,fight_attack_arrow_audio;
	public float		f_fight_attack_move_force_1,f_fight_attack_move_force_2,f_fight_attack_move_force_3,
						f_fight_attack_move_force_4,f_fight_attack_move_force_5,f_fight_attack_move_force_6,
	f_fight_attack_move_force_7,f_fight_attack_move_force_8,f_fight_run_attack_move_force,f_fight_shooting_force = 50.0f;

	// not use any more
	float		f_fight_attack_radius_1,f_fight_attack_radius_2,f_fight_attack_radius_3,
						f_fight_attack_radius_4,f_fight_attack_radius_5,f_fight_attack_radius_6,
	f_fight_attack_radius_7,f_fight_attack_radius_8,f_fight_run_attack_radius;
	// *** *** *** //
	public float f_fight_run_trample_radius = 1.0f;
	public float f_fight_repluse_radius = 2.0f, f_fight_repluse_force = 1200.0f;

	public float		f_fight_attack_angle_1,f_fight_attack_angle_2,f_fight_attack_angle_3,
						f_fight_attack_angle_4,f_fight_attack_angle_5,f_fight_attack_angle_6,
	f_fight_attack_angle_7,f_fight_attack_angle_8,f_fight_run_attack_angle;
	public float 		f_parry_angle_limit = 60.0f;
	public float 		f_fight_attack_scale_1 = 1.0f,f_fight_attack_scale_2 = 1.0f,f_fight_attack_scale_3 = 1.0f,
						f_fight_attack_scale_4 = 1.0f,f_fight_attack_scale_5 = 1.0f,f_fight_attack_scale_6 = 1.0f,
	f_fight_attack_scale_7 = 1.0f,f_fight_attack_scale_8 = 1.0f,f_fight_run_attack_scale = 1.0f,
	f_fight_run_trample_boss_scale = 1.0f, f_fight_attack_arrow_scale = 1.0f;

	float f_fight_parry_attack_scale= 0.0f;

	public float		f_trample_force = 300.0f, f_trample_radius = 1.0f;
	#endregion Fight Part

	// **********************************************************//
	#region Private
	Animation 			anim;
	AudioSource 		audioSource;
	Damage_Profile 		damage_profile;	
	Backpack_Profile 	backpack_profile;
	Weapon_Profile		weapon_profile;
	Defense_Profile		defense_profile;
	// weapon 
	GameObject weapon_main_instance, weapon_second_instance;

	public Weapon_Mode Get_Weapon_Mode(){return current_weapon_mode;}

	bool is_can_combo;
	public bool Get_Is_Can_Combo(){return is_can_combo;}

	bool is_parry;
	public bool Is_Parry(){return is_parry;}

	bool is_dodge;
	public bool Is_Dodge(){return is_dodge;}

	bool is_death;
	public bool Get_Is_Death(){return is_death;}

	//public int HP = 1;
	//public int Get_HP(){return HP;}

	float f_fade_time = 0.3f; // animation transition time

	// for attack event
	float f_next_attack_move_force;
	float f_next_attack_damage_scale = 1.0f;
	float f_next_attack_damage_radius;
	public float Get_Next_Attack_Radius(){return f_next_attack_damage_radius;}
	float f_next_attack_damage_angle;
	public float Get_Next_Attack_Angle(){return f_next_attack_damage_angle;}
	float f_weapon_radius;
	public float Get_Weapon_Radius(){return f_weapon_radius;}



	//float f_current_knock_down_value;
	float f_knock_down_max_limit = 30.0f;
	float f_knock_down_recover_per_sceond = 5.0f;

	float f_drop_weapon_force = 5.0f;

	public float f_delay_to_shut_down_this = 40.0f;

	//

	bool is_player;
	public bool Is_Player() {return is_player;}
	// ************* //
	
	public bool Debug_Animation_Event;
	#endregion Private


	// **********************************************************//
	#region System Method
	// Use this for initialization
	void Start () 
	{
		// name
		Creat_Name();

		anim = gameObject.GetComponent<Animation>();
		audioSource = gameObject.GetComponent<AudioSource>();
		damage_profile = gameObject.GetComponent<Damage_Profile>();
		damage_profile.owner = gameObject;
		damage_profile.source = gameObject;

		backpack_profile = gameObject.GetComponent<Backpack_Profile>();

		if (!backpack_profile.weapon_set_prefab)
		{
			Debug.Log(gameObject.name + " need a weapon set.");
		}
		weapon_profile = backpack_profile.weapon_set_prefab.GetComponent<Weapon_Profile>();

		defense_profile = gameObject.GetComponent<Defense_Profile>();

		// get the weapon
		Start_Weapon();
		// get the armor matrial 
		Start_Armor();

		//weapon
		current_weapon_mode = Weapon_Mode.Off;
		Weapon_Init();

		// if emeny, force weapon ready
		if (weapon_mode_force_weapon_ready)
		{
			Force_Weapon_Ready();
		}

		// is player
		if (gameObject.GetComponent<Player_Controller>())
		{
			is_player = true;
		}
	}



	// Get Name
	void Creat_Name()
	{
		if (!is_show_texter)
			return;

		// get name
		if (game_name.Length == 0)
			game_name = Name_System.Get_Name(game_name_type);

		if (texter_prefab)
		{
			texter_instance = Instantiate(texter_prefab,transform.position,transform.rotation) as GameObject;
			texter_instance.transform.parent = transform;
			texter_instance.transform.position += new Vector3(0,f_texter_fix_y,0);

			Camera_Facing_Billboard cfb = texter_instance.GetComponent<Camera_Facing_Billboard>();
			cfb.name_text.text = game_name;
			cfb.title_text.text = game_title;

		}

	}

	//

	void Force_Weapon_Ready()
	{
		Weapon_Main_Binding_To_Ready();
		Weapon_Second_Binding_To_Ready();
		current_weapon_mode = weapon_class;

		if (anim)
		{
			anim.Stop();
			Reset_to_Idle();
		}
	}

	void Force_Weapon_Back()
	{
		Weapon_Main_Binding_To_Back();
		Weapon_Second_Binding_To_Back();
		current_weapon_mode = Weapon_Mode.Off;
	}

	void Start_Weapon()
	{
		weapon_main_prefab = weapon_profile.weapon_main_prefab;
		if (weapon_main_prefab)
			weapon_main_prefab.transform.localScale = gameObject.transform.localScale;

		weapon_second_prefab = weapon_profile.weapon_second_prefab;
		if (weapon_second_prefab)
			weapon_second_prefab.transform.localScale = gameObject.transform.localScale;

		weapon_attack_fx_prefab = weapon_profile.weapon_attack_fx_prefab;

		// accept the data to damage profile
		// type of damage
		damage_profile.physics_min = weapon_profile.physics_min;
		damage_profile.physics_max = weapon_profile.physics_max;
		damage_profile.fire_mix = weapon_profile.fire_mix;
		damage_profile.fire_max = weapon_profile.fire_max;
		damage_profile.ice_mix = weapon_profile.ice_mix;
		damage_profile.ice_max = weapon_profile.ice_max;
		damage_profile.wood_mix = weapon_profile.wood_mix;
		damage_profile.wood_max = weapon_profile.wood_max;
		damage_profile.earth_mix = weapon_profile.earth_mix;
		damage_profile.earth_max = weapon_profile.earth_max;
		damage_profile.metal_mix = weapon_profile.metal_mix;
		damage_profile.metal_max = weapon_profile.metal_max;

		// def
		defense_profile.def_physics = weapon_profile.def_physics;
		defense_profile.def_fire = weapon_profile.def_fire;
		defense_profile.def_ice = weapon_profile.def_ice;
		defense_profile.def_wood = weapon_profile.def_wood;
		defense_profile.def_earth = weapon_profile.def_earth;
		defense_profile.def_metal = weapon_profile.def_metal;

		// radius
		f_weapon_radius = weapon_profile.radius;

		damage_profile.attack_force = weapon_profile.attack_force;
	}

	void Start_Armor()
	{
		// the real armor with the item profile.
		//boots,brasers,chest,leg_armor,panties,shoulder_armor,mask,earrings;
		if (boots && backpack_profile.boots)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.boots) as GameObject;
			boots.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = boots;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.boots;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (boots)
			{
				boots.SetActive(false);
			}
		}
		if (brasers && backpack_profile.brasers)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.brasers) as GameObject;
			brasers.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = brasers;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.brasers;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (brasers)
			{
				brasers.SetActive(false);
			}
		}
		if (chest && backpack_profile.chest)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.chest) as GameObject;
			chest.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = chest;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.chest;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (chest)
			{
				chest.SetActive(false);
			}
		}
		if (leg_armor && backpack_profile.leg_armor)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.leg_armor) as GameObject;
			leg_armor.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = leg_armor;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.leg_armor;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (leg_armor)
			{
				leg_armor.SetActive(false);
			}
		}
		if (panties && backpack_profile.panties)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.panties) as GameObject;
			panties.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = panties;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.panties;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (panties)
			{
				panties.SetActive(false);
			}
		}
		if (shoulder_armor && backpack_profile.shoulder_armor)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.shoulder_armor) as GameObject;
			shoulder_armor.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = shoulder_armor;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.shoulder_armor;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (shoulder_armor)
			{
				shoulder_armor.SetActive(false);
			}
		}
		if (mask && backpack_profile.mask)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.mask) as GameObject;
			mask.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = mask;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.mask;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (mask)
			{
				mask.SetActive(false);
			}
		}
		if (earrings && backpack_profile.earrings)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.earrings) as GameObject;
			earrings.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = earrings;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.earrings;
			defense_profile.armors.Add(armorInfo);

			Destroy(go);
		}
		else
		{
			if (earrings)
			{
				earrings.SetActive(false);
			}
		}
		if (belt && backpack_profile.belt)
		{
			// Material 
			GameObject go = Instantiate(backpack_profile.belt) as GameObject;
			belt.renderer.material.CopyPropertiesFromMaterial(go.renderer.material);
			// create a armor info
			Defense_Profile.ArmorInfo armorInfo = new Defense_Profile.ArmorInfo ();
			armorInfo.go = belt;
			armorInfo.ap = go.GetComponent<Armor_Profile>().Clone();
			armorInfo.prefab = backpack_profile.earrings;
			defense_profile.armors.Add(armorInfo);
			
			Destroy(go);
		}
		else
		{
			if (belt)
			{
				belt.SetActive(false);
			}
		}
	}

	void Auto_Death_Drop_Weapon()
	{
		if (is_death)
		{
			if (weapon_main_instance)
			{
				Drop_Equipment(weapon_main_instance);
				weapon_main_instance = null;
			}
			if (weapon_second_instance)
			{
				Drop_Equipment(weapon_second_instance);
				weapon_second_instance = null;
			}
		}
	}

	void Drop_Equipment(GameObject go)
	{
		if (go == null)
			return;
		go.transform.localScale = gameObject.transform.localScale;
		go.transform.parent = null;
		// armor has a box collider, but weapon does not.
		go.AddMissingComponent<BoxCollider>();
//		go.GetComponent<BoxCollider>().size = go.GetComponent<BoxCollider>().size * 0.2f;
		go.collider.enabled = true;
		go.collider.isTrigger = true;
		go.AddMissingComponent<Rigidbody>();
		go.rigidbody.useGravity = true;
		go.AddMissingComponent<Item_Drop_To_Static>();
		//go.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		go.transform.Rotate(
			Random.Range(0.0f,360.0f),
			Random.Range(0.0f,360.0f),
			Random.Range(0.0f,360.0f)
			);
		// add force
		go.rigidbody.AddForce(
			Random.Range(-f_drop_weapon_force,f_drop_weapon_force),
			Random.Range(-f_drop_weapon_force,f_drop_weapon_force * 1.5f),
			Random.Range(-f_drop_weapon_force,f_drop_weapon_force),
			ForceMode.Impulse
			);
		go.rigidbody.AddRelativeTorque(
			Random.Range(-f_drop_weapon_force,f_drop_weapon_force),
			Random.Range(-f_drop_weapon_force,f_drop_weapon_force),
			Random.Range(-f_drop_weapon_force,f_drop_weapon_force),
			ForceMode.Impulse
			);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if the animation is done, then replay the idle animation
		Auto_Reset();

		Auto_Death_Drop_Weapon();

	}

	// if the animation is done, then replay the idle animation
	void Auto_Reset()
	{
		// reset rotate, fixed the x-axis and z-axis.
		transform.rotation = Quaternion.Euler(0,transform.eulerAngles.y,0);

		// Idle
		if (!anim.isPlaying && !is_death)
		{
			Reset_to_Idle();
		}

		// death
		if (is_death)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
				
			rigidbody.useGravity = false;
			collider.enabled = false;

			// reset the texter position
			if (texter_instance)
			{
				Vector3 newPos = transform.position;
				newPos.y += f_texter_fix_y * 0.3f;
				texter_instance.transform.position = newPos;
			}

			// shut down my self
			f_delay_to_shut_down_this -= Time.deltaTime;
			if (f_delay_to_shut_down_this <= 0.0f)
			{
				if (texter_instance)
					texter_instance.SetActive(false);
				this.enabled = false;
			}

		}
		// auto stop movment
		Auto_Stoppable();


	}

	void OnCollisionEnter(Collision collision) 
	{

		if (collision.gameObject.layer == gameObject.layer)
		{
			//Debug.Log("OnCollisionEnter");
			// both stop
			//gameObject.rigidbody.angularVelocity = Vector3.zero;
			//gameObject.rigidbody.velocity = Vector3.zero;

			//collider.gameObject.rigidbody.angularVelocity = Vector3.zero;
			//collider.gameObject.rigidbody.velocity = Vector3.zero;

			Vector3 dir = transform.position - collision.gameObject.transform.position;
			dir.y = 0;
			dir.Normalize();
			rigidbody.AddForce(dir,ForceMode.Impulse);
		}
	}

	void OnCollisionStay(Collision collision) 
	{
		OnCollisionEnter(collision);
	}

	void Reset_All_Combo_Bool()
	{
		is_can_combo = false;
		is_dodge = false;
		is_parry = false;
		//is_boss_move_to_way_point = false;
	}

	void Auto_Stoppable()
	{
		if (!Is_Dodge() && !Is_Hurt() && !Is_Attack())
		{
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
		}

	}

	void Reset_to_Idle()
	{	
		
		Reset_All_Combo_Bool();
		gameObject.rigidbody.angularVelocity = Vector3.zero;
		gameObject.rigidbody.velocity = Vector3.zero;

		if (!anim)
			return;

		// *** more weapon add here *** //
		switch (current_weapon_mode)
		{
		case Weapon_Mode.Dual:
			anim.CrossFade("Dual Idle",f_fade_time);
			break;
		case Weapon_Mode.Main:
			anim.CrossFade("Main Idle",f_fade_time);
			break;
		case Weapon_Mode.Range:
			anim.CrossFade("Range Idle",f_fade_time);
			//show the arrow trans
			Weapon_Main_Binding_To_Ready();

			break;
		case Weapon_Mode.SS:
			anim.CrossFade("SS Idle",f_fade_time);
			break;
			
		default :
			anim.CrossFade("Idle",f_fade_time);
			current_weapon_mode = Weapon_Mode.Off;
			break;
		}
	}

	public bool Is_Weapon_On()
	{
		return current_weapon_mode != Weapon_Mode.Off;
	}

	public bool Is_Run()
	{
		return anim.IsPlaying("Run");
	}

	public bool Is_Real_Idle()
	{
		if (anim.IsPlaying("Idle"))
			return true;
		
		//if (anim.IsPlaying("Walk"))
			//return true;
		
		//if (anim.IsPlaying("Run"))
			//return true;

		if (anim.IsPlaying("Dual Idle"))
			return true;

		if (anim.IsPlaying("Main Idle"))
			return true;

		if (anim.IsPlaying("Range Idle"))
			return true;

		if (anim.IsPlaying("SS Idle"))
			return true;
		// more weapon ready Idle add here

		return false;
	}

	public bool Is_Idle()
	{
		if (!anim)
			return true;

		if (anim.IsPlaying("Idle"))
			return true;
		
		if (anim.IsPlaying("Walk"))
			return true;
		
		if (anim.IsPlaying("Run"))
			return true;
		
		if (anim.IsPlaying("Dual Idle"))
			return true;

		if (anim.IsPlaying("Main Idle"))
			return true;

		if (anim.IsPlaying("Range Idle"))
			return true;

		if (anim.IsPlaying("SS Idle"))
			return true;
		
		// more weapon ready Idle add here
		
		return false;
	}

	public bool Is_Throw()
	{
		if (anim.IsPlaying("Throw"))
			return true;

		return false;
	}
	
	public bool Is_Attack()
	{

		if (anim.IsPlaying("Attack 1"))
			return true;
		if (anim.IsPlaying("Attack 2"))
			return true;
		if (anim.IsPlaying("Attack 3"))
			return true;
		if (anim.IsPlaying("Attack 4"))
			return true;
		if (anim.IsPlaying("Attack 5"))
			return true;
		if (anim.IsPlaying("Attack 6"))
			return true;
		if (anim.IsPlaying("Attack 7"))
			return true;
		if (anim.IsPlaying("Attack 8"))
			return true;
		if (anim.IsPlaying("Run Attack Dual"))
			return true;
		if (anim.IsPlaying("Run Attack Main"))
			return true;
		if (anim.IsPlaying("Shooting"))
			return true;
		
		// more attack add here
		
		return false;
	}

	public bool Is_Hurt()
	{
		//if (anim.IsPlaying("Down Backward"))
		//	return true;
		//if (anim.IsPlaying("Down Forward"))
			//return true;
		if (anim.IsPlaying("Hit From Back"))
			return true;
		if (anim.IsPlaying("Hit From Front"))
			return true;
		//if (anim.IsPlaying("Knock Backward"))
			//return true;
		//if (anim.IsPlaying("Knock Forward"))
			//return true;

		return false;
	}

	public bool Is_Ready_For_Recover()
	{
		if (anim.IsPlaying("Down Backward"))
			return true;
		if (anim.IsPlaying("Down Forward"))
			return true;

		return false;
	}
	 


	public bool Is_Down()
	{
		if (anim.IsPlaying("Down Backward"))
			return true;
		if (anim.IsPlaying("Down Forward"))
			return true;
		if (anim.IsPlaying("Knock Backward"))
			return true;
		if (anim.IsPlaying("Knock Forward"))
			return true;
		
		return false;
	}

	// is it ready for next animation play
	public bool Is_Ready_To_Play_Next()
	{
	//	if (is_death)
	//		return false;

		if (is_can_combo)
		{
			//is_can_combo = false;
			return true;
		}

		return Is_Idle();
	}

	void Set_Death()
	{
		if (!is_death)
		{
			int index = Random.Range(0,2);

			switch (index)
			{
			case 0:
				Play_Dying_1();
				break;
			case 1:
				Play_Dying_2();
				break;
			default:
				Play_Dying_1();
				break;
			}
		}
	}

	void Critical_Hit_Audio()
	{
		if (fight_attack_critical_hit_audios.Length > 0)
		{
			if (Random.Range(0,100.0f) < fight_attack_critical_hit_audios_precent)
			{
				int index = Random.Range(0,fight_attack_critical_hit_audios.Length);
				audioSource.PlayOneShot(fight_attack_critical_hit_audios[index]);
			}
		}
	}

	void Charing_Audio()
	{
		if (fight_charing_audios.Length > 0)
		{
			if (Random.Range(0,100.0f) < fight_charing_audios_precent)
			{
				int index = Random.Range(0,fight_charing_audios.Length);
				audioSource.PlayOneShot(fight_charing_audios[index]);
			}

		}
	}

	void Get_Kill_Audio()
	{
		if (fight_get_kill_aduios.Length > 0)
		{
			int index = Random.Range(0,fight_get_kill_aduios.Length);
			audioSource.PlayOneShot(fight_get_kill_aduios[index]);
			
		}
	}

	public void Reset_Weapon()
	{
		// get the weapon
		Start_Weapon();
		Weapon_Init();
		
		// if emeny, force weapon ready
		if (weapon_mode_force_weapon_ready)
		{
			Force_Weapon_Ready();
		}
	}
	#endregion System Method

	// **********************************************************//
	#region Weapon Method
	// create weapons and binding to back position
	void Weapon_Init()
	{
		if (weapon_main_prefab)
		{
			if (weapon_main_instance)
				Destroy(weapon_main_instance);
			weapon_main_instance = GameObject.Instantiate(weapon_main_prefab) as GameObject;
			weapon_main_instance.transform.parent = weapon_main_back_trans;
			weapon_main_instance.transform.position = weapon_main_back_trans.position;
			weapon_main_instance.transform.rotation = weapon_main_back_trans.rotation;

		}

		// arrow
		if (weapon_profile.arrow_ready_prefab)
		{
			if (weapon_main_instance)
				Destroy(weapon_main_instance);
			weapon_main_instance = GameObject.Instantiate(weapon_profile.arrow_ready_prefab) as GameObject;
			weapon_main_instance.transform.parent = weapon_main_ready_trans;
			weapon_main_instance.transform.position = weapon_main_ready_trans.position;
			weapon_main_instance.transform.rotation = weapon_main_ready_trans.rotation;
		}


		if (weapon_second_prefab)
		{
			if (weapon_second_instance)
				Destroy(weapon_second_instance);
			weapon_second_instance = GameObject.Instantiate(weapon_second_prefab) as GameObject;
			weapon_second_instance.transform.parent = weapon_second_back_trans;
			weapon_second_instance.transform.position = weapon_second_back_trans.position;
			weapon_second_instance.transform.rotation = weapon_second_back_trans.rotation;
		}
	}

	void Weapon_Main_Binding_To_Back()
	{
		if (!weapon_profile.arrow_ready_prefab && weapon_main_instance)
		{
			weapon_main_instance.transform.parent = weapon_main_back_trans;
			weapon_main_instance.transform.position = weapon_main_back_trans.position;
			weapon_main_instance.transform.rotation = weapon_main_back_trans.rotation;
			
		}

		// arrow
		if (weapon_profile.arrow_ready_prefab && weapon_main_instance)
		{
			if (weapon_main_instance)
			{
				Destroy(weapon_main_instance);
			}
		}
	}

	void Weapon_Main_Binding_To_Ready()
	{
		if (!weapon_profile.arrow_ready_prefab && weapon_main_instance)
		{
			weapon_main_instance.transform.parent = weapon_main_ready_trans;
			weapon_main_instance.transform.position = weapon_main_ready_trans.position;
			weapon_main_instance.transform.rotation = weapon_main_ready_trans.rotation;
			
		}

		// arrow
		if (weapon_profile.arrow_ready_prefab && weapon_main_instance == null)
		{
			weapon_main_instance = GameObject.Instantiate(weapon_profile.arrow_ready_prefab) as GameObject;
			weapon_main_instance.transform.parent = weapon_main_ready_trans;
			weapon_main_instance.transform.position = weapon_main_ready_trans.position;
			weapon_main_instance.transform.rotation = weapon_main_ready_trans.rotation;
		}
	}

	void Weapon_Second_Binding_To_Back()
	{
		if (weapon_second_instance)
		{
			weapon_second_instance.transform.parent = weapon_second_back_trans;
			weapon_second_instance.transform.position = weapon_second_back_trans.position;
			weapon_second_instance.transform.rotation = weapon_second_back_trans.rotation;
		}
	}

	void Weapon_Second_Binding_To_Ready()
	{
		if (weapon_second_instance)
		{
			weapon_second_instance.transform.parent = weapon_second_ready_trans;
			weapon_second_instance.transform.position = weapon_second_ready_trans.position;
			weapon_second_instance.transform.rotation = weapon_second_ready_trans.rotation;
		}
	}
	#endregion Weapon Method

	// **********************************************************//
	#region Animation Event
	void Anim_Event_Dual_Back()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dual_Back");

		current_weapon_mode = Weapon_Mode.Off;
		Weapon_Main_Binding_To_Back();
		Weapon_Second_Binding_To_Back();
	}
	void Anim_Event_Main_Back()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Main_Back");
		
		current_weapon_mode = Weapon_Mode.Off;
		Weapon_Main_Binding_To_Back();
	}
	void Anim_Event_Range_Back()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Range_Back");
		
		current_weapon_mode = Weapon_Mode.Off;
		Weapon_Main_Binding_To_Back();
		Weapon_Second_Binding_To_Back();
	}
	void Anim_Event_SS_Back()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_SS_Back");
		
		current_weapon_mode = Weapon_Mode.Off;
		Weapon_Main_Binding_To_Back();
		Weapon_Second_Binding_To_Back();
	}

	void Anim_Event_Dual_Ready()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dual_Ready");

		current_weapon_mode = Weapon_Mode.Dual;
		Weapon_Main_Binding_To_Ready();
		Weapon_Second_Binding_To_Ready();
	}
	void Anim_Event_Main_Ready()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Main_Ready");
		
		current_weapon_mode = Weapon_Mode.Main;
		Weapon_Main_Binding_To_Ready();
	}
	void Anim_Event_Range_Ready()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Range_Ready");
		
		current_weapon_mode = Weapon_Mode.Range;
		Weapon_Main_Binding_To_Ready();
		Weapon_Second_Binding_To_Ready();
	}
	void Anim_Event_SS_Ready()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Range_Ready");
		
		current_weapon_mode = Weapon_Mode.SS;
		Weapon_Main_Binding_To_Ready();
		Weapon_Second_Binding_To_Ready();
	}

	void Anim_Event_Dual_Audio_Back()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dual_Audio_Back");

		if (weapon_audio_back)
			audioSource.PlayOneShot(weapon_audio_back);
	}

	void Anim_Event_Main_Audio_Back()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Main_Audio_Back");
		
		if (weapon_audio_back)
			audioSource.PlayOneShot(weapon_audio_back);
	}

	void Anim_Event_Dual_Audio_Ready()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dual_Audio_Ready");
		
		if (weapon_audio_ready)
			audioSource.PlayOneShot(weapon_audio_ready);
	}

	void Anim_Event_Main_Audio_Ready()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Main_Audio_Ready");
		
		if (weapon_audio_ready)
			audioSource.PlayOneShot(weapon_audio_ready);
	}

	void Anim_Event_Boss_Run_Damage_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Boss_Run_Damage_Apply");

		if (is_boss_move_to_way_point)
		{

			f_next_attack_damage_radius = f_fight_run_trample_radius;
			f_next_attack_damage_angle = 180.0f;
			f_next_attack_damage_scale = f_fight_run_trample_boss_scale;
			DP_Do_Explosive_To_Targets_Around();
		}
	}

	void Anim_Event_Motion_Left_FootSetp()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Motion_Left_FootSetp");

		if (motion_audio_left_footsetp)
			audioSource.PlayOneShot(motion_audio_left_footsetp);

		if (left_foot_trans && footsetp_dust_fx_prefab && is_allow_footsetp_fx)
		{
			FX_Creator(footsetp_dust_fx_prefab,left_foot_trans);
		}
	}

	void Anim_Event_Motion_Right_FootSetp()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Motion_Right_FootSetp");

		if (motion_audio_right_footsetp)
			audioSource.PlayOneShot(motion_audio_right_footsetp);

		if (right_foot_trans && footsetp_dust_fx_prefab && is_allow_footsetp_fx)
		{
			FX_Creator(footsetp_dust_fx_prefab,right_foot_trans);
		}
	}

	void Anim_Event_Drinking_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Drinking_Audio");

		if (action_audio_drinking)
			audioSource.PlayOneShot(action_audio_drinking);
	}

	void Anim_Event_Drinking_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Drinking_Apply");
		// *** add apply effect here *** //

	}

	void Anim_Event_Making_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Making_Aduio");
		
		if (action_audio_making)
			audioSource.PlayOneShot(action_audio_making);
	}

	void Anim_Event_Making_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Making_Apply");
		// *** add apply effect here *** //
	}

	void Anim_Event_Picking_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Picking_Aduio");
		
		if (action_audio_picking)
			audioSource.PlayOneShot(action_audio_picking);
	}

	void Anim_Event_Picking_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Picking_Apply");
		// *** add apply effect here *** //
	}

	void Anim_Event_Looking_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Looking_Aduio");
		
		if (action_audio_looking)
			audioSource.PlayOneShot(action_audio_looking);
	}

	void Anim_Event_Looking_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Looking_Apply");
		// *** add apply effect here *** //
	}

	void Anim_Event_Speaking_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Speaking_Audio");
		
		if (action_audio_speaking)
			audioSource.PlayOneShot(action_audio_speaking);
	}

	void Anim_Event_Hurt_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Hurt_Audio");

		if (fight_hurt_audios.Length > 0)
		{
			int index = Random.Range(0,fight_hurt_audios.Length);
			AudioClip clip = fight_hurt_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Death_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Death_Audio");
		
		if (fight_hurt_audios.Length > 0)
		{
			int index = Random.Range(0,fight_death_audios.Length);
			AudioClip clip = fight_death_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Roar_Audio_1()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Roar_Audio_1");
		
		if (fight_roar_audios_1)
			audioSource.PlayOneShot(fight_roar_audios_1);
	}

	void Anim_Event_Roar_Audio_2()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Roar_Audio_2");
		
		if (fight_roar_audios_2)
			audioSource.PlayOneShot(fight_roar_audios_2);
	}

	void Anim_Event_Roar_Audio_3()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Roar_Audio_3");
		
		if (fight_roar_audios_3)
			audioSource.PlayOneShot(fight_roar_audios_3);
	}

	void Anim_Event_Repluse_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Repluse_Apply");

		DP_Do_Repluse_To_Targets_Around();
	}

	void Anim_Event_Attack_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Audio");
		
		if (fight_attack_audios.Length > 0)
		{
			int index = Random.Range(0,fight_attack_audios.Length);
			AudioClip clip = fight_attack_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Parry_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Parry_Audio");
		
		if (fight_attack_audios.Length > 0)
		{
			int index = Random.Range(0,fight_attack_audios.Length);
			AudioClip clip = fight_attack_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Parry_Weapon_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Parry_Audio");

		if (fight_parry_weapon_audio)
			audioSource.PlayOneShot(fight_parry_weapon_audio);
	}

	void Anim_Event_Parry_Weapon_On()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Parry_On");

		is_parry = true;
	}

	void Anim_Event_Parry_Weapon_Off()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Parry_Off");

		is_parry = false;
	}

	void Anim_Event_Dodge_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_Audio");
		
		if (fight_attack_audios.Length > 0)
		{
			int index = Random.Range(0,fight_attack_audios.Length);
			AudioClip clip = fight_attack_audios[index];
			audioSource.PlayOneShot(clip);
		}


	}

	void Anim_Event_Dodge_Off()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_Off");
		
		is_dodge = false;
	}

	void Anim_Event_Dodge_On()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_On");
		
		is_dodge = true;
	}

	void Anim_Event_Dodge_Stop_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_Stop");

		gameObject.rigidbody.angularVelocity = Vector3.zero;
		gameObject.rigidbody.velocity = Vector3.zero;
		// FX
		FX_Creator(dodge_dust_fx_prefab,gameObject.transform);
	}

	void Anim_Event_Dodge_To_Left_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_To_Left");

		//Anim_Event_Dodge_Stop_Apply();
		gameObject.rigidbody.AddForce( Vector3.up * rigidbody.mass, ForceMode.Impulse);
		gameObject.rigidbody.AddForce( -gameObject.transform.right * f_fight_dodge_force, ForceMode.Impulse);
		// FX
		FX_Creator(dodge_dust_fx_prefab,gameObject.transform);
	}

	void Anim_Event_Dodge_To_Right_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_To_Right");

		//Anim_Event_Dodge_Stop_Apply();
		gameObject.rigidbody.AddForce( Vector3.up * rigidbody.mass, ForceMode.Impulse);
		gameObject.rigidbody.AddForce( gameObject.transform.right * f_fight_dodge_force, ForceMode.Impulse);
		// FX
		FX_Creator(dodge_dust_fx_prefab,gameObject.transform);
	}

	void Anim_Event_Dodge_To_Forward_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_To_Forward");

		//Anim_Event_Dodge_Stop_Apply();
		gameObject.rigidbody.AddForce( Vector3.up * rigidbody.mass, ForceMode.Impulse);
		gameObject.rigidbody.AddForce( gameObject.transform.forward * f_fight_dodge_force, ForceMode.Impulse);
		// FX
		FX_Creator(dodge_dust_fx_prefab,gameObject.transform);
	}

	void Anim_Event_Dodge_To_Backward_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Dodge_To_Backward");

		//Anim_Event_Dodge_Stop_Apply();
		gameObject.rigidbody.AddForce( Vector3.up * rigidbody.mass, ForceMode.Impulse);
		gameObject.rigidbody.AddForce( -gameObject.transform.forward * f_fight_dodge_force, ForceMode.Impulse);
		// FX
		FX_Creator(dodge_dust_fx_prefab,gameObject.transform);
	}

	void Anim_Event_Combo_On()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Combo_On");

		is_can_combo = true;
	}

	void Anim_Event_Combo_Off()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Combo_Off");
		
		is_can_combo = false;
	}

	void Anim_Event_Hit_Stop_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Hit_Stop_Apply");

		gameObject.rigidbody.angularVelocity = Vector3.zero;
		gameObject.rigidbody.velocity = Vector3.zero;
	}

	void Anim_Event_Throw_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Throw_Audio");
		
		if (fight_attack_audios.Length > 0)
		{
			int index = Random.Range(0,fight_attack_audios.Length);
			AudioClip clip = fight_attack_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Throw_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Throw_Apply");

		if (throw_object_prefab)
		{
			// setup [Throw_Object_Profile]
			GameObject go = Instantiate(throw_object_prefab,weapon_main_ready_trans.position,transform.rotation) as GameObject;
			Throw_Object_Profile tp = go.GetComponent<Throw_Object_Profile>();
			if (!tp)
			{
				Debug.Log("Throw Object require a [Throw_Object_Profile] + [" + go.name + "]");
			}
			tp.owner = gameObject;

			// add force
			Vector3 dir = transform.forward;
			dir.y = f_throw_direction_up_fix;
			dir.Normalize();
			go.rigidbody.AddForce(dir * f_throw_force, ForceMode.Impulse);
			go.rigidbody.AddTorque(Random.onUnitSphere * f_throw_force,ForceMode.Impulse);
		}
	}

	void Anim_Event_Knock_Backward_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Knock_Backward_Audio");
		
		if (fight_hurt_audios.Length > 0)
		{
			int index = Random.Range(0,fight_hurt_audios.Length);
			AudioClip clip = fight_hurt_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Knock_Forward_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Knock_Forward_Audio");
		
		if (fight_hurt_audios.Length > 0)
		{
			int index = Random.Range(0,fight_hurt_audios.Length);
			AudioClip clip = fight_hurt_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Knock_Down_To_Ground_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Knock_Down_To_Ground_Audio");

		if (fight_knock_down_impact_to_ground_audio)
			audioSource.PlayOneShot(fight_knock_down_impact_to_ground_audio);


	}

	void Anim_Event_Knock_Down_Stop_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Knock_Down_Stop_Apply");
		
		gameObject.rigidbody.angularVelocity = Vector3.zero;
		gameObject.rigidbody.velocity = Vector3.zero;

		
		if (fight_knock_down_fx_prefab)
			FX_Creator(fight_knock_down_fx_prefab,gameObject.transform);
	}

	void Anim_Event_Turn_into_Down_Backward()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Knock_Down_Stop_Apply");

		anim.CrossFade("Down Backward",f_fade_time);
	}

	void Anim_Event_Turn_into_Down_Forward()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Turn_into_Down_Forward");
		
		anim.CrossFade("Down Forward",f_fade_time);
	}

	void Anim_Event_Recover_From_Backward_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Recover_From_Backward_Audio");

		if (fight_attack_audios.Length > 0)
		{
			int index = Random.Range(0,fight_attack_audios.Length);
			AudioClip clip = fight_attack_audios[index];
			audioSource.PlayOneShot(clip);
		}
	}

	void Anim_Event_Recover_From_Backward_FX()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Recover_From_Backward_FX");
		
		//FX
		FX_Creator(dodge_dust_fx_prefab,transform);

		// if trample hit the emeny around him
		if (is_trample)
		{
			f_next_attack_move_force = f_trample_force;
			f_next_attack_damage_radius = f_trample_radius;
			f_next_attack_damage_angle = 180.0f;
			f_next_attack_damage_scale = 0.5f;
			DP_Do_Explosive_To_Targets_Around();
		}

	}

	// ************** Attack ***************** //
	void Anim_Event_Attack_Move_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Move_Apply");

		// add force
		Vector3 dir = transform.forward;
		dir.Normalize();
		rigidbody.AddForce(dir * f_next_attack_move_force, ForceMode.Impulse);
	}

	void Anim_Event_Attack_Move_Stop()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Move_Stop");
		
		gameObject.rigidbody.angularVelocity = Vector3.zero;
		gameObject.rigidbody.velocity = Vector3.zero;
	}

	void Anim_Event_Attack_Move_FX()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Move_FX");
		
		//FX
		FX_Creator(attack_move_dust_fx_prefab,transform);
	}

	void Anim_Event_Attack_Damage_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Damage_Apply");

		DP_Do_Damage_To_Targets();
	}

	void Anim_Event_Attack_Weapon_Audio_1()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_1");
		
		if (fight_attack_weapon_audio_1)
			audioSource.PlayOneShot(fight_attack_weapon_audio_1);
	}
	void Anim_Event_Attack_Weapon_Audio_2()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_2");
		
		if (fight_attack_weapon_audio_2)
			audioSource.PlayOneShot(fight_attack_weapon_audio_2);
	}
	void Anim_Event_Attack_Weapon_Audio_3()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_3");
		
		if (fight_attack_weapon_audio_3)
			audioSource.PlayOneShot(fight_attack_weapon_audio_3);
	}
	void Anim_Event_Attack_Weapon_Audio_4()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_4");
		
		if (fight_attack_weapon_audio_4)
			audioSource.PlayOneShot(fight_attack_weapon_audio_4);
	}
	void Anim_Event_Attack_Weapon_Audio_5()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_5");
		
		if (fight_attack_weapon_audio_5)
			audioSource.PlayOneShot(fight_attack_weapon_audio_5);
	}
	void Anim_Event_Attack_Weapon_Audio_6()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_6");
		
		if (fight_attack_weapon_audio_6)
			audioSource.PlayOneShot(fight_attack_weapon_audio_6);
	}
	void Anim_Event_Attack_Weapon_Audio_7()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_7");
		
		if (fight_attack_weapon_audio_7)
			audioSource.PlayOneShot(fight_attack_weapon_audio_7);
	}
	void Anim_Event_Attack_Weapon_Audio_8()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Attack_Weapon_Audio_8");
		
		if (fight_attack_weapon_audio_8)
			audioSource.PlayOneShot(fight_attack_weapon_audio_8);
	}
	void Anim_Event_Run_Attack_Weapon_Audio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Run_Attack_Weapon_Audio");
		
		if (fight_run_attack_weapon_audio)
			audioSource.PlayOneShot(fight_run_attack_weapon_audio);
	}

	void Anim_Event_Shooting_Aduio()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Shooting_Aduio");
		
		if (fight_attack_arrow_audio)
			audioSource.PlayOneShot(fight_attack_arrow_audio);
	}

	void Anim_Event_Charing_Turn_Into_Shooting()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Charing_Turn_Into_Shooting");

		anim.CrossFade("Shooting",f_fade_time);
	}

	void Anim_Event_Shooting_Apply()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_Charing_Turn_Into_Shooting");

		//
		DP_Shooting();
	}

	// **********************************************************//
	void Anim_Event_End()
	{
		if (Debug_Animation_Event)
			Debug.Log("Anim_Event_End");

		Reset_to_Idle();
	}

	#endregion Animation Event

	// **********************************************************//
	#region Animation Play Method
	public void Play_Weapon_Back()
	{
		// add more weapon mode here
		switch(weapon_class)
		{
		case Weapon_Mode.Dual:
			Play_Dual_Back();
			break;
		case Weapon_Mode.Main:
			Play_Main_Back();
			break;
		case Weapon_Mode.Range:
			Play_Range_Back();
			break;
		case Weapon_Mode.SS:
			Play_SS_Back();
			break;
		}
	}

	public void Play_Weapon_Ready()
	{
		// add more weapon mode here
		switch(weapon_class)
		{
		case Weapon_Mode.Dual:
			Play_Dual_Ready();
			break;
		case Weapon_Mode.Main:
			Play_Main_Ready();
			break;
		case Weapon_Mode.Range:
			Play_Range_Ready();
			break;
		case Weapon_Mode.SS:
			Play_SS_Ready();
			break;
		}
	}


	public void Play_Dual_Back()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.Off)
				anim.CrossFade("Dual Back",f_fade_time);
		}
	}
	public void Play_Main_Back()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.Off)
				anim.CrossFade("Main Back",f_fade_time);
		}
	}
	public void Play_Range_Back()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.Off)
				anim.CrossFade("Range Back",f_fade_time);
		}
	}
	public void Play_SS_Back()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.Off)
				anim.CrossFade("SS Back",f_fade_time);
		}
	}

	public void Play_Dual_Ready()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.Dual)
				anim.CrossFade("Dual Ready",f_fade_time);
		}
	}
	public void Play_Main_Ready()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.Main)
				anim.CrossFade("Main Ready",f_fade_time);
		}
	}

	public void Play_Range_Ready()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.Main)
				anim.CrossFade("Range Ready",f_fade_time);
		}
	}
	public void Play_SS_Ready()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			if (current_weapon_mode != Weapon_Mode.SS)
				anim.CrossFade("SS Ready",f_fade_time);
		}
	}

	public void Play_Walk()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Walk",f_fade_time);
		}
	}

	public void Play_Run()
	{
		if (Is_Ready_To_Play_Next())
		{
			if(!anim.IsPlaying("Run"))
			{
				//audio
				Charing_Audio();
			}

			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Run",f_fade_time);


		}
	}


	public void Play_Idle()
	{
		//is_death = false;
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			Reset_to_Idle();
		}

	}

	public void Play_Drinking()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off)
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Drinking",f_fade_time);
		}
	}

	public void Play_Looking()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off)
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Looking",f_fade_time);
		}
	}

	public void Play_Making()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off)
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Making",f_fade_time);
		}
	}

	public void Play_Picking()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off)
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Picking",f_fade_time);
		}
	}

	public void Play_Pointing()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off)
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Pointing",f_fade_time);
		}
	}

	public void Play_Speaking()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off)
		{
			Reset_All_Combo_Bool();
			anim.CrossFade("Speaking",f_fade_time);
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
		}
	}

	public void Play_Hit_From_Back()
	{
		if (!is_death)
		{
			Reset_All_Combo_Bool();
			anim.Stop();
			anim.CrossFade("Hit From Back",f_fade_time);
		}
	}

	public void Play_Hit_From_Front()
	{
		if (!is_death)
		{
			Reset_All_Combo_Bool();
			anim.Stop();
			anim.CrossFade("Hit From Front",f_fade_time);
		}
	}

	public void Play_Dying_1()
	{
		if (!is_death)
		{
			Reset_All_Combo_Bool();
			is_death = true;
			anim.Stop();
			anim.Play("Dying 1");
		}
	}
	
	public void Play_Dying_2()
	{
		if (!is_death)
		{
			Reset_All_Combo_Bool();
			is_death = true;
			anim.Stop();
			anim.Play("Dying 2");
		}
	}

	public void Play_Parry()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off)
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Parry",f_fade_time);
		}
	}

	public void Play_Dodge_To_Left()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			Anim_Event_Dodge_Stop_Apply();
			//anim.CrossFade("Dodge To Left",f_fade_time);
			if (anim.IsPlaying("Dodge To Forward"))
				anim.Rewind();
			anim.CrossFade("Dodge To Forward",f_fade_time);
			transform.Rotate(0,-90.0f,0);
		}
	}

	public void Play_Dodge_To_Right()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			Anim_Event_Dodge_Stop_Apply();
			//anim.CrossFade("Dodge To Right",f_fade_time);
			if (anim.IsPlaying("Dodge To Forward"))
				anim.Rewind();
			anim.CrossFade("Dodge To Forward",f_fade_time);
			transform.Rotate(0,90.0f,0);
		}
	}

	public void Play_Dodge_To_Backward()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			Anim_Event_Dodge_Stop_Apply();
			//anim.CrossFade("Dodge To Backward",f_fade_time);
			if (anim.IsPlaying("Dodge To Forward"))
				anim.Rewind();
			anim.CrossFade("Dodge To Forward",f_fade_time);
			transform.Rotate(0,180.0f,0);
		}
	}

	public void Play_Dodge_To_Forward()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			Anim_Event_Dodge_Stop_Apply();
			if (anim.IsPlaying("Dodge To Forward"))
				anim.Rewind();
			anim.CrossFade("Dodge To Forward",f_fade_time);
		}
	}

	public void Play_Throw()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Throw",f_fade_time);
		}
	}

	public void Play_Knock_Backward()
	{
		if (!is_death)
		{
			Reset_All_Combo_Bool();
			anim.Stop();
			anim.CrossFade("Knock Backward",f_fade_time);
		}
	}

	public void Play_Knock_Forward()
	{
		if (!is_death)
		{
			Reset_All_Combo_Bool();
			anim.Stop();
			anim.CrossFade("Knock Forward",f_fade_time);

		}
	}

	public void Play_Recover_From_Backward()
	{
		if (anim.IsPlaying("Down Backward"))
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Recover From Backward", f_fade_time);
		}
	}

	public void Play_Recover_From_Forward()
	{
		if (anim.IsPlaying("Down Forward"))
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			anim.CrossFade("Recover From Forward", f_fade_time);
		}
	}

	public void Play_Roar_1()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			anim.CrossFade("Roar 1", f_fade_time);
		}
	}

	public void Play_Roar_2()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			anim.CrossFade("Roar 2", f_fade_time);
		}
	}

	public void Play_Roar_3()
	{
		if (Is_Ready_To_Play_Next())
		{
			Reset_All_Combo_Bool();
			anim.CrossFade("Roar 3", f_fade_time);
		}
	}

	// attack
	public void Play_Attack_1()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_1;
			f_next_attack_damage_radius = f_fight_attack_radius_1;
			f_next_attack_damage_angle = f_fight_attack_angle_1;
			f_next_attack_damage_scale = f_fight_attack_scale_1;
			anim.CrossFade("Attack 1",f_fade_time);
		}
	}

	public void Play_Attack_2()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_2;
			f_next_attack_damage_radius = f_fight_attack_radius_2;
			f_next_attack_damage_angle = f_fight_attack_angle_2;
			f_next_attack_damage_scale = f_fight_attack_scale_2;
			anim.CrossFade("Attack 2",f_fade_time);
		}
	}

	public void Play_Attack_3()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_3;
			f_next_attack_damage_radius = f_fight_attack_radius_3;
			f_next_attack_damage_angle = f_fight_attack_angle_3;
			f_next_attack_damage_scale = f_fight_attack_scale_3;
			anim.CrossFade("Attack 3",f_fade_time);
		}
	}

	public void Play_Attack_4()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_4;
			f_next_attack_damage_radius = f_fight_attack_radius_4;
			f_next_attack_damage_angle = f_fight_attack_angle_4;
			f_next_attack_damage_scale = f_fight_attack_scale_4;
			anim.CrossFade("Attack 4",f_fade_time);
		}
	}

	public void Play_Attack_5()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_5;
			f_next_attack_damage_radius = f_fight_attack_radius_5;
			f_next_attack_damage_angle = f_fight_attack_angle_5;
			f_next_attack_damage_scale = f_fight_attack_scale_5;
			anim.CrossFade("Attack 5",f_fade_time);
		}
	}

	public void Play_Attack_6()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_6;
			f_next_attack_damage_radius = f_fight_attack_radius_6;
			f_next_attack_damage_angle = f_fight_attack_angle_6;
			f_next_attack_damage_scale = f_fight_attack_scale_6;
			anim.CrossFade("Attack 6",f_fade_time);
		}
	}

	public void Play_Attack_7()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_7;
			f_next_attack_damage_radius = f_fight_attack_radius_7;
			f_next_attack_damage_angle = f_fight_attack_angle_7;
			f_next_attack_damage_scale = f_fight_attack_scale_7;
			anim.CrossFade("Attack 7",f_fade_time);
		}
	}

	public void Play_Attack_8()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode != Weapon_Mode.Off )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_attack_move_force_8;
			f_next_attack_damage_radius = f_fight_attack_radius_8;
			f_next_attack_damage_angle = f_fight_attack_angle_8;
			f_next_attack_damage_scale = f_fight_attack_scale_8;
			anim.CrossFade("Attack 8",f_fade_time);
		}
	}

	public void Play_Run_Attack_Dual()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off && anim.IsPlaying("Run") )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_run_attack_move_force;
			f_next_attack_damage_radius = f_fight_run_attack_radius;
			f_next_attack_damage_angle = f_fight_run_attack_angle;
			f_next_attack_damage_scale = f_fight_run_attack_scale;

			// binding weapon
			Weapon_Main_Binding_To_Ready();
			Weapon_Second_Binding_To_Ready();

			current_weapon_mode = Weapon_Mode.Dual;

			anim.CrossFade("Run Attack Dual",f_fade_time);
		}
	}

	public void Play_Run_Attack_Main()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Off && anim.IsPlaying("Run") )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			f_next_attack_move_force = f_fight_run_attack_move_force;
			f_next_attack_damage_radius = f_fight_run_attack_radius;
			f_next_attack_damage_angle = f_fight_run_attack_angle;
			f_next_attack_damage_scale = f_fight_run_attack_scale;
			
			// binding weapon
			Weapon_Main_Binding_To_Ready();
			Weapon_Second_Binding_To_Ready();
			
			current_weapon_mode = Weapon_Mode.Main;
			
			anim.CrossFade("Run Attack Main",f_fade_time);
		}
	}

	public void Play_Shooting()
	{
		if (Is_Ready_To_Play_Next() && current_weapon_mode == Weapon_Mode.Range 
		    && (!anim.IsPlaying("Charing") && !anim.IsPlaying("Shooting")) )
		{
			Reset_All_Combo_Bool();
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			
			anim.CrossFade("Charing",f_fade_time);
		}
	}


	#endregion Animation Play Method

	#region Damage Apply 
	public void DP_Do_Parry_To_Targets_Around()
	{
		// find all the object with Character_Profile
		Character_Profile[] targets_dp = FindObjectsOfType<Character_Profile>() as Character_Profile[];
		bool is_critical_audio = false,is_get_kill_audio = false;
		foreach(Character_Profile target_cp in targets_dp)
		{
			// ref to the gameo bject
			GameObject target = target_cp.gameObject;
			
			//Debug.Log("Anim_Event_Attack_Damage_Apply not done yet");
			
			// determine who will be attacked
			if (target.Equals(gameObject))
				continue;
			
			if (target_cp.Get_Is_Death())
				continue;
			
			if (target.tag.Equals(gameObject.tag))
				continue;
			
			if (!target.rigidbody)
				continue;
			
			if (!target.rigidbody.useGravity)
				continue;
			
			// calculate the distance to me
			float f_distance = (target.transform.position - transform.position).magnitude;
			if (f_distance > (weapon_profile.radius))
				continue;
			
			// calculate the angle to me
			Vector3 dir = target.transform.position - transform.position;
			dir.y=0;
			dir.Normalize();
			
			// clone a Damage_Profile with 1.0f radio
			Damage_Profile dp = damage_profile.Clone(f_fight_parry_attack_scale);
			// add force
			target.rigidbody.AddForce(dir * dp.Get_Attack_force() * 3.0f ,ForceMode.Impulse);
			// FX
			FX_Creator(weapon_attack_fx_prefab, target_cp.blood_fx_trans);
			// apply damage to target
			target_cp.DP_Explosive_Apply(dp);
			// audio
			if (!is_critical_audio)
			{
				is_critical_audio = true;
				Critical_Hit_Audio();
			}
			/*if (!is_get_kill_audio)
			{
				if (target_cp.is_death)
				{
					is_get_kill_audio = true;
					Get_Kill_Audio();
				}
				
			}
			*/
		}
	}

	public void DP_Do_Repluse_To_Targets_Around()
	{
		// find all the object with Character_Profile
		Character_Profile[] targets_dp = FindObjectsOfType<Character_Profile>() as Character_Profile[];
		bool is_critical_audio = false,is_get_kill_audio = false;
		foreach(Character_Profile target_cp in targets_dp)
		{
			// ref to the gameo bject
			GameObject target = target_cp.gameObject;
			
			//Debug.Log("Anim_Event_Attack_Damage_Apply not done yet");
			
			// determine who will be attacked
			if (target.Equals(gameObject))
				continue;
			
			if (target_cp.Get_Is_Death())
				continue;
			
			//if (target.tag.Equals(gameObject.tag))
			//	continue;
			
			if (!target.rigidbody)
				continue;
			
			if (!target.rigidbody.useGravity)
				continue;
			
			// calculate the distance to me
			float f_distance = (target.transform.position - transform.position).magnitude;
			if (f_distance > (f_fight_repluse_radius))
				continue;
			
			// calculate the angle to me
			Vector3 dir = target.transform.position - transform.position;
			dir.y=0;
			dir.Normalize();
			
			// clone a Damage_Profile with 1.0f radio
			Damage_Profile dp = damage_profile.Clone(0.0f);
			// add force
			target.rigidbody.AddForce(dir * f_fight_repluse_force ,ForceMode.Impulse);
			// FX
			FX_Creator(weapon_attack_fx_prefab, target_cp.blood_fx_trans);
			// apply damage to target
			target_cp.DP_Explosive_Apply(dp);
			// audio
			if (!is_critical_audio)
			{
				is_critical_audio = true;
				Critical_Hit_Audio();
			}
			/*if (!is_get_kill_audio)
			{
				if (target_cp.is_death)
				{
					is_get_kill_audio = true;
					Get_Kill_Audio();
				}
				
			}
			*/
		}
	}

	public void DP_Do_Explosive_To_Targets_Around()
	{
		// find all the object with Character_Profile
		Character_Profile[] targets_dp = FindObjectsOfType<Character_Profile>() as Character_Profile[];
		bool is_critical_audio = false,is_get_kill_audio = false;
		foreach(Character_Profile target_cp in targets_dp)
		{
			// ref to the gameo bject
			GameObject target = target_cp.gameObject;
			
			//Debug.Log("Anim_Event_Attack_Damage_Apply not done yet");
			
			// determine who will be attacked
			if (target.Equals(gameObject))
				continue;
			
			if (target_cp.Get_Is_Death())
				continue;
			
			if (target.tag.Equals(gameObject.tag))
				continue;
			
			if (!target.rigidbody)
				continue;
			
			if (!target.rigidbody.useGravity)
				continue;
			
			// calculate the distance to me
			float f_distance = (target.transform.position - transform.position).magnitude;
			if (f_distance > (f_next_attack_damage_radius))
				continue;

			// calculate the angle to me
			Vector3 dir = target.transform.position - transform.position;
			dir.y=0;
			dir.Normalize();
			
			// clone a Damage_Profile with 1.0f radio
			Damage_Profile dp = damage_profile.Clone(f_next_attack_damage_scale);
			// add force
			target.rigidbody.AddForce(dir * dp.Get_Attack_force() ,ForceMode.Impulse);
			// FX
			FX_Creator(weapon_attack_fx_prefab, target_cp.blood_fx_trans);
			// apply damage to target
			target_cp.DP_Explosive_Apply(dp);
			// audio
			if (!is_critical_audio)
			{
				is_critical_audio = true;
				Critical_Hit_Audio();
			}
			if (!is_get_kill_audio)
			{
				if (target_cp.is_death)
				{
					is_get_kill_audio = true;
					Get_Kill_Audio();
				}
				
			}
		}
	}

	public void DP_Do_Damage_To_Targets()
	{

		// find all the object with Character_Profile
		Character_Profile[] targets_dp = FindObjectsOfType<Character_Profile>() as Character_Profile[];
		bool is_critical_audio = false,is_get_kill_audio = false;
		foreach(Character_Profile target_cp in targets_dp)
		{
			// ref to the gameo bject
			GameObject target = target_cp.gameObject;

			//Debug.Log("Anim_Event_Attack_Damage_Apply not done yet");
			
			// determine who will be attacked
			if (target.Equals(gameObject))
				continue;

			if (target_cp.Get_Is_Death())
				continue;

			if (target.tag.Equals(gameObject.tag))
				continue;

			if (!target.rigidbody)
				continue;

			if (!target.rigidbody.useGravity)
				continue;

			// calculate the distance to me
			float f_distance = (target.transform.position - transform.position).magnitude;
			if (f_distance > (f_next_attack_damage_radius + f_weapon_radius))
				continue;

			// calculate the angle to me
			Vector3 dir = target.transform.position - transform.position;
			dir.y=0;
			dir.Normalize();
			// if distance less then a value, then damage target anyway
			if (f_distance > (f_next_attack_damage_radius + f_weapon_radius) * 0.05f)
			{
				float f_angle = Vector3.Angle(transform.forward,dir);
				if (f_angle > f_next_attack_damage_angle)
					continue;
			}


			// clone a Damage_Profile with 1.0f radio
			Damage_Profile dp = damage_profile.Clone(f_next_attack_damage_scale);
			// add force
			if (!target_cp.is_dodge)
				target.rigidbody.AddForce(dir * dp.Get_Attack_force() ,ForceMode.Impulse);
			// FX
			FX_Creator(weapon_attack_fx_prefab, target_cp.blood_fx_trans);
			// apply damage to target
			target_cp.DP_Damage_Apply(dp);
			// audio
			if (!is_critical_audio)
			{
				is_critical_audio = true;
				Critical_Hit_Audio();
			}
			if (!is_get_kill_audio)
			{
				if (target_cp.is_death)
				{
					is_get_kill_audio = true;
					Get_Kill_Audio();
				}

			}
		}

	}

	// get damage
	public void DP_Damage_Apply(Damage_Profile dp)
	{
		// if death
		if (is_death)
			return;

		// if in dodge
		if (is_dodge)
			return;

		// if in parry, 
		if (is_parry)
		{

			GameObject target = dp.owner;
			// no character profile should be arrow
			Character_Profile target_cp = target.GetComponent<Character_Profile>();

			// calculate the angle to me
			Vector3 dirT = target.transform.position - transform.position;
			dirT.y=0;
			dirT.Normalize();
			float f_angle = Vector3.Angle(transform.forward,dirT);
			if (f_angle <= f_parry_angle_limit && target.rigidbody )
			{
				if (target_cp != null)
				{
					/*
					// combat
					// clone a Damage_Profile with 1.0f radio
					Damage_Profile dp_parray = damage_profile.Clone(f_fight_parry_attack_scale);
					// apply damage to target
					target_cp.DP_Explosive_Apply(dp_parray);
					// add force
					target.rigidbody.AddForce(dirT * dp.Get_Attack_force() ,ForceMode.Impulse);
					*/

					// repluse around
					DP_Do_Parry_To_Targets_Around();

					// FX
					FX_Creator(weapon_parry_fx_prefab, weapon_main_ready_trans);
					// audio
					if (weapon_audio_parry_apply)
						audioSource.PlayOneShot(weapon_audio_parry_apply);
					// combo
					is_can_combo = true;
					// face to target
					transform.LookAt(target.transform);

				}

				// end
				return;
			}
		}



		// calculate how much damage apply to me.
		if ( defense_profile.Any_Armor() )
		{
			Defense_Profile.ArmorInfo armor_destory = Calculate_Damage_Apply_To_Me(dp);
			if (armor_destory != null)
			{
				
				// FX
				// FX_Creator_Attach_To_Trans(armor_destory_fx_prefab,blood_fx_trans);
				//if (armor_destory.ap.destory_audio)
				 //	audioSource.PlayOneShot(armor_destory.ap.destory_audio);
			}

			// calculate the damage comes from which direction
			Vector3 dir = dp.source.transform.position - transform.position;
			dir.y = 0;
			dir.Normalize();
			float angle = Vector3.Angle(transform.forward, dir);
			if (angle < 90.0f)
			{
				// look at target
				transform.LookAt(dp.source.transform,Vector3.up);
				if (is_tough)
				{
					if (armor_destory != null)
					{
						Play_Knock_Backward();
						// add force
						rigidbody.AddForce( -transform.forward *
						                   rigidbody.mass *20.0f ,ForceMode.Impulse);
					}
				}
				else
				{
					if (Is_Down()|| armor_destory != null)
					{

						Play_Knock_Backward();
						// add force
						rigidbody.AddForce( -transform.forward *
						                   rigidbody.mass *20.0f ,ForceMode.Impulse);
					}
					else
					{
						Play_Hit_From_Front();
					}
				}
			}
			else
			{
				// back to target
				transform.LookAt(dp.source.transform,Vector3.up);
				transform.Rotate(0,180.0f,0);
				if (is_tough)
				{
					if (armor_destory != null)
					{
						Play_Knock_Forward();
						// add force
						rigidbody.AddForce( transform.forward *
						                   rigidbody.mass *20.0f ,ForceMode.Impulse);
					}
				}
				else
				{
					if (Is_Down() || armor_destory != null)
					{
						Play_Knock_Forward();
						// add force
						rigidbody.AddForce( transform.forward *
						                   rigidbody.mass *20.0f ,ForceMode.Impulse);
					}
					else
					{

						Play_Hit_From_Back();
					}
				}
			}
		}
		else
		{
			// look at target
			transform.LookAt(dp.source.transform,Vector3.up);
			Set_Death();
		}

		// FX
		FX_Creator_Attach_To_Trans(blood_fx_prefab,blood_fx_trans);
		if (blood_fx_on_ground_prefab.Length > 0)
		{
			GameObject go = blood_fx_on_ground_prefab[Random.Range(0,blood_fx_on_ground_prefab.Length)];
			FX_Creator(go,transform);
		}


	}

	// get damage
	public void DP_Explosive_Apply(Damage_Profile dp)
	{
		// if death
		if (is_death)
			return;
		
		// if in dodge
		if (is_dodge)
			return;

		// Reset_All_Combo_Bool();

		
		// calculate how much damage apply to me.

		if ( defense_profile.Any_Armor() )
		{
			Defense_Profile.ArmorInfo armor_destory = Calculate_Damage_Apply_To_Me(dp);
			if (armor_destory != null)
			{
				// FX
				// FX_Creator_Attach_To_Trans(armor_destory_fx_prefab,blood_fx_trans);
				//	if (armor_destory.ap.destory_audio)
					// audioSource.PlayOneShot(armor_destory.ap.destory_audio);
			}


			// calculate the damage comes from which direction
			Vector3 dir = dp.source.transform.position - transform.position;
			dir.y = 0;
			dir.Normalize();
			float angle = Vector3.Angle(transform.forward, dir);
			if (angle < 90.0f)
			{
				// look at target
				//if (!is_tough)
				{
					transform.LookAt(dp.source.transform,Vector3.up);
					Play_Knock_Backward();
					// add force
					rigidbody.AddForce( -transform.forward *
					                   rigidbody.mass *20.0f ,ForceMode.Impulse);
				}
			}
			else
			{
				// back to target
				//if (!is_tough)
				{
					transform.LookAt(dp.source.transform,Vector3.up);
					transform.Rotate(0,180.0f,0);
					Play_Knock_Forward();
					// add force
					rigidbody.AddForce( transform.forward *
					                   rigidbody.mass *20.0f ,ForceMode.Impulse);
				}
			}
		}
		else
		{
			// look at target
			transform.LookAt(dp.source.transform,Vector3.up);
			Set_Death();
		}
		// FX
		FX_Creator_Attach_To_Trans(blood_fx_prefab,blood_fx_trans);
		if (blood_fx_on_ground_prefab.Length > 0)
		{
			GameObject go = blood_fx_on_ground_prefab[Random.Range(0,blood_fx_on_ground_prefab.Length)];
			FX_Creator(go,transform);
		}

	}

	// if destory a armor , return true
	Defense_Profile.ArmorInfo Calculate_Damage_Apply_To_Me(Damage_Profile dp)
	{
		// audio hit body
		if (fight_attack_hit_body_audio)
		{
			audioSource.PlayOneShot(fight_attack_hit_body_audio);
		}

		//Debug.Log("Calculate_Damage_Apply_To_Me not done yet");
		Defense_Profile.ArmorInfo info = defense_profile.Calculate_Damage(dp);

		if (info != null)
		{
			if (info.prefab)
			{
				// drop armor
				GameObject go = Instantiate(info.prefab, info.go.transform.position, info.go.transform.rotation) as GameObject;
				Drop_Equipment(go);
			
			
				// FX
				FX_Creator_Attach_To_Trans(armor_destory_fx_prefab,go.transform);
			}

			if (info.ap.destory_audio)
				audioSource.PlayOneShot(info.ap.destory_audio);

			// disactive
			info.go.SetActive(false);
			return info;
		}

		return null;

	}

	void DP_Shooting()
	{

		//hide the arrow trans
		if (weapon_main_instance)
		{
			// shoot
			if (weapon_profile.arrow_shoot_prefab)
			{
				GameObject go = Instantiate(weapon_profile.arrow_shoot_prefab,
				                            weapon_main_instance.transform.position,
				                            weapon_main_instance.transform.rotation) as GameObject;
				Throw_Object_Profile shoot_top = go.GetComponent<Throw_Object_Profile>();
				shoot_top.owner = gameObject;

				Damage_Profile shoot_dp = go.GetComponent<Damage_Profile>();
				shoot_dp.CopyFrom(damage_profile);
				shoot_dp.owner = gameObject;

				// add force
				go.rigidbody.AddForce( transform.forward * f_fight_shooting_force,ForceMode.Impulse );

				// audio
				Critical_Hit_Audio();

			}
			Destroy(weapon_main_instance);
		}

	}
	
	#endregion Damage Apply

	#region FX Method
	void FX_Creator(GameObject prefab, Transform trans)
	{
		if (prefab && trans)
		{
			GameObject go = Instantiate(prefab,trans.position,trans.rotation) as GameObject;
			go.transform.localScale = transform.localScale;

			// lock rotatation x z
			go.transform.rotation = Quaternion.Euler(0,go.transform.eulerAngles.y,0);
		}
	}
	void FX_Creator_Attach_To_Trans(GameObject prefab, Transform trans)
	{
		if (prefab && trans)
		{
			GameObject go = Instantiate(prefab,trans.position,trans.rotation) as GameObject;
			go.transform.parent = trans;
			go.transform.localScale = transform.localScale;

			// lock rotatation x z
			go.transform.rotation = Quaternion.Euler(0,go.transform.eulerAngles.y,0);

			//go.transform.position = trans.position;
			//go.transform.rotation = trans.rotation;
		}
	}
	#endregion FX Method

}
















