using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("HHK")]
public class FSM_Play_Action : FsmStateAction
{
	public enum PlayAction
	{
		None,
		throw_action,
		weapon_ready,
		weapon_back,
		attack_1,
		attack_2,
		attack_3,
		attack_4,
		attack_5,
		attack_6,
		attack_7,
		attack_8,
		run_attack_dual,
		recover,
		parry,
		dodge_left,
		dodge_right,
		dodge_backward,
		dodge_forward,
		looking,
		shooting,
		roar_1,
		roar_2,
		roar_3
	};
	public PlayAction action;

	Character_Profile cp;

	// Code that runs on entering the state.
	public override void Awake()
	{
	}
	public override void OnEnter()
	{
		
		cp = Owner.GetComponent<Character_Profile>();
		switch (action)
		{
		case PlayAction.throw_action:
			cp.Play_Throw();
			break;
		case PlayAction.weapon_ready:
			cp.Play_Weapon_Ready();
			break;
		case PlayAction.weapon_back:
			cp.Play_Weapon_Back();
			break;
		case PlayAction.attack_1:
			cp.Play_Attack_1();
			break;
		case PlayAction.attack_2:
			cp.Play_Attack_2();
			break;
		case PlayAction.attack_3:
			cp.Play_Attack_3();
			break;
		case PlayAction.attack_4:
			cp.Play_Attack_4();
			break;
		case PlayAction.attack_5:
			cp.Play_Attack_5();
			break;
		case PlayAction.attack_6:
			cp.Play_Attack_6();
			break;
		case PlayAction.attack_7:
			cp.Play_Attack_7();
			break;
		case PlayAction.attack_8:
			cp.Play_Attack_8();
			break;
		case PlayAction.run_attack_dual:
			cp.Play_Run_Attack_Dual();
			break;
		case PlayAction.recover:
			cp.Play_Recover_From_Backward();
			cp.Play_Recover_From_Forward();
			break;
		case PlayAction.parry:
			cp.Play_Parry();
			break;
		case PlayAction.dodge_left:
			cp.Play_Dodge_To_Left();
			break;
		case PlayAction.dodge_right:
			cp.Play_Dodge_To_Right();
			break;
		case PlayAction.dodge_backward:
			cp.Play_Dodge_To_Backward();
			break;
		case PlayAction.dodge_forward:
			cp.Play_Dodge_To_Forward();
			break;
		case PlayAction.looking:
			cp.Play_Looking();
			break;
		case PlayAction.shooting:
			cp.Play_Shooting();
			break;
		case PlayAction.roar_1:
			cp.Play_Roar_1();
			break;
		case PlayAction.roar_2:
			cp.Play_Roar_2();
			break;
		case PlayAction.roar_3:
			cp.Play_Roar_3();
			break;
		}

		Finish();
	}


}
