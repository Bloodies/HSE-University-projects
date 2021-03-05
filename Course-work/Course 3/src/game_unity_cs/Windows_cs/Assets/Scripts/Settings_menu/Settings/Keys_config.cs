using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys_config : MonoBehaviour
{

	public KeyCode forward { get; set; }
	public KeyCode back { get; set; }
	public KeyCode left { get; set; }
	public KeyCode right { get; set; }
	public KeyCode crouch { get; set; }
	public KeyCode jump { get; set; }

	public string forwardDefaultKey;
	public string backDefaultKey;
	public string leftDefaultKey;
	public string rightDefaultKey;
	public string crouchDefaultKey;
	public string jumpDefaultKey;

	// Use this for initialization
	void Awake()
	{
		forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", forwardDefaultKey));
		back = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backKey", backDefaultKey));
		left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", leftDefaultKey));
		right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", rightDefaultKey));
		crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchKey", crouchDefaultKey));
		jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", jumpDefaultKey));
	}

	// Update is called once per frame
	void Update()
	{

	}
}