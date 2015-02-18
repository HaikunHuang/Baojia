using UnityEngine;
using System.Collections;

[System.Serializable]
public class Inventory_Profile 
{

	public int item_id = -1;

	public Item_Profile item_profile;
	//public Item_Profile Get_Item_Profile() {return item_profile;}

	public Inventory_Profile (int id)
	{
		Create(id);
	}

	void Create (int id)
	{
		Item_Profile it = Item_System_Profile.Get_Item(id);
		if (it != null)
		{
			item_id = id;
			item_profile = it.Copy();
		}
		else
		{
			item_id = -1;
			item_profile = null;

		}
	}


	public bool Ready_To_stack(GameObject other)
	{
		return item_profile.Is_Equals_Item(other);
	}

	public bool Add_Stack(int id)
	{
		if (item_id == id)
		{
			if (item_profile.stack_limit == 0)
			{
				item_profile.current_stack_number ++;
				return true;
			}
			else
			{
				if (item_profile.current_stack_number < item_profile.stack_limit)
				{
					item_profile.current_stack_number ++;
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		return false;
	}

	public bool Add_Stack(int id, int number)
	{
		if (item_id == id)
		{
			if (item_profile.stack_limit == 0)
			{
				item_profile.current_stack_number += number;
				return true;
			}
			else
			{
				if (item_profile.current_stack_number +number  <= item_profile.stack_limit)
				{
					item_profile.current_stack_number +=number;
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		return false;
	}


	public bool Add_Stack(GameObject other)
	{
		if (Ready_To_stack(other))
		{
			if (item_profile.stack_limit == 0)
			{
				item_profile.current_stack_number ++;
				return true;
			}
			else
			{
				if (item_profile.current_stack_number < item_profile.stack_limit)
				{
					item_profile.current_stack_number ++;
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		return false;
	}

	public bool Add_Stack(GameObject other, int number)
	{
		if (Ready_To_stack(other))
		{
			if (item_profile.stack_limit == 0)
			{
				item_profile.current_stack_number += number;
				return true;
			}
			else
			{
				if (item_profile.current_stack_number + number <= item_profile.stack_limit)
				{
					item_profile.current_stack_number +=number;
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		return false;
	}

	// also reduce current stack 1
	public bool Reduce_Stack()
	{
		if (item_profile.current_stack_number > 0 )
		{
			item_profile.current_stack_number --;
			return true;
		}

		return false;
	}

	public bool Reduce_Stack(int number)
	{
		if (item_profile.current_stack_number >= number)
		{
			item_profile.current_stack_number -= number;
			return true;
		}

		return false;
	}

	public bool Is_Empty()
	{
		return item_profile.current_stack_number == 0;
	}
}

