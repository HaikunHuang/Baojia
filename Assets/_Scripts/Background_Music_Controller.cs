using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Background_Music_Controller : MonoBehaviour {


	GameObject player;

	// int status; // 0 normal, 1 combat.

	public AudioClip normal_clip, combat_clip;
	public float	normal_volume = 0.5f, combat_volume = 1.0f;

	public float radius = 15.0f;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindObjectOfType<Player_Controller>().gameObject;
		audio.volume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player)
		{
			bool is_combat = false;
			// find the closet target
			Collider[] colliders =  Physics.OverlapSphere(player.transform.position, radius);
			foreach(Collider coll in colliders)
			{
				Character_Profile cp = coll.gameObject.GetComponent<Character_Profile>();
				if (cp == null)
					continue;
				
				GameObject go = cp.gameObject;
				
				if (go.Equals(player))
					continue;
				
				if (cp.Get_Is_Death())
					continue;
				
				if (!cp.enabled)
					continue;
				
				if (go.tag.Equals(player.tag))
					continue;

				is_combat = true;
				break;
			}

			if (is_combat)
			{
				Play_Clip(combat_clip);
			}
			else
			{
				Play_Clip(normal_clip);
			}
		}
		else
		{
			player = GameObject.FindObjectOfType<Player_Controller>().gameObject;
		}
	}

	void Play_Clip(AudioClip clip)
	{
		if (clip)
		{
			if ( !audio.clip ||!audio.clip.Equals(clip))
			{
				if (audio.volume > 0.0f)
				{
					audio.volume -= Time.deltaTime * 0.2f;
				}
				else
				{
					if (audio.isPlaying)
						audio.Stop();
					audio.clip = clip;
					audio.Play();
				}
			}

			if ( audio.clip && audio.clip.Equals(clip))
			{
				audio.volume += Time.deltaTime;
				if (audio.clip.Equals(combat_clip))
					audio.volume = Mathf.Clamp(audio.volume, 0, combat_volume);
				else
					audio.volume = Mathf.Clamp(audio.volume, 0, normal_volume);
			}
		}
		else
		{
			if (audio.isPlaying)
			{
				if (audio.volume > 0.0f)
				{
					audio.volume -= Time.deltaTime * 0.2f;
					audio.volume = Mathf.Clamp(audio.volume, 0, 1.0f);
				}
				else
				{
					audio.Stop();
					audio.clip = null;
				}
			}
		}
	}

}
