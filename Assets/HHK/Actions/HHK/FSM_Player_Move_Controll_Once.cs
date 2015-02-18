using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Player_Move_Controll_Once : FsmStateAction
{
	Character_Profile cp;

	// Code that runs on entering the state.
	public override void Awake()
	{

	}

	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		
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
		
		if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
			// this is controll rotate
			if (cp.Is_Ready_To_Play_Next())
			{
				Owner.transform.LookAt(newDir);
			}
		}

		Finish();
	}


}
