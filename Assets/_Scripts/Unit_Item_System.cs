using UnityEngine;
using System.Collections;

public class Unit_Item_System : MonoBehaviour {
	

	// Use this for initialization
	void Start () 
	{

		Item_Profile weapon = Item_System_Profile.Get_Item(0);
		GameObject weapon_go = Instantiate(weapon.item_prefab,new Vector3(1.0f,0,0), Quaternion.identity) as GameObject;
		Weapon_Profile wp = weapon.item_prefab.GetComponent<Weapon_Profile>();

		if (wp.weapon_main_prefab)
		{
			Instantiate(wp.weapon_main_prefab,new Vector3(1.1f,0,0), Quaternion.identity);
		}
		if (wp.weapon_second_prefab)
		{
			Instantiate(wp.weapon_second_prefab,new Vector3(0.9f,0,0), Quaternion.identity);
		}

		if (weapon.Is_Equals_Item(weapon_go))
		{
			//Debug.Log("weapon correct.");
		}
		
		Item_Profile armor = Item_System_Profile.Get_Item(1);
		GameObject armor_go = Instantiate(armor.item_prefab,new Vector3(-1.0f,0,0), Quaternion.identity) as GameObject;
		if (armor.Is_Equals_Item(armor_go))
		{
			//Debug.Log("armor correct.");
		}

	}
	
	// Update is called once per frame
	void Update () 
	{

	}


}
