using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Is_Emeny_In_Attack_Rang : FsmStateAction
{
	public FsmEvent no_target, yes, no;
	
	AI_Profile ai;
	
	Character_Profile cp;
	
	// Code that runs on entering the state.
	public override void Awake()
	{


	}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		ai = Owner.GetComponent<AI_Profile>();
		cp = Owner.GetComponent<Character_Profile>();

		if (!ai.target_emeny)
		{
			Fsm.Event(no_target);
		}
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (!ai.target_emeny)
		{
			Fsm.Event(no_target);
		}
		else
		{
			Character_Profile target_cp = ai.target_emeny.GetComponent<Character_Profile>();
			if (target_cp.Get_Is_Death())
			{
				Fsm.Event(no);
			}
			float radius = cp.Get_Weapon_Radius();

			if ((Owner.transform.position - ai.target_emeny.transform.position).magnitude <= radius)
			{
				Fsm.Event(yes);
			}
			else
			{
				Fsm.Event(no);
			}
		}

	}


}
