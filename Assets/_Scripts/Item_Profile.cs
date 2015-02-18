using UnityEngine;
using System.Collections;

// this class is a shell that containt the item ogject whatever it is. //

[System.Serializable]
public class Item_Profile /*: MonoBehaviour*/ {

	public GameObject item_prefab;

	public enum Item_Type
	{
		Money,
		Weapon,
		Armor,
		Consumables,
		Accessory
	}

	public Item_Type type;

	public Texture icon;

	// 0 for infinite
	public int stack_limit = 1;
	public int current_stack_number = 0;

	public int buy;
	public int sell;

	public Item_Profile Copy()
	{
		Item_Profile item = new Item_Profile();
		item.item_prefab = item_prefab;
		item.type = type;
		item.icon = icon;
		item.stack_limit = stack_limit;
		item.current_stack_number = 1;

		item.buy = buy;
		item.sell = sell;

		return item;
	}

	public Item_Profile Copy(int number)
	{
		Item_Profile item = new Item_Profile();
		item.item_prefab = item_prefab;
		item.type = type;
		item.icon = icon;
		item.stack_limit = stack_limit;
		item.current_stack_number = number;

		item.buy = buy;
		item.sell = sell;
		
		return item;
	}

	// *** Get Profile *** //
	public Weapon_Profile Get_Weapon_Profile()
	{
		return item_prefab.GetComponent<Weapon_Profile>();
	}

	public Armor_Profile Get_Armor_Profile()
	{
		return item_prefab.GetComponent<Armor_Profile>();
	}

	public bool Is_Equals_Item(GameObject go)
	{
		switch (type)
		{
			//*** Money *** //
		case Item_Type.Money:
			break;

			//*** Weapon *** //
		case Item_Type.Weapon:
			Weapon_Profile wp_my, wp_go;
			wp_my = item_prefab.GetComponent<Weapon_Profile>();
			wp_go = go.GetComponent<Weapon_Profile>();

			// no way to dose not has Weapon_Profile itself
			if (!wp_my)
			{
				Debug.Log("Item_Profile's item need a Weapon_Profile");
			}
			if (wp_go && wp_my)
			{
				return wp_my.weapon_name.Equals(wp_go.weapon_name);
			}
			break;

			//*** Armor *** //
		case Item_Type.Armor:
			// no way to dose not has Weapon_Profile itself
			Armor_Profile ap_my, ap_go;
			ap_my = item_prefab.GetComponent<Armor_Profile>();
			ap_go = go.GetComponent<Armor_Profile>();
			if (!ap_my)
			{
				Debug.Log("Item_Profile's item need a Armor_Profile");
			}
			if (ap_go && ap_my)
			{
				return ap_my.armor_name.Equals(ap_go.armor_name);
			}
			break;

			//*** Consumables *** //
		case Item_Type.Consumables:
			break;

			//*** Accessory *** //
		case Item_Type.Accessory:
			Accessory_Profile as_my, as_go;
			as_my = item_prefab.GetComponent<Accessory_Profile>();
			as_go = go.GetComponent<Accessory_Profile>();
			if (!as_my)
			{
				Debug.Log("Item_Profile's item need a Accessory_Profile");
			}
			if (as_go && as_my)
			{
				return as_my.accessory_name.Equals(as_go.accessory_name);
			}
			break;
		}

		return false;
	}



}
