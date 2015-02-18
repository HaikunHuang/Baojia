using UnityEngine;
using System.Collections;

public class Camera_Facing_Billboard : MonoBehaviour {

	public TextMesh name_text, title_text;

	Camera cameraToLookAt;
	void Start()
	{
		cameraToLookAt = Camera.main;
	}
	
	void Update()
	{
		transform.LookAt(cameraToLookAt.transform);
	}
}
