using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public enum animDir
{
	vertical, horizontal
}

public class Mode : MonoBehaviour
{

	[Header("Basic Settings")]
	public bool isActive = true;			
	public bool enableMask = true;			
	public float minSize = 1f;				
	public float maxSize = 1.10f;			

	[Header("Animation Settings")]
	public bool isAnim=true;				
	public animDir ad;						
	public float timerInitAnim;				
	public float axisStart = -700f;			
	public float animSpeed = 700f;			

	
	private RectTransform _rect;			
	private GameObject _mask;				
	private float _currentTime;				
	private Vector3 _initPos;				

	
	void Awake ()
    {
		startSettings ();					// Call the method "startSettings"
		if(isAnim == true)
        {
			resetAnim ();					// Call the method "resetAnim"
		}
	}
		
	void Update ()
    {		
		if(isAnim == true)
        {
			updateAnimation ();				// Call the method "updateAnimation"
		}
	}
    	
	void startSettings()
    {
		_rect = this.GetComponent<RectTransform> ();		
		_initPos = _rect.localPosition;						
		_currentTime = timerInitAnim;						

		if (enableMask==true && isActive==false)
        {			
			_mask = this.transform.GetChild (3).gameObject;	// Get the GameObject of mask
			_mask.SetActive (true);							// Enable the mask effect
		}
	}

	// Updating the Animation
	void updateAnimation()
    {
		// If the current time is less than zero
		if (_currentTime <= 0)
        {
			// The animation starts
			_rect.transform.localPosition = Vector2.MoveTowards (_rect.transform.localPosition, _initPos, animSpeed * Time.deltaTime);
		}
        else
        { 
			// The timer starts
			_currentTime -= Time.deltaTime;
		}
	}

	
	public void mouseEnter()
    {
		// If the game mode is active
		if(isActive)
			_rect.localScale = new Vector3 (maxSize, maxSize, maxSize); 
        
	}

	// Returns the default when the mouse exits
	public void mouseExit()
    {
		// If the game mode is active
		if(isActive)
			_rect.localScale = new Vector3 (minSize, minSize, minSize); // I set the size of the game mode (panel) with the value of the variable minSize
	}

	// When clicking / pressed
	public void mousePressed(string value)
    {
		// If the game mode is active
		if (isActive)
        {
			switch (value)
            {
			case "playmode1":								
				Debug.Log ("Play in Mode 1");				
				break;
			case "playmode2":								
				Debug.Log ("Play in Mode 2");				
				break;
			case "playmode3":								
				Debug.Log ("Play in Mode 3");				
				break;
			}
		}
	}
    	
	public void resetAnim()
    {
		setTimeInit ();										
		setPosInitAnim ();									
	}
	
	private void setTimeInit(){
		_currentTime = timerInitAnim;						
	}
    	
	private void setPosInitAnim()
    {
		switch (ad)
        {
		case animDir.vertical:								
			_rect.localPosition = new Vector2 (_rect.localPosition.x, axisStart); 
			break;
		case animDir.horizontal:
			_rect.localPosition = new Vector2 (axisStart, _rect.localPosition.y); 
			break;
		}
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