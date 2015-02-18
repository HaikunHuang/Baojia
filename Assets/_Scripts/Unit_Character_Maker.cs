using UnityEngine;
using System.Collections;

public class Unit_Character_Maker : MonoBehaviour {

	//*******************************************************//
	// target
	public Character_Profile cp;
	public GameObject throw_ogject;
	//public float f_throw_direction_up_fix = 0.3f;
	//public float f_throw_force = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetMouseButton(1))
		{
			float x = Input.GetAxis("Mouse X");
			cp.gameObject.transform.Rotate(0,x * -360.0f * Time.deltaTime, 0);

		}
*/
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Play_Attack_1();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Play_Attack_2();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Play_Attack_3();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Play_Attack_4();
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Play_Attack_5();
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			Play_Attack_6();
		}
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			Play_Attack_7();
		}
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			Play_Attack_8();
		}
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			Play_Run_Attack_Dual();
		}
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			Play_Shooting();
		}
	}

	//*******************************************************//
	#region Play Method
	public void Reset_Position_To_Zero()
	{
		cp.gameObject.transform.position = Vector3.zero;
	}
	public void Play_Dual_Back()
	{
		cp.Play_Dual_Back();
	}
	public void Play_Dual_Ready()
	{
		cp.Play_Dual_Ready();
	}
	public void Play_Walk()
	{
		cp.Play_Walk();
	}
	public void Play_Run()
	{
		cp.Play_Run();
	}
	public void Play_Idle()
	{
		cp.Play_Idle();
	}
	public void Play_Drinking()
	{
		cp.Play_Drinking();
	}
	public void Play_Looking()
	{
		cp.Play_Looking();
	}
	public void Play_Making()
	{
		cp.Play_Making();
	}
	public void Play_Picking()
	{
		cp.Play_Picking();
	}
	public void Play_Pointing()
	{
		cp.Play_Pointing();
	}
	public void Play_Speaking()
	{
		cp.Play_Speaking();
	}
	public void Play_Hit_From_Back()
	{
		cp.Play_Hit_From_Back();
		cp.gameObject.rigidbody.AddForce(cp.gameObject.transform.forward * 120, ForceMode.Impulse);
	}
	public void Play_Hit_From_Front()
	{
		cp.Play_Hit_From_Front();
		cp.gameObject.rigidbody.AddForce(-cp.gameObject.transform.forward * 80, ForceMode.Impulse);
	}
	public void Play_Dying_1()
	{
		cp.Play_Dying_1();
	}
	public void Play_Dying_2()
	{
		cp.Play_Dying_2();
	}
	public void Play_Parry()
	{
		cp.Play_Parry();
	}
	public void Play_Dodge_Left()
	{
		cp.Play_Dodge_To_Left();
	}
	public void Play_Dodge_Right()
	{
		cp.Play_Dodge_To_Right();
	}
	public void Play_Dodge_Forward()
	{
		cp.Play_Dodge_To_Forward();
	}
	public void Play_Dodge_Backward()
	{
		cp.Play_Dodge_To_Backward();
	}
	public void Play_Throw()
	{
		cp.throw_object_prefab = throw_ogject;
		//cp.f_throw_direction_up_fix = f_throw_direction_up_fix;
		//cp.f_throw_force = f_throw_force;
		cp.Play_Throw();
	}
	public void Play_Knock_Backward()
	{
		// add force here
		cp.Play_Knock_Backward();
		Vector3 dir = -cp.gameObject.transform.forward;
		dir.y += 0.3f;
		dir.Normalize();
		cp.gameObject.rigidbody.AddForce(dir * 180, ForceMode.Impulse);
	}
	public void Play_Knock_Forward()
	{
		// add force here
		cp.Play_Knock_Forward();
		Vector3 dir = cp.gameObject.transform.forward;
		dir.y += 0.3f;
		dir.Normalize();
		cp.gameObject.rigidbody.AddForce(dir * 130, ForceMode.Impulse);
	}
	public void Play_Recover_From_Backward()
	{
		cp.Play_Recover_From_Backward();
	}
	public void Play_Recover_From_Forward()
	{
		cp.Play_Recover_From_Forward();
	}

	// attack
	public void Play_Attack_1()
	{
		cp.Play_Attack_1();
	}
	public void Play_Attack_2()
	{
		cp.Play_Attack_2();
	}
	public void Play_Attack_3()
	{
		cp.Play_Attack_3();
	}
	public void Play_Attack_4()
	{
		cp.Play_Attack_4();
	}
	public void Play_Attack_5()
	{
		cp.Play_Attack_5();
	}
	public void Play_Attack_6()
	{
		cp.Play_Attack_6();
	}
	public void Play_Attack_7()
	{
		cp.Play_Attack_7();
	}
	public void Play_Attack_8()
	{
		cp.Play_Attack_8();
	}
	public void Play_Run_Attack_Dual()
	{
		cp.Play_Run_Attack_Dual();
	}
	public void Play_Shooting()
	{
		cp.Play_Shooting();
	}
	#endregion Play Method
}














