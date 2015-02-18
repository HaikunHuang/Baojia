using UnityEngine;
using System.Collections;

public class GUI_Text_Reset : MonoBehaviour {

	public float show_timing = 5.0f;
	float current_timing = 0.0f;
	string preString = "";

	// Use this for initialization
	void Start () 
	{
		gameObject.guiText.text = "";
		current_timing = show_timing;
		preString = gameObject.guiText.text;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// re-new the timing
		if (!preString.Equals(gameObject.guiText.text))
			current_timing = show_timing;

		if (!gameObject.guiText.text.Equals(""))
		{
			current_timing -= Time.deltaTime;
			if (current_timing <=0)
			{
				gameObject.guiText.text ="";
				current_timing = show_timing;
			}
		}

		if (gameObject.guiText.text.Equals(""))
		{
			current_timing = show_timing;
		}

		preString = gameObject.guiText.text;
	}


}
