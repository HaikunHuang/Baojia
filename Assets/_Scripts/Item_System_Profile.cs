using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item_System_Profile : MonoBehaviour {

	public Item_Profile[] items;

	static Item_System_Profile isp;

	static public void Init()
	{
		if (!isp)
		{
			GameObject.FindObjectOfType<Global_Controller>().Init_Item_System();
			isp = GameObject.FindObjectOfType<Item_System_Profile>();
		}
	}

	static public Item_Profile Get_Item(int index)
	{
		Init();

		if (isp.items.Length <= index || index < 0)
			return null;

		return isp.items[index].Copy();
	}

	static public int Get_Item_ID(GameObject go)
	{
		Init();

		for(int i=0; i<isp.items.Length; i++)
		{
			if (isp.items[i].Is_Equals_Item(go))
			{
				return i;
			}
		}
		return -1;
	}



	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
}


