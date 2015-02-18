using UnityEngine;
using System.Collections;

public class Global_Controller : MonoBehaviour {

	public GameObject item_system_prefab;
	public GameObject inventory_system_prefab;
	// Use this for initialization
	void Awake () 
	{
		Init();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void Init()
	{
		Init_Item_System();
		Init_Inventory_System();
	}

	// Item_System
	public void Init_Item_System()
	{
		if (item_system_prefab)
		{
			Item_System_Profile script = GameObject.FindObjectOfType<Item_System_Profile>();
			if (!script)
			{
				Instantiate(item_system_prefab);
			}
		}
	}
	// Inventory_System
	public void Init_Inventory_System()
	{
		if (inventory_system_prefab)
		{
			Inventory_System_Profile script = GameObject.FindObjectOfType<Inventory_System_Profile>();
			if (!script)
			{
				Instantiate(inventory_system_prefab);
			}
		}
	}
}
