using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Is_Hurt : FsmStateAction
{
	Character_Profile cp;
	public FsmEvent hurt,down;
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
		if (cp.Is_Hurt())
		{
			Fsm.Event(hurt);
		}
		if (cp.Is_Down())
		{
			Fsm.Event(down);
		}
	}


}
