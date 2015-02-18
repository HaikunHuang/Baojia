using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Losing_Emeny : FsmStateAction
{
	public FsmEvent losing;
	
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
		if (ai.target_emeny)
		{
			cp = ai.target_emeny.GetComponent<Character_Profile>();
			if (cp.Get_Is_Death() || !cp.enabled
			    || (Owner.transform.position - ai.target_emeny.transform.position).magnitude > ai.serching_radius )
			{
				ai.target_emeny = null;
				Fsm.Event(losing);
			}
		}
		else
		{
			Fsm.Event(losing);
		}
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (ai.target_emeny)
		{
			if (cp.Get_Is_Death() || !cp.enabled
			    || (Owner.transform.position - ai.target_emeny.transform.position).magnitude > ai.serching_radius )
			{
				ai.target_emeny = null;
				Fsm.Event(losing);
			}
		}
		else
		{
			Fsm.Event(losing);
		}
	}

	// Code that runs when exiting the state.
	public override void OnExit()
	{
		ai.target_emeny = null;
	}


}
