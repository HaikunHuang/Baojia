using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Is_Death : FsmStateAction
{
	Character_Profile cp;
	public FsmEvent death;

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
		if (cp.Get_Is_Death())
		{
			Fsm.Event(death);
		}
	}


}
