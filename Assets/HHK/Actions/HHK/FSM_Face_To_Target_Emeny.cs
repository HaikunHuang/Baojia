using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Face_To_Target_Emeny : FsmStateAction
{
	AI_Profile ai;

	public bool every_frames;
	
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
			Owner.transform.LookAt(ai.target_emeny.transform);
		}

		if (!every_frames)
			Finish();
	}
	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (ai.target_emeny)
		{
			Owner.transform.LookAt(ai.target_emeny.transform);
		}
	}


}
