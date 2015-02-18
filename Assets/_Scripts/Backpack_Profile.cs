using UnityEngine;
using System.Collections;

// *** this profile represented the back pack what items character has *** //

public class Backpack_Profile : MonoBehaviour {
	
	// weapon set must attach [Weapon Profile]
	public int weapon_set_item_id = -1;
	public GameObject 	weapon_set_prefab;

	// the real armor with the item profile.
	public GameObject 	boots,brasers,chest,/*body,hair,*/leg_armor,panties,shoulder_armor,mask,earrings,belt;

	#region System Method
	// load the data here
	void Awake()
	{
		if (!weapon_set_prefab)
		{
			Item_Profile ip = Item_System_Profile.Get_Item(weapon_set_item_id);
			if (ip != null)
			{
				if (ip.type == Item_Profile.Item_Type.Weapon)
					weapon_set_prefab = ip.item_prefab;
			}
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Copy_From(Backpack_Profile bp)
	{
		weapon_set_prefab = bp.weapon_set_prefab;

		boots = bp.boots;
		brasers = bp.brasers;
		chest = bp.chest;
		leg_armor = bp.leg_armor;
		panties = bp.panties;
		shoulder_armor = bp.shoulder_armor;
		mask = bp.mask;
		earrings = bp.earrings;
		belt = bp.belt;

	}

	#endregion System Method
}
