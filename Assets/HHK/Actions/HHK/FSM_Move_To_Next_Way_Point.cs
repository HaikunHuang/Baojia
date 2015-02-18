using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
[RequireComponent(typeof(NavMeshAgent))]
public class FSM_Move_To_Next_Way_Point : FsmStateAction
{
	AI_Profile ai;
	Character_Profile cp;
	NavMeshAgent agent;
	
	public FsmEvent done,no_target;

	Vector3 prev_wapPoint;

	public bool boss_move_wap_point;
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		ai = Owner.GetComponent<AI_Profile>();
		cp = Owner.GetComponent<Character_Profile>();
		agent = Owner.GetComponent<NavMeshAgent>();
		agent.acceleration = 999999.0f;
		agent.angularSpeed = 999999.0f;
		
		float stop_Distance = 0.1f;
		agent.stoppingDistance = stop_Distance;

		
		// get next wp
		if (boss_move_wap_point)
		{
			ai.Next_Boss_Way_Point();
			cp.is_boss_move_to_way_point = true;

		}
		else
		{
			ai.Next_Way_Point();
		}

		if (ai.current_way_point)
		{
			agent.SetDestination(ai.current_way_point.transform.position);
			prev_wapPoint = ai.current_way_point.transform.position;
		}
	}
	
	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (ai.current_way_point == null)
		{
			Fsm.Event(no_target);
			Finish();
		}
		else
		{

			if (Vector3.Distance(ai.current_way_point.transform.position,prev_wapPoint) > 0.5f)
			{
				prev_wapPoint = ai.current_way_point.transform.position;
				agent.SetDestination(ai.current_way_point.transform.position);
				agent.speed = cp.run_speed;
			}

			// reach
			if ((agent.destination - Owner.transform.position).magnitude <= agent.stoppingDistance)
			{
				agent.speed = 0.0f;
				
				agent.Stop();
				cp.Play_Idle();
				// align the rotation
				agent.gameObject.transform.rotation = ai.current_way_point.transform.rotation;
				Fsm.Event(done);

				// continue to move
				agent.SetDestination(ai.current_way_point.transform.position);
			}
			else
			{
				//agent.SetDestination(ai.current_way_point.transform.position);
				agent.speed = cp.run_speed;
				// action
				cp.Play_Run();
			}
		}

		// prev_pos = Owner.transform.position;
	}
	
	// Code that runs when exiting the state.
	public override void OnExit()
	{
		cp.is_boss_move_to_way_point = false;
		agent.speed = 0.0f;
		agent.Stop();
		cp.Play_Idle();
	}
}
