using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Wait_To_Ready : FsmStateAction
{
	Character_Profile cp;
	public FsmEvent done;

	// Code that runs on entering the state.
	public override void Awake()
	{
	}
	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();
		
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		
		if (cp.Is_Ready_To_Play_Next())
		{
			if (done != null)
				Fsm.Event(done);
			else
				Finish();

		}
	}


}
