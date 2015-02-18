using UnityEngine;
using System.Collections;

public class Tips_Trigger_Profile : MonoBehaviour {

	public GUIText target_guiText;
	public string tips;

	public AudioClip clip;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
		collider.isTrigger = true;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		ShowTips(other, tips);
		if (clip && other.gameObject.GetComponent<Player_Controller>())
			audio.PlayOneShot(clip);
	}

	void OnTriggerExit(Collider other) 
	{
		// ShowTips(other, "");
	}

	void ShowTips(Collider other, string str)
	{
		if (target_guiText)
		{
			if (other.gameObject.GetComponent<Player_Controller>())
			{
				target_guiText.text = str;
			}
		}
	}
}
