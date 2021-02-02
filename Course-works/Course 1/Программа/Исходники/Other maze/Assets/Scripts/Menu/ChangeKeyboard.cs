using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region
/*                               Text / Script is read-only                                     */
/*                                                                                              */
/*                          This text is protected by copyright,                                */
/*                         copying and distribution is prohibited                               */
/*                                                                                              */
/*                             Script was created by Bloodies                                   */
/*                                                                                              */
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*──████████─────██───────────████████───████████───███████────██████───█████████───██████████──*/
/*─█░░░░░░░░█───█░░█─────────█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*─█░░████░░█───█░░█─────────█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████──█░░████████──*/
/*─█░░█──█░░█───█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────█░░█─────────*/
/*─█░░████░░███─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████───█░░████████──*/
/*─█░░░░░░░░░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░░░░░░░█──█░░░░░░░░░░█─*/
/*─█░░██████░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████────████████░░█─*/
/*─█░░█────█░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────────────█░░█─*/
/*─█░░██████░░█─█░░████████──█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████───████████░░█─*/
/*─█░░░░░░░░░░█─█░░░░░░░░░░█─█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*──██████████───██████████───████████───████████──████████────██████───█████████───██████████──*/
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*                                                                                              */
/*                          For partnership please contact here:                                */
/*                       -> bloodiesco@yandex.ru                                                */
/*                       -> kloko436@gmail.com                                                  */
/*                       -> https://vk.com/elikch                                               */
/*                       -> https://www.facebook.com/bloodiesprod                               */
/*                                                                                              */
/*                      © 20?? Elizar Chepokov All Rights Reserved                              */
#endregion

public class ChangeKeyboard : MonoBehaviour
{

	public CustomInput customInput;
	public Button[] button;

	public bool canChangeKey;
	public KeyCode newKey;
	private Event eventKey;
	public int indexChange=0;

	// Use this for initialization
	void Start ()
    {
		canChangeKey = false;
		SetKeys ();
		UpdateTextUI ();

	}
	
	// Update is called once per frame
	void Update ()
    {

	}

	void OnGUI()
    {
		button [0].onClick.AddListener 
            (delegate
            {
                ChangeKeyButton (0);
            });

		button [1].onClick.AddListener 
            (delegate 
            {
                ChangeKeyButton (1);
            });

		button [2].onClick.AddListener 
            (delegate 
            {
                ChangeKeyButton (2);
            });

		button [3].onClick.AddListener 
            (delegate 
            {
                ChangeKeyButton (3);
            });

		button [4].onClick.AddListener 
            (delegate 
            {
                ChangeKeyButton (4);
            });

		button [5].onClick.AddListener 
            (delegate 
            {
                ChangeKeyButton (5);
            });

		ChangeKey ();
		if (Input.anyKeyDown)
        {
			if (canChangeKey == true)
            {
				if (indexChange == 0)
                {
					customInput.forward = newKey;
				}
                else if (indexChange == 1)
                {
					customInput.back = newKey;
				}
                else if (indexChange == 2)
                {
					customInput.right = newKey;
				}
                else if (indexChange == 3)
                {
					customInput.left = newKey;
				}
                else if (indexChange == 4)
                {
					customInput.crouch = newKey;
				}
                else if (indexChange == 5)
                {
					customInput.jump = newKey;
				}
				SetKeyDefault ();
				UpdateTextUI ();
				button [indexChange].enabled = true;
				canChangeKey = false;
			}
		}
	}

	public void ChangeKeyButton(int indexKey)
    {
		indexChange = indexKey;
		canChangeKey = true;
		button [indexKey].GetComponentInChildren<Text> ().text = "WAIT FOR KEY";
		button [indexKey].enabled = false;
	}

	void ChangeKey()
    {
		eventKey = Event.current;
		newKey = eventKey.keyCode;
	}

	void UpdateTextUI()
    {
		button [0].GetComponentInChildren<Text> ().text = customInput.forward.ToString ();	
		button [1].GetComponentInChildren<Text> ().text = customInput.back.ToString ();
		button [2].GetComponentInChildren<Text> ().text = customInput.right.ToString ();
		button [3].GetComponentInChildren<Text> ().text = customInput.left.ToString ();
		button [4].GetComponentInChildren<Text> ().text = customInput.crouch.ToString ();
		button [5].GetComponentInChildren<Text> ().text = customInput.jump.ToString ();
	}

	public void SetKeyDefault()
    {
		customInput.forwardDefaultKey = customInput.forward.ToString ();
		customInput.backDefaultKey = customInput.back.ToString ();
		customInput.rightDefaultKey = customInput.right.ToString ();
		customInput.leftDefaultKey = customInput.left.ToString ();
		customInput.crouchDefaultKey = customInput.crouch.ToString ();
		customInput.jumpDefaultKey = customInput.jump.ToString ();
	}

	public void SetKeys()
    {
		customInput.forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), customInput.forwardDefaultKey);
		customInput.back = (KeyCode) System.Enum.Parse(typeof(KeyCode), customInput.backDefaultKey);
		customInput.right = (KeyCode) System.Enum.Parse(typeof(KeyCode), customInput.rightDefaultKey);
		customInput.left = (KeyCode) System.Enum.Parse(typeof(KeyCode), customInput.leftDefaultKey);
		customInput.crouch = (KeyCode) System.Enum.Parse(typeof(KeyCode), customInput.crouchDefaultKey);
		customInput.jump = (KeyCode) System.Enum.Parse(typeof(KeyCode), customInput.jumpDefaultKey);
	}
}
#region
/*                               Text / Script is read-only                                     */
/*                                                                                              */
/*                          This text is protected by copyright,                                */
/*                         copying and distribution is prohibited                               */
/*                                                                                              */
/*                             Script was created by Bloodies                                   */
/*                                                                                              */
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*──████████─────██───────────████████───████████───███████────██████───█████████───██████████──*/
/*─█░░░░░░░░█───█░░█─────────█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*─█░░████░░█───█░░█─────────█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████──█░░████████──*/
/*─█░░█──█░░█───█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────█░░█─────────*/
/*─█░░████░░███─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████───█░░████████──*/
/*─█░░░░░░░░░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░░░░░░░█──█░░░░░░░░░░█─*/
/*─█░░██████░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████────████████░░█─*/
/*─█░░█────█░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────────────█░░█─*/
/*─█░░██████░░█─█░░████████──█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████───████████░░█─*/
/*─█░░░░░░░░░░█─█░░░░░░░░░░█─█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*──██████████───██████████───████████───████████──████████────██████───█████████───██████████──*/
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*                                                                                              */
/*                          For partnership please contact here:                                */
/*                       -> bloodiesco@yandex.ru                                                */
/*                       -> kloko436@gmail.com                                                  */
/*                       -> https://vk.com/elikch                                               */
/*                       -> https://www.facebook.com/bloodiesprod                               */
/*                                                                                              */
/*                      © 20?? Elizar Chepokov All Rights Reserved                              */
#endregion