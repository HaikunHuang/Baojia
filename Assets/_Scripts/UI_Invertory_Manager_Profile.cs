using UnityEngine;
using System.Collections;

public class UI_Invertory_Manager_Profile : MonoBehaviour {

	public enum TYPE
	{
		None,
		Player_Invertory,
		Store_Invertory
	};
	public TYPE type; 
	
	public TYPE transferTo;

	public UI_Preview_Profile preview_Profile;

	public UI_Invertory_Manager_Profile target_invertory_manager_profile;

	public Itemshot_Profile[] itemShots;

	int current_page = 0, max_page;


	// Use this for initialization
	void Start () 
	{
		Init();
	}

	public void Init()
	{

		Inventory_Profile[] ips = {};
		switch (type)
		{
		case TYPE.Player_Invertory:
			ips = Inventory_System_Profile.Get_Instance().player_inventory.ToArray();
			break;
			
		case TYPE.Store_Invertory:
			ips = Inventory_System_Profile.Get_Instance().store_inventory.ToArray();
			break;
		}
		
		int count_per_page = itemShots.Length;
		max_page = ips.Length / count_per_page + 1;
		
		// assign a item shot id to itemshots
		for (int i=0; i<itemShots.Length; i++)
		{
			int j = i + current_page * count_per_page;
			if (j < ips.Length)
			{
				// reset the values
				itemShots[i].inventory_profile = ips[j];
				itemShots[i].icon.enabled = true;
				itemShots[i].icon.mainTexture = ips[j].item_profile.icon;
				if (ips[j].item_profile.current_stack_number <= 1)
				{
					itemShots[i].count_Lable.enabled = false;
				}
				else
				{
					itemShots[i].count_Lable.enabled = true;
					itemShots[i].count_Lable.text = "" + ips[j].item_profile.current_stack_number;
				}
				
			}
			else
			{
				// reset the values
				itemShots[i].inventory_profile = null;
				itemShots[i].icon.enabled = false;
				itemShots[i].count_Lable.enabled = false;
			}
			
		}
		/*
		switch (type)
		{
		case TYPE.Player_Invertory:
			Init_Player_Invertory();
			break;

		case TYPE.Store_Invertory:
			Init_Store_Invertory();
			break;

		}*/

	}
	/*
	void Init_Player_Invertory () 
	{
		Inventory_Profile[] ips = Inventory_System_Profile.Get_Instance().player_inventory.ToArray();
		// assign a item shot id to itemshots
		for (int i=0; i<itemShots.Length; i++)
		{
			if (i < ips.Length)
			{
				// reset the values
				itemShots[i].inventory_profile = ips[i];
				itemShots[i].icon.enabled = true;
				itemShots[i].icon.mainTexture = ips[i].item_profile.icon;
				if (ips[i].item_profile.current_stack_number <= 1)
				{
					itemShots[i].count_Lable.enabled = false;
				}
				else
				{
					itemShots[i].count_Lable.enabled = true;
					itemShots[i].count_Lable.text = "" + ips[i].item_profile.current_stack_number;
				}

			}
			else
			{
				// reset the values
				itemShots[i].inventory_profile = null;
				itemShots[i].icon.enabled = false;
				itemShots[i].count_Lable.enabled = false;
			}

		}
	}

	void Init_Store_Invertory () 
	{

		int count_per_page = itemShots.Length;
		Inventory_Profile[] ips = Inventory_System_Profile.Get_Instance().store_inventory.ToArray();
		max_page = ips.Length / count_per_page + 1;

		// assign a item shot id to itemshots
		for (int i=0; i<itemShots.Length; i++)
		{
			int j = i + current_page * count_per_page;
			if (j < ips.Length)
			{
				// reset the values
				itemShots[i].inventory_profile = ips[j];
				itemShots[i].icon.enabled = true;
				itemShots[i].icon.mainTexture = ips[j].item_profile.icon;
				if (ips[j].item_profile.current_stack_number <= 1)
				{
					itemShots[i].count_Lable.enabled = false;
				}
				else
				{
					itemShots[i].count_Lable.enabled = true;
					itemShots[i].count_Lable.text = "" + ips[j].item_profile.current_stack_number;
				}
				
			}
			else
			{
				// reset the values
				itemShots[i].inventory_profile = null;
				itemShots[i].icon.enabled = false;
				itemShots[i].count_Lable.enabled = false;
			}
			
		}

	}
	*/
	// Update is called once per frame
	void Update () 
	{
	
	}


	public void Item_OnClick(Inventory_Profile inventory_profile)
	{
		//if (inventory_profile.item_id < 0)
		//	return;

		// from store to player
		if (type == TYPE.Store_Invertory && transferTo == TYPE.Player_Invertory)
		{
			if (Inventory_System_Profile.Add_Item_To_Player(inventory_profile.item_id))
			{
				if (Inventory_System_Profile.Reduce_Item_From_Inventory_Profile(inventory_profile))
				{
					
				}
				else
				{
					Debug.Log("item id: " + inventory_profile.item_id +" item did not exist.");
					Inventory_System_Profile.Reduce_Item_From_Player(inventory_profile.item_id);
				}
				Init();
				target_invertory_manager_profile.Init();
			}
		}

		// from player to store
		if (type == TYPE.Player_Invertory && transferTo == TYPE.Store_Invertory)
		{
			if (Inventory_System_Profile.Add_Item_To_Store(inventory_profile.item_id))
			{
				if (Inventory_System_Profile.Reduce_Item_From_Inventory_Profile(inventory_profile))
				{
					
				}
				else
				{
					Debug.Log("item id: " + inventory_profile.item_id +" item did not exist.");
					Inventory_System_Profile.Reduce_Item_From_Store(inventory_profile.item_id);
				}
				Init();
				target_invertory_manager_profile.Init();
			}
		}
	}

	public void Next_Page()
	{
		current_page = (current_page+1) % max_page;
		Init();
	}

	public void Prev_Page()
	{
		current_page = (current_page + max_page - 1) % max_page;
		Init();
	}
}
