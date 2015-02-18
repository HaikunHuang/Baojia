using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
[RequireComponent(typeof(NavMeshAgent))]
public class FSM_Move_To_Target_Emeny : FsmStateAction
{
	AI_Profile ai;
	Character_Profile cp, target_cp;
	NavMeshAgent agent;

	public FsmEvent done,no_target;


	Vector3 prev_target_pos;

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		ai = Owner.GetComponent<AI_Profile>();
		cp = Owner.GetComponent<Character_Profile>();
		agent = Owner.GetComponent<NavMeshAgent>();
		agent.acceleration = 999999.0f;
		agent.angularSpeed = 999999.0f;


		if (ai.target_emeny)
		{
			target_cp = ai.target_emeny.GetComponent<Character_Profile>();
			prev_target_pos = ai.target_emeny.transform.position;

			float dis = Vector3.Distance(ai.target_emeny.transform.position, Owner.transform.position) * 0.5f;

			float stop_Distance = (cp.Get_Weapon_Radius() > dis ? cp.Get_Weapon_Radius() : dis);
			agent.stoppingDistance = stop_Distance;
			
			agent.SetDestination(ai.target_emeny.transform.position);
		}
		else
		{
			Fsm.Event(no_target);
			Finish();
		}
	}


	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (ai.target_emeny == null || !target_cp || target_cp.Get_Is_Death())
		{
			Fsm.Event(no_target);
			Finish();
		}
		else
		{

			if (Vector3.Distance(ai.target_emeny.transform.position,prev_target_pos) > 0.5f)
			{
				prev_target_pos = ai.target_emeny.transform.position;
				agent.SetDestination(ai.target_emeny.transform.position);
				agent.speed = cp.run_speed;
			}



			// reach
			if ((agent.destination - Owner.transform.position).magnitude < agent.stoppingDistance)
			{
				agent.speed = 0.0f;
				agent.Stop();
				cp.Play_Idle();
				Fsm.Event(done);

				// continue to move
				agent.SetDestination(ai.target_emeny.transform.position);
			}
			else
			{
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
		agent.speed = 0.0f;
		agent.Stop();
		cp.Play_Idle();
	}


}
