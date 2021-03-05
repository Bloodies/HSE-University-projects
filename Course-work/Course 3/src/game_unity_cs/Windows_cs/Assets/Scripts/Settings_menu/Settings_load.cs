using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Скрипт для вызова панели настроек */
public class Settings_load : MonoBehaviour
{

	[Header("Transform")]
	public Transform[] rows;

	[Header("Organize Menu")]
	public int spaceDistance = 45;
	public int xAxis = 0;
	public int yAdjust = 260;

	void Start()
	{

	}

	void Update()
	{

	}

	public void OrganizeRows()
	{
		for (int i = 0; i < rows.Length; i++)
		{
			float a = -spaceDistance * i;
			rows[i].localPosition = new Vector3(this.transform.localPosition.x + xAxis, yAdjust + a, rows[i].localPosition.z);
		}
		Debug.Log("Rows Organized!");
	}
}