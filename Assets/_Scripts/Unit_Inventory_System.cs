using UnityEngine;
using System.Collections;

public class Unit_Inventory_System : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		Inventory_System_Profile.Add_Item_To_Player(0,2);
		Inventory_System_Profile.Add_Item_To_Player(2,100);

		Inventory_System_Profile.Add_Item_To_Store(1,8);

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
