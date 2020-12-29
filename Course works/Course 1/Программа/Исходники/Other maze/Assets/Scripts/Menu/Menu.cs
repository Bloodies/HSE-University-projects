using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

/* Скрипт для хранения настроек о менюшке
 * Объявление шрифтов и мыши
 * В том числе и анимации для неё
 */

[System.Serializable]
public class Menu : MonoBehaviour {

	[Header("Menu Settings")]
	public bool active;									// Responsible variable if the button is active or not    / настройка для проверки активна кнопка или нет

	public int maxFontSize = 22;						// Maximum font size                                      / максимальный размер шрифта
	public int minFontSize = 18;						// Minimum  font size                                     / минимальный размер шрифта

	public Color mouseEnter;							// MenuItem color when the mouse enters                   / цвет мыши при включении
	public Color mouseExit;								// MenuItem color when the mouse exits                    / цвет мыши при выключении
	public Color mousePressed;							// MenuItem color when the mouse pressed / click          / мышь при нажатии 
	public Color deactivatedColor;						// MenuItem color when disabled

	[Header("Line Settings")]
	public bool enableLine;								// Enable underline                                       / подчеркивание для красоты
	public bool enableLineEffect;						// Enable effect when mouse enters                        / эффекты при наводе мыши
	public float widthLine=200f;						// Width underline                                        / ширина подчеркивания
	public float heightLineMin=1f;						// Minimum height underline                               / минимальная высота
	public float heightLineMax=2f;						// Maximum height underline                               / максимальная высота

	[Header("Animation Settings")]
	public bool initAnim;								// Initial animation                                      / инициализация подчеркивания
	public float timerInitAnim;							// Time for animation starts                              / время до начала анимации

	public float menuXStart = -233f;					// Initial X-axis                                         / инициализация х координат
	public float menuXEnd = 115f;						// End X-axis
	public float speedAnim = 400f;						// Speed animation

	[Header("Events")]
	[SerializeField]
	private UnityEvent Enter = new UnityEvent ();

	[SerializeField]
	private UnityEvent Exit = new UnityEvent ();

	[SerializeField]
	private UnityEvent Click = new UnityEvent ();

	// Variables that the user does not need to change
	private MenuControl _menuc;							// Menu Control Component
	private Image _effectSelected;						// Underline Component
	private Text _text;									// Text Component
	private Vector3 _initPos;							// Initial position
	private RectTransform _rect;						// RectTransform component

    void Start ()
    {
		getComponents ();
		basicSettings ();			
	}
	
	void Update () {
		if (initAnim == true) {
			updateAnimation ();
		}
	}

    //-----------------------------------------------------------------------------START METHODS MENUITEM--------------------------------------------------------------------\\
    #region
    void getComponents()
    {
		_rect = this.GetComponent<RectTransform> ();					// Get the RectTransform 
		_menuc = FindObjectOfType<MenuControl> ();						// Get the Control Menu 
		_text = this.GetComponent<Text> ();								// Get the Text component of children
		_effectSelected = this.GetComponentInChildren<Image> ();		// Get the Image component of children
	}

	// Basic and necessary settings
	void basicSettings()
    {
		_initPos = _rect.localPosition;														
        // Get initial position
		if (initAnim == true)
        {																
            // If the initial animation is true
			_rect.localPosition = new Vector3(menuXStart, _initPos.y, _initPos.z);			
            // Arrow the position of the object to the X axis of the variable "menuXStart"
		}

		//Set Default Color
		_text.color = mouseExit;
		_effectSelected.color = mouseExit;

		// If the button is not active
		if (active == false)
        {
			_text.color = deactivatedColor; // Set color to "deactivatedColor"
		}

		// If the underline is false it defines the effect of the underline as false too
		if (enableLine == false)
        {
			_effectSelected.enabled = false;
		}
			
	}// END

	// Update initial animation
	void updateAnimation()
    {
		
		if (timerInitAnim <= 0)
        {
			_rect.transform.localPosition = Vector2.MoveTowards (_rect.transform.localPosition, new Vector2 (menuXEnd, _initPos.y), speedAnim * Time.deltaTime); // Starts Animation
		}
		if (timerInitAnim >= 0)
        {
            timerInitAnim -= Time.deltaTime;
        } 
	}
    #endregion
    //-----------------------------------------------------------------------------END METHODS MENUITEM----------------------------------------------------------------------\\

    //-----------------------------------------------------------------------------START METHODS ON/OFF----------------------------------------------------------------------\\
    #region
    // (mouseExit)
    public void menuDisable()
    {
		if (active == true)
        {																			                    // If the button is active
			_text.color = mouseExit;																	// Sets the default color
			_effectSelected.color = mouseExit;															// Sets the default color (underline)
			if (enableLineEffect==true)
            {																                            // If the underline effect is active
				_effectSelected.rectTransform.sizeDelta = new Vector2 (widthLine, heightLineMin);		// Set a new size
			}
			_text.fontSize = minFontSize;																// Sets the default font size
		}
	}

	// (mouseEnter)
	public void menuEnable()
    {
		if (active == true)
        {																			                    // If the button is active
			_text.color = mouseEnter;																	// Sets new color (mouseEnter)
			_effectSelected.color = mouseEnter;															// Sets new color for underline (mouseEnter)
			if (enableLineEffect==true)
            {															                                // If the underline effect is active
				_effectSelected.rectTransform.sizeDelta = new Vector2 (widthLine, heightLineMax);		// Set a new size
			}
			_text.fontSize = maxFontSize;																// Sets font size to max font size
		}
	}

	// Set underline is active or no
	public void setMenuLine(bool value)
    {
		enableLine = value;																				// Set value
		_effectSelected.gameObject.SetActive (value);													// Set value
	}

	// Activate an object and mask it
	public void enableObject(GameObject obj)
    {
		obj.SetActive (true);																			// Active the object
        if (_menuc.inGame == false)
        {
            _menuc.mask.SetActive(true);                                                                // Active the mask
            _menuc.setAlphaMask(0.5f);                                                                  // Set alpha of mask to 0.5f
        }
	}
    #endregion
    //-----------------------------------------------------------------------------END METHODS ON/OFF------------------------------------------------------------------------\\

    public void CallTheEvent(int index)
    {
		if (index == 0)
        {
			Enter.Invoke ();
		} else if (index == 1)
        {
			Exit.Invoke ();
		} else if (index == 2) {
			Click.Invoke ();
		}
	}

	public void SetText(string newText)
    {
		this.GetComponent<Text> ().text = newText;
	}

	public void showMessageInConsole(string s)
    {
		Debug.Log (s);
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