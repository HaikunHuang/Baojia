using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Inventory_System_Profile : MonoBehaviour {

	
	public int money = 0;

	// player Inventory, player can use in combat, no money put in here
	public List<Inventory_Profile> player_inventory;
	int player_inventory_limit = 20;

	// store Inventory, only use in town sence, money put in here
	public List<Inventory_Profile> store_inventory;

	static Inventory_System_Profile isp;


	static public Inventory_System_Profile Get_Instance()
	{
		Init();
		return isp;
	}

	static public void Init()
	{
		if (!isp)
		{
			GameObject.FindObjectOfType<Global_Controller>().Init_Inventory_System();
			isp = GameObject.FindObjectOfType<Inventory_System_Profile>();
		}
	}

	static public bool Add_Item_To_Player(int item_id)
	{
		Init();

		Item_Profile item_ip = Item_System_Profile.Get_Item(item_id);
		GameObject item = null;
		if (item_ip != null)
		{
			item = item_ip.item_prefab;
		}
		else
		{
			return false;
		}

		// if item already exist
		foreach (Inventory_Profile ip in isp.player_inventory)
		{
			if (ip.Add_Stack(item))
			{
				return true;
			}
		}

		// if not, create a new inventory profile
		if (isp.player_inventory.Count < isp.player_inventory_limit )
		{
			//int item_id = Item_System_Profile.Get_Item_ID(item);
			// if exist
			if (item_id >=0 )
			{
				Inventory_Profile ip = new Inventory_Profile(item_id);
				isp.player_inventory.Add(ip);
				return true;
			}
			else
			{
				Debug.Log("Item not exist: " + item );
			}
		}

		return false;
	}

	static public bool Reduce_Item_From_Player(int item_id)
	{
		Init();

		// if item already exist
		foreach (Inventory_Profile ip in isp.player_inventory)
		{
			if (ip.item_id == item_id)
			{
				if (ip.Reduce_Stack())
				{
					if (ip.item_profile.current_stack_number <=0 )
					{
						isp.player_inventory.Remove(ip);
					}
					return true;
				}
				else
				{
					if (ip.item_profile.current_stack_number <=0 )
					{
						isp.player_inventory.Remove(ip);
					}
				}
			}
		}

		return false;
	}

	static public bool Reduce_Item_From_Inventory_Profile(Inventory_Profile inventory_profile)
	{
		bool b = inventory_profile.Reduce_Stack();
		if (b)
		{
			if (inventory_profile.item_profile.current_stack_number <=0)
			{
				isp.player_inventory.Remove(inventory_profile);
				isp.store_inventory.Remove(inventory_profile);

				// *** more list need to remove add here ***//
			}
		}
		return b;
	}


	// return how many item be added
	static public int Add_Item_To_Player(int item_id, int number)
	{
		int iRet = 0;
		for(int i=0;i<number;i++)
		{
			if (Add_Item_To_Player(item_id))
				iRet++;
		}

		return iRet;
	}

	static public int Reduce_Item_From_Player(int item_id, int number)
	{
		int iRet = 0;
		for(int i=0;i<number;i++)
		{
			if (Reduce_Item_From_Player(item_id))
				iRet++;
		}
		
		return iRet;
	}

	static public bool Add_Item_To_Store(int item_id)
	{
		Init();

		Item_Profile item_ip = Item_System_Profile.Get_Item(item_id);
		GameObject item = null;
		if (item_ip != null)
		{
			item = item_ip.item_prefab;
		}
		else
		{
			return false;
		}

		// if item already exist
		foreach (Inventory_Profile ip in isp.store_inventory)
		{
			if (ip.Add_Stack(item))
				return true;
		}
		
		// if not, create a new inventory profile
		//int item_id = Item_System_Profile.Get_Item_ID(item);
		// if exist
		if (item_id >= 0 )
		{
			Inventory_Profile ip = new Inventory_Profile(item_id);
			isp.store_inventory.Add(ip);
			return true;
		}
		else
		{
			Debug.Log("Item not exist: " + item );
		}

		return false;
	}

	static public bool Reduce_Item_From_Store(int item_id)
	{
		Init();
		
		// if item already exist
		foreach (Inventory_Profile ip in isp.store_inventory)
		{
			if (ip.item_id == item_id)
			{
				if (ip.Reduce_Stack())
				{
					if (ip.item_profile.current_stack_number <=0 )
					{
						isp.store_inventory.Remove(ip);
					}
					return true;
				}
				else
				{
					if (ip.item_profile.current_stack_number <=0 )
					{
						isp.store_inventory.Remove(ip);
					}
				}
			}
		}
		
		return false;
	}

	// return how many item be added
	static public int Add_Item_To_Store(int item_id, int number)
	{
		int iRet = 0;
		for(int i=0;i<number;i++)
		{
			if (Add_Item_To_Store(item_id))
				iRet++;
		}
		
		return iRet;
	}

	static public int Reduce_Item_From_Store(int item_id, int number)
	{
		int iRet = 0;
		for(int i=0;i<number;i++)
		{
			if (Reduce_Item_From_Store(item_id))
				iRet++;
		}
		
		return iRet;
	}

	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}


}
