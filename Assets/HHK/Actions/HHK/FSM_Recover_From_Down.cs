using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
// player use
public class FSM_Recover_From_Down : FsmStateAction
{
	Character_Profile cp;
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
		if (cp.Is_Ready_For_Recover())
		{
			if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
			{
				cp.Play_Recover_From_Backward();
				cp.Play_Recover_From_Forward();
			}
		}
	}


}
