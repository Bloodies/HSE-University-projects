using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScriptTrigger : MonoBehaviour
{
	public GameObject TextUp;
	public GameObject Paper;
	
	void Start() // Use this for initialization
	{

	}
	
	void Update() // Update is called once per frame
	{

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			TextUp.SetActive(true);
			//paperScript.enabled = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
		{
			Paper.SetActive(false);
			TextUp.SetActive(false);
			//paperScript.enabled = false;
		}
	}
}