using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Is_Ready_For_Recover : FsmStateAction
{
	Character_Profile cp;

	public FsmEvent ready;

	// Code that runs on entering the state.
	public override void Awake()
	{

	}

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (cp.Is_Ready_For_Recover())
		{
			Fsm.Event(ready);
			Finish();
		}
	}


}
