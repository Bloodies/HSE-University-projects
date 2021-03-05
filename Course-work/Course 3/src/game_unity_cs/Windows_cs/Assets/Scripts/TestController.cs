using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Скрипт для тестирования
 * После публикации будет не нужен
 */
public class TestController : MonoBehaviour
{

	public Keys_config InputManager;

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKey(InputManager.forward))
		{
			Debug.Log("The key " + InputManager.forward.ToString() + " has been pressed!");
		}

		if (Input.GetKey(InputManager.back))
		{
			Debug.Log("The key " + InputManager.back.ToString() + " has been pressed!");
		}

		if (Input.GetKey(InputManager.left))
		{
			Debug.Log("The key " + InputManager.left.ToString() + " has been pressed!");
		}

		if (Input.GetKey(InputManager.right))
		{
			Debug.Log("The key " + InputManager.right.ToString() + " has been pressed!");
		}

		if (Input.GetKey(InputManager.crouch))
		{
			Debug.Log("The key " + InputManager.crouch.ToString() + " has been pressed!");
		}

		if (Input.GetKey(InputManager.jump))
		{
			Debug.Log("The key " + InputManager.jump.ToString() + " has been pressed!");
		}
	}
}