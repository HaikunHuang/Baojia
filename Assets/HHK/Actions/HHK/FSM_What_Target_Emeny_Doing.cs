using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_What_Target_Emeny_Doing : FsmStateAction
{
	Character_Profile cp,target_cp;

	AI_Profile ai;

	public FsmEvent is_attack, is_down, is_hurt, is_dodge, is_parry, is_throw, otherwise;

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();
		ai = Owner.GetComponent<AI_Profile>();
		target_cp = ai.target_emeny.GetComponent<Character_Profile>();

		
		if (target_cp.Is_Attack())
		{
			Vector3 dir = Owner.transform.position - target_cp.gameObject.transform.position;
			if (Vector3.Angle(target_cp.gameObject.transform.forward, dir) <= target_cp.Get_Next_Attack_Angle())
			{
				Fsm.Event(is_attack);
			}
		}
		
		if (target_cp.Is_Down())
		{
			Fsm.Event(is_down);
		}
		
		if (target_cp.Is_Hurt())
		{
			Fsm.Event(is_hurt);
		}
		
		if (target_cp.Is_Dodge())
		{
			Fsm.Event(is_dodge);
		}
		
		if (target_cp.Is_Parry())
		{
			Vector3 dir = Owner.transform.position - target_cp.gameObject.transform.position;
			if (Vector3.Angle(target_cp.gameObject.transform.forward, dir) <= target_cp.f_parry_angle_limit)
			{
				Fsm.Event(is_parry);
			}
		}

		if (target_cp.Is_Throw())
		{
			Fsm.Event(is_throw);
		}

		Fsm.Event(otherwise);
		Finish();

	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
	}


}
