using UnityEngine;
using System.Collections;

// this profile attach to the throw object that represent the throw object's property

[RequireComponent(typeof(Damage_Profile))]
public class Throw_Object_Profile : MonoBehaviour {

	public bool b_ignore_same_tag_with_owner = false;
	public bool b_ignore_different_tag_with_owner = false;
	public bool b_stop_movement = true;
	public bool b_shooting = false;
	public bool b_explosive_damage_instant = false;

	public float f_radius_explosive = 10.0f;
	//public float f_force = 80f;
	public float f_force_up_fix_percent = 0.0f;

	public GameObject smoke_prefab,arrow_smoke_prefab, arrow_attach_prefab; 
	GameObject arrow_smoke_instance;

	Damage_Profile damage;
	
	public GameObject owner;

	public AudioClip audio;

	bool b_attach;

	public bool debug_Event;

	#region System Method
	// Use this for initialization
	void Start () 
	{
		damage = GetComponent<Damage_Profile>();
		damage.source = gameObject;
		damage.owner = owner;

		collider.isTrigger = true;

		// FX arrow smoke
		if (arrow_smoke_prefab)
		{
			arrow_smoke_instance = Instantiate(arrow_smoke_prefab, transform.position, transform.rotation) as GameObject;
			arrow_smoke_instance.transform.parent = gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (b_attach)
		{
			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if (debug_Event)
			Debug.Log("OnCollisionEnter");

		if (collision.collider.gameObject.Equals(owner))
			return;

		if (b_ignore_same_tag_with_owner && collision.collider.gameObject.tag.Equals(owner.tag))
			return;

		if (b_ignore_different_tag_with_owner && !collision.collider.gameObject.tag.Equals(owner.tag))
			return;


		// stop movement
		if (b_stop_movement)
		{
			if (debug_Event)
				Debug.Log("b_stop_movement");

			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;

			gameObject.rigidbody.useGravity = false;
			gameObject.collider.enabled = false;
		}

		// shooting
		if (b_shooting)
		{
			if (debug_Event)
				Debug.Log("b_shooting");

			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			
			gameObject.rigidbody.useGravity = false;
			gameObject.collider.enabled = false;

			gameObject.transform.parent = collision.collider.transform;

			if (audio)
			{
				gameObject.AddMissingComponent<AudioSource>();
				AudioSource source = GetComponent<AudioSource>();
				source.PlayOneShot(audio);
			}

			Character_Profile cp = collision.collider.gameObject.GetComponent<Character_Profile>();
			
			// character apply
			if (cp && !cp.Get_Is_Death())
			{	
				// attach to the target
				if (arrow_attach_prefab)
				{
					GameObject go = Instantiate(arrow_attach_prefab) as GameObject;
					go.transform.parent = cp.blood_fx_trans;
					go.transform.rotation = gameObject.transform.rotation;
					go.transform.position = cp.blood_fx_trans.position;
					go.transform.position -= go.transform.up * Random.Range(0.3f,0.6f);


					b_attach = true;
				}
				Damage_Profile dp = damage.Clone(1.0f);
			
				Vector3 dir = (cp.gameObject.transform.position - gameObject.transform.position).normalized;
				// add force
				cp.gameObject.rigidbody.AddForce(  dir * dp.Get_Attack_force()
				                                 ,ForceMode.Impulse);

				cp.DP_Damage_Apply(dp);

				// delete it
				Destroy(gameObject);

			}

			// deattach smoke
			Destory_Arrow_Smoke();

			this.enabled = false;


		}

		// explosive damage
		if (b_explosive_damage_instant)
		{
			if (debug_Event)
				Debug.Log("b_explosive_instant");




			// smoke
			if (smoke_prefab)
			{
				GameObject smoke = Instantiate(smoke_prefab, transform.position,transform.rotation) as GameObject;
			}

			Do_Explosive_damage(collision);

			// destory
			Destroy(gameObject);
		}
	}

	void Do_Explosive_damage(Collider target_collision)
	{

		// find all collider
		Vector3 explosionPos = transform.position;
		//explosionPos.y -= (target_collision.collider.gameObject.transform.position.y - 0.3f);
		Collider[] colliders = Physics.OverlapSphere(explosionPos, f_radius_explosive);
		foreach (Collider hit in colliders) {

			if (b_ignore_same_tag_with_owner && hit.gameObject.tag.Equals(owner.tag))
				continue;
			if (b_ignore_different_tag_with_owner && !hit.gameObject.tag.Equals(owner.tag))
				continue;


			Character_Profile cp = hit.gameObject.GetComponent<Character_Profile>();

			// character apply
			if (cp && !cp.Get_Is_Death())
			{
				// recalculate the damage base on distance from explosionPos
				Vector3 dir = hit.gameObject.transform.position - transform.position;
				float distance = dir.magnitude;
				float ratio_base_on_distance = distance / f_radius_explosive;

				Damage_Profile dp = damage.Clone(ratio_base_on_distance);

				// apply force
				hit.rigidbody.AddExplosionForce(damage.Get_Attack_force(), explosionPos, f_radius_explosive * 1.5f, 
				                                f_force_up_fix_percent * damage.Get_Attack_force(),
				                                ForceMode.Impulse);

				
				cp.DP_Explosive_Apply(dp);
			}
			
		}

	}

	void OnTriggerStay(Collider collision)
	{
		/*
		if (collision.collider.gameObject.Equals(owner))
			return;

		if (b_ignore_same_tag_with_owner && collision.collider.gameObject.tag.Equals(owner.tag))
			return;

		if (b_ignore_different_tag_with_owner && !collision.collider.gameObject.tag.Equals(owner.tag))
			return;


		// stop movement
		if (b_stop_movement)
		{
			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;
		}
		*/
	}

	void Destory_Arrow_Smoke()
	{
		// deattach smoke
		if (arrow_smoke_instance)
		{
			arrow_smoke_instance.transform.parent = null;
			ParticleSystem ps = arrow_smoke_instance.GetComponentInChildren<ParticleSystem>();
			ps.Stop();
			
			Destroy(arrow_smoke_instance, 2.0f);
		}
	}



	#endregion System Method
}
