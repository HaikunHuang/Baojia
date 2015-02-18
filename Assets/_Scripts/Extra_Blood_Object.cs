using UnityEngine;
using System.Collections;

public class Extra_Blood_Object : MonoBehaviour {

	public GameObject blood_prefab;
	public int mount_pre_frame = 16;
	public float time = 0.5f;
	public float force = 5.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (time <= 0.0f)
			return;
		time -= Time.deltaTime;

		if (blood_prefab)
		{
			for (int i=0; i < mount_pre_frame; i++)
			{
				GameObject go = Instantiate(blood_prefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
				go.transform.rotation = Random.rotation;
				go.AddMissingComponent<Rigidbody>();
				go.rigidbody.useGravity = true;
				go.AddMissingComponent<Item_Drop_To_Static>();

				// add force
				go.rigidbody.AddForce(
					Random.Range(-force,force),
					Random.Range(force * 1.0f,force * 2.0f),
					Random.Range(-force,force),
					ForceMode.Impulse
					);
				go.rigidbody.AddRelativeTorque(
					Random.Range(-force,force),
					Random.Range(0-force,force),
					Random.Range(-force,force),
					ForceMode.Impulse
					);

				// destroy
				Destory_By_Time dbt;
				go.AddMissingComponent<Destory_By_Time>();
				dbt = go.GetComponent<Destory_By_Time>();
				dbt.time = 5.0f;
			}
		}
	}
}
