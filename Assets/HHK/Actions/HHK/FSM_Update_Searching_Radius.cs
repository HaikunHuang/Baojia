using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Update_Searching_Radius : FsmStateAction
{
	AI_Profile ai;

	public bool reset;

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		ai = Owner.GetComponent<AI_Profile>();

		if (reset)
		{
			ai.Reset_Search_Radius();
		}
		else
		{
			ai.Double_Search_Radius();
		}

		Finish();
	}


}
