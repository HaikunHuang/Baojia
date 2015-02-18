using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Is_Real_Idle : FsmStateAction
{

	Character_Profile cp;

	public FsmEvent done;
	
	// Code that runs on entering the state.
	public override void Awake()
	{
		cp = Owner.GetComponent<Character_Profile>();
	}

	public override void OnEnter()
	{
	}
	
	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (cp.Is_Real_Idle())
		{
			Fsm.Event(done);
		}
	}



}
