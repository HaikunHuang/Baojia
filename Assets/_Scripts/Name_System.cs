using UnityEngine;
using System.Collections;

public class Name_System : MonoBehaviour {

	public enum Name_Type
	{
		None,
		Player,
		Gnome_Warrior,
		Gnome_Worker,
		Gnome_Archer,
		Elven_Hunter,
		Elven_Assassin,
		Gnome_Boss
	}

	static string[] Gnome_Warrior = {"Yellow","Yang Hua Zhang"};

	static string[] Gnome_Worker = {"Yellow","Yang Hua Zhang"};

	static string[] Gnome_Archer = {"Yellow","Yang Hua Zhang"};

	static string[] Elven_Hunter = {"Anne","Candice","Mary","Rose","Lily"};

	static string[] Elven_Assassin = {"Anne","Candice","Mary","Rose","Lily"};
	

	public static string Get_Name(Name_Type type)
	{
		switch (type)
		{
		case Name_Type.Player:
			return "Huang";
			break;
		case Name_Type.Gnome_Warrior:
			return Gnome_Warrior[Random.Range(0,Gnome_Warrior.Length)];
			break;
		case Name_Type.Gnome_Worker:
			return Gnome_Worker[Random.Range(0,Gnome_Worker.Length)];
			break;
		case Name_Type.Gnome_Archer:
			return Gnome_Archer[Random.Range(0,Gnome_Archer.Length)];
			break;
		case Name_Type.Elven_Hunter:
			return Elven_Hunter[Random.Range(0,Elven_Hunter.Length)];
			break;
		case Name_Type.Elven_Assassin:
			return Elven_Assassin[Random.Range(0,Elven_Assassin.Length)];
			break;
		}

		return "";
	}
}




