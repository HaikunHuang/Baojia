using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Is_Run : FsmStateAction
{

	Character_Profile cp;

	public FsmEvent yes, no;

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();

		if (cp.Is_Run())
		{
			Fsm.Event(yes);
		}
		else
		{
			Fsm.Event(no);
		}
		Finish();
	}


}
