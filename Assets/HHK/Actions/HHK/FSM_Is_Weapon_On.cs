using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Is_Weapon_On : FsmStateAction
{

	Character_Profile cp;

	public FsmEvent on,off;

	// Code that runs on entering the state.
	public override void Awake()
	{

	}
	public override void OnEnter()
	{
		cp = Owner.GetComponent<Character_Profile>();
		if (cp.Is_Weapon_On())
		{
			Fsm.Event(on);
		}
		else
		{
			Fsm.Event(off);
		}

		Finish();
	}


}
