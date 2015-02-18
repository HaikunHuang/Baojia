using UnityEngine;
using System.Collections;

[System.Serializable]
public class Itemshot_Profile : MonoBehaviour {

	public UI_Invertory_Manager_Profile invertory_manager_profile;
	public UITexture icon;
	public UILabel count_Lable;

	// auto assigned by manager
	//public int item_id;
	public Inventory_Profile inventory_profile;

	Vector3 original_pos;

	// *** colors *** ..
	Color name_color = new Color(1.0f,1.0f,1.0f,1.0f);
	Color descript_color = Color.yellow;

	// Use this for initialization
	void Start () 
	{
		original_pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnClick()
	{
		// test
		//Debug.Log(" item id : " + item_id);
		//gameObject.transform.position = original_pos;
		if (invertory_manager_profile && inventory_profile != null && icon.enabled != false)
		{
			invertory_manager_profile.Item_OnClick(inventory_profile);
		}
		Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
	}

	void OnDrag (Vector2 delta)
	{
		// test
		//Debug.Log(" my name : " + name);

		//gameObject.transform.position += new Vector3(delta.x,delta.y,0) * 0.005f;
		//if (invertory_manager_profile && inventory_profile != null && icon.enabled != false)
		//{
			//Texture2D t2d = (Texture2D)(icon.mainTexture);
			//t2d.Resize((int)(icon.localSize.x), (int)(icon.localSize.y));
			//Cursor.SetCursor(t2d, Vector2.zero, CursorMode.ForceSoftware);
		//}
	}

	
	void OnDrop (GameObject go)
	{
		// test
		//if (go)
			//Debug.Log(" my name : " + name + " , target name : "+ go.name);

			//Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
	}

	void OnMouseEnter() 
	{
		//Debug.Log(" OnMouseEnter" );
		if (invertory_manager_profile)
		{
			if (invertory_manager_profile.preview_Profile)
			{
				// *** show model *** //
				
				// show label
				if (invertory_manager_profile.preview_Profile.label)
				{
					string descript = "";
					
					switch (inventory_profile.item_profile.type)
					{
						//*** Money *** //
					case Item_Profile.Item_Type.Money:
					{
					}
						break;
						
						//*** Weapon *** //
					case Item_Profile.Item_Type.Weapon:
					{
						Weapon_Profile script = inventory_profile.item_profile.item_prefab.GetComponent<Weapon_Profile>();
						descript += "[" + NGUIText.EncodeColor(name_color) + "]" + script.weapon_name +"\n";
						descript += "[" + NGUIText.EncodeColor(descript_color) + "] descript:\n        "+script.comment;
					}
						break;
						
						//*** Armor *** //
					case Item_Profile.Item_Type.Armor:
					{
						Armor_Profile script = inventory_profile.item_profile.item_prefab.GetComponent<Armor_Profile>();
						descript += "[" + NGUIText.EncodeColor(name_color) + "][" + script.armor_name +"\n";
						descript += "[" + NGUIText.EncodeColor(descript_color) + "] descript:\n        "+script.comment;
					}
						break;
						
						//*** Consumables *** //
					case Item_Profile.Item_Type.Consumables:
					{
					}
						break;
						
						//*** Accessory *** //
					case Item_Profile.Item_Type.Accessory:
					{
						Accessory_Profile script = inventory_profile.item_profile.item_prefab.GetComponent<Accessory_Profile>();
						descript += "[" + NGUIText.EncodeColor(name_color) + "]" + script.accessory_name +"\n";
						descript += "[" + NGUIText.EncodeColor(descript_color) + "] descript:\n        "+script.comment;
					}
						break;
						
					}
					invertory_manager_profile.preview_Profile.label.text = descript;
					
					
				}
			}
		}
	}

	void OnMouseExit()
	{
		//Debug.Log(" OnMouseExit" );
		if (invertory_manager_profile)
		{
			if (invertory_manager_profile.preview_Profile.model_viewer)
			{
				//invertory_manager_profile.preview_Profile.model_viewer.mainTexture = "";
			}
			if (invertory_manager_profile.preview_Profile.label)
			{
				invertory_manager_profile.preview_Profile.label.text = "";
			}
		}
	
	}
	void OnTooltip (bool show)
	{
		if (show)
		{
			OnMouseEnter();
		}
		else
		{
			OnMouseExit();
		}

	}
}
