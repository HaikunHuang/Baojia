using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Player_Move_Controll : FsmStateAction
{
	Character_Profile cp;
	bool is_move;
	NavMeshAgent agent;

	// Code that runs on entering the state.
	public override void Awake()
	{

	}

	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();
		agent = Owner.GetComponent<NavMeshAgent>();
		agent.acceleration = 999999.0f;
		agent.angularSpeed = 999999.0f;
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		Move_Update();
	}
	
	// Code that runs when exiting the state.
	public override void OnExit()
	{
		if (is_move)
		{
			is_move = false;

			Vector3 newPos = Owner.transform.position;
			newPos.y += 1.0f;
			agent.SetDestination(newPos);
			
			agent.speed = 0.0f;
			agent.Stop();

			agent.speed = 0.0f;
			agent.Stop();
			cp.Play_Idle();
		}

	}

	void Move_Update()
	{
		
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
			is_move = true;
			// this is controll rotate
			if (cp.Is_Ready_To_Play_Next())
			{
				Owner.transform.LookAt(newDir);
			}
			
			// this is controll move
			if (cp.Is_Idle())
			{
				Vector3 newPos = Owner.transform.position;


				if (Input.GetButton("Run Toggle"))
				{
					//Vector3 newPos = Owner.transform.position + Owner.transform.forward * cp.walk_speed * Time.deltaTime;
					//Owner.transform.position = newPos;
					agent.speed = cp.walk_speed;
					newPos += Owner.transform.forward * cp.walk_speed;
					cp.Play_Walk();
				}
				else
				{
					//Vector3 newPos = Owner.transform.position + Owner.transform.forward * cp.run_speed * Time.deltaTime;
					//Owner.transform.position = newPos;
					agent.speed = cp.run_speed;
					newPos += Owner.transform.forward * cp.run_speed;
					cp.Play_Run();
				}

				newPos.y += 1.0f;
				agent.SetDestination(newPos);
			}
			else
			{

				if (is_move)
				{
					is_move = false;

					Vector3 newPos = Owner.transform.position;
					newPos.y += 1.0f;
					agent.SetDestination(newPos);
					
					agent.speed = 0.0f;
					agent.Stop();
				}
			}
			
		}
		else
		{
			if (is_move)
			{
				is_move = false;
				cp.Play_Idle();

				
				Vector3 newPos = Owner.transform.position;
				newPos.y += 1.0f;
				agent.SetDestination(newPos);
				
				agent.speed = 0.0f;
				agent.Stop();
			}


		}
	}

}
