using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_NPC_Will : FsmStateAction
{
	public FsmEvent throw_will, attack_will, combo_will, violent_will, parry_will, dodge_will, chase_will,boss_move_to_way_point_will;

	AI_Profile ai;

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		ai = Owner.GetComponent<AI_Profile>();
		
		if (Random.Range(0,100.0f) < ai.throw_will )
		{
			Fsm.Event(throw_will);
		}

		if (Random.Range(0,100.0f) < ai.parry_will )
		{
			
			Fsm.Event(parry_will);
		}

		if (Random.Range(0,100.0f) < ai.combo_will )
		{	
			Fsm.Event(combo_will);
		}
		if (Random.Range(0,100.0f) < ai.violent_will )
		{
			
			Fsm.Event(violent_will);
		}

		if (Random.Range(0,100.0f) < ai.dodge_will )
		{
			
			Fsm.Event(dodge_will);
		}


		if (Random.Range(0,100.0f) < ai.attack_will )
		{
			Fsm.Event(attack_will);
		}



		if (Random.Range(0,100.0f) < ai.chase_will )
		{
			
			Fsm.Event(chase_will);
		}

		if (Random.Range(0,100.0f) < ai.boss_move_to_way_point_will )
		{
			
			Fsm.Event(boss_move_to_way_point_will);
		}

		Finish();
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{

	}
}
