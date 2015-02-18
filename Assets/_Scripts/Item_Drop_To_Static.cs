using UnityEngine;
using System.Collections;

public class Item_Drop_To_Static : MonoBehaviour {

	//Destory_By_Time dbt;
	// Use this for initialization

	float f_delay_to_destory = 300.0f;

	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Equals("Ground"))
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			gameObject.collider.enabled = false;
			gameObject.rigidbody.useGravity = false;
			gameObject.AddComponent<Destory_By_Time>();
			Destory_By_Time dbt = gameObject.GetComponent<Destory_By_Time>();
			dbt.time = f_delay_to_destory;
			//dbt.enabled = false;
			this.enabled = false;
		}
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag.Equals("Ground"))
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			gameObject.collider.enabled = false;
			gameObject.rigidbody.useGravity = false;
			gameObject.AddComponent<Destory_By_Time>();
			Destory_By_Time dbt = gameObject.GetComponent<Destory_By_Time>();
			dbt.time = f_delay_to_destory;
			//dbt.enabled = false;
			this.enabled = false;
		}
	}

}
