using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Button_Input : FsmStateAction
{
	public FsmEvent fire1,fire2,fire3,fire4,r1,r2;

	Character_Profile cp;

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			//Face_To_Forward();
			Fsm.Event(fire1);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			//Face_To_Forward();
			Fsm.Event(fire2);
		}
		if (Input.GetButtonDown("Fire3"))
		{
			Fsm.Event(fire3);
		}
		if (Input.GetButtonDown("Fire4"))
		{
			Fsm.Event(fire4);
		}
		if (Input.GetButtonDown("R1"))
		{
			Fsm.Event(r1);
		}
		if (Input.GetButtonDown("R2"))
		{
			Fsm.Event(r2);
		}

	}

	// face to forward
	public void Face_To_Forward()
	{
		float x = 0;
		float z = 1.0f;
		
		// angle
		Vector3 cameraDir = Camera.main.transform.forward;
		cameraDir.y=0.0f;
		cameraDir.Normalize();
		Vector3 inputDir = new Vector3(x,
		                               0,
		                               z).normalized;
		
		
		float angle = Vector3.Angle(cameraDir,inputDir);
		Vector3 cross = Vector3.Cross(cameraDir, inputDir);
		if (cross.y < 0) angle = - angle;
		Vector3 cameraV3 = Camera.main.transform.rotation.eulerAngles;
		Vector3 newDir = Quaternion.Euler(0,cameraV3.y,0) * inputDir;
		newDir += Owner.transform.position;
		
		// this is controll rotate
		if (cp.Is_Ready_To_Play_Next())
		{
			Owner.transform.LookAt(newDir);
		}


	}

	// Code that runs when exiting the state.
	public override void OnExit()
	{
		
	}


}
