using UnityEngine;
using System.Collections;

// *** this is a player controller, if a gameobject has this component that it will controlled by human

public class Player_Controller : MonoBehaviour {

	public float rotate_speed = 360.0f;
	public float mix_fixy = -1.0f, max_fixy = 5.0f , y_speed = 1.0f;

	public float reset_radius = 5.0f;

	bool auto_lock,mouse_lock;
	GameObject target;
	Character_Profile target_cp,cp;
	Smooth_Follow sf;

	#region System Method
	// Use this for initialization
	void Start () {
		cp = gameObject.GetComponent<Character_Profile>();
		sf = Camera.main.GetComponent<Smooth_Follow>();
		if (!sf)
		{
			Debug.Log("Camera need a Smooth_Follow");
		}
		if (!cp)
			Debug.Log("Need a Character_Profile.");

		Auto_Lock_Cursor();
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (mouse_lock)
		{
			float x = Input.GetAxis("Mouse X");
			Camera.main.transform.Rotate(0,x * rotate_speed * Time.deltaTime, 0);

			if (sf)
			{
			float y = Input.GetAxis("Mouse Y");
			sf.height = Mathf.Clamp(sf.height - (y * y_speed * Time.deltaTime), 
			                        mix_fixy, max_fixy);
			}
		}

		if (Input.GetButtonDown("Mouse Lock"))
		{
			Auto_Lock_Cursor();
		}

		if (Input.GetButtonDown("Reset AI"))
		{
			Reset_AI();
		}
	}

	void LateUpdate()
	{

	}

	void Reset_AI()
	{
		// find the closet target
		Collider[] colliders =  Physics.OverlapSphere(gameObject.transform.position, reset_radius);

		foreach(Collider coll in colliders)
		{
			Character_Profile cp = coll.gameObject.GetComponent<Character_Profile>();
			if (cp == null)
				continue;
			
			GameObject go = cp.gameObject;
			
			if (go.Equals(gameObject))
				continue;
			
			if (cp.Get_Is_Death())
				continue;
			
			if (!cp.enabled)
				continue;
			
			if (!go.tag.Equals(gameObject.tag))
				continue;

			// reset the Main AI
			PlayMakerFSM[] fsms = go.GetComponents<PlayMakerFSM>();
			foreach (PlayMakerFSM fsm in fsms)
			{
				if (fsm.FsmName.Equals("Main AI"))
				{
					Debug.Log("Reset");
					fsm.enabled = false;
					fsm.enabled = true;
					break;
				}
			}

		}
	}

	void Auto_Lock_Cursor()
	{
		mouse_lock = !mouse_lock;
		Screen.lockCursor = !Screen.lockCursor;
	}



	void Auto_Keep_To_Ground()
	{
		Vector3 currentPos = gameObject.transform.position;
		currentPos.y += 1.0f;

		Ray ray = new Ray(currentPos,Vector3.down);
		RaycastHit[] infos = Physics.RaycastAll(ray);
		foreach(RaycastHit info in infos)
		{
			if (info.collider.gameObject.tag.Equals("Ground"))
			{
				gameObject.transform.position = info.point;
				break;
			}
		}

	}



	#endregion System Method
}
