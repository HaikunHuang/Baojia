using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Searching_Emeny : FsmStateAction
{
	public FsmEvent detected;
	public FsmEvent none;

	AI_Profile ai;

	// Code that runs on entering the state.
	public override void Awake()
	{

	}
	public override void OnEnter()
	{
		ai = Owner.GetComponent<AI_Profile>();
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		
		// reset target
		ai.target_emeny = null;

		// find the closet target
		Collider[] colliders =  Physics.OverlapSphere(Owner.transform.position, ai.serching_radius);
		
		float distance = 1000000.0f;
		List<GameObject> goes= new List<GameObject>();

		foreach(Collider coll in colliders)
		{
			Character_Profile cp = coll.gameObject.GetComponent<Character_Profile>();
			if (cp == null)
				continue;
			
			GameObject go = cp.gameObject;
			
			if (go.Equals(Owner))
				continue;
			
			if (cp.Get_Is_Death())
				continue;

			if (!cp.enabled)
				continue;

			if (go.tag.Equals(Owner.tag))
				continue;
			/*
			float dis = (go.transform.position - Owner.transform.position).magnitude;
			if (dis < distance)
			{
				distance = dis;
				ai.target_emeny = go;
			}
			*/
			goes.Add(go);
		}

		GameObject[] targets = goes.ToArray();
		if (targets.Length>0)
		{
			ai.target_emeny = targets[Random.Range(0,targets.Length)];
		}


		if (ai.target_emeny)
		{
			// target insight
			Vector3 myPos = Owner.transform.position;
			myPos.y += 1.5f;
			Vector3 dir = ai.target_emeny.transform.position -  Owner.transform.position;
			dir.Normalize();

			Ray ray = new Ray(myPos, dir);
			RaycastHit info;

			//int layer = ~ (LayerMask.NameToLayer("Spwan Point"));

			if (Physics.Raycast(ray,out info))
			{
				if (info.collider.gameObject.Equals(ai.target_emeny))
				{
					Fsm.Event(detected);
				}
				else
				{
					Fsm.Event(none);
				}
			}
			else
			{
				Fsm.Event(none);
			}



		}
		else
		{
			Fsm.Event(none);
		}

	}

	// Code that runs when exiting the state.
	public override void OnExit()
	{
		
	}


}
