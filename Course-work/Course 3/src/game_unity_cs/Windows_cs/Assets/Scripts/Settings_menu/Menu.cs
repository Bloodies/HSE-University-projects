using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/* Скрипт для хранения настроек о менюшке
 * Объявление шрифтов и мыши
 * В том числе и анимации для неё
 */

[System.Serializable]
public class Menu : MonoBehaviour
{

	[Header("Menu Settings")]
	public bool active;					// Responsible variable if the button is active or not    / настройка для проверки активна кнопка или нет

	public int maxFontSize = 22;		// Maximum font size                                      / максимальный размер шрифта
	public int minFontSize = 18;		// Minimum  font size                                     / минимальный размер шрифта

	public Color mouseEnter;			// MenuItem color when the mouse enters                   / цвет мыши при включении
	public Color mouseExit;				// MenuItem color when the mouse exits                    / цвет мыши при выключении
	public Color mousePressed;			// MenuItem color when the mouse pressed / click          / мышь при нажатии 
	public Color deactivatedColor;		// MenuItem color when disabled

	[Header("Line Settings")]
	public bool enableLine;				// Enable underline                                       / подчеркивание для красоты
	public bool enableLineEffect;		// Enable effect when mouse enters                        / эффекты при наводе мыши
	public float widthLine = 200f;		// Width underline                                        / ширина подчеркивания
	public float heightLineMin = 1f;	// Minimum height underline                               / минимальная высота
	public float heightLineMax = 2f;	// Maximum height underline                               / максимальная высота

	[Header("Animation Settings")]
	public bool initAnim;				// Initial animation                                      / инициализация подчеркивания
	public float timerInitAnim;			// Time for animation starts                              / время до начала анимации

	public float menuXStart = -233f;	// Initial X-axis                                         / инициализация х координат
	public float menuXEnd = 115f;		// End X-axis
	public float speedAnim = 400f;		// Speed animation

	[Header("Events")]
	[SerializeField]
	private UnityEvent Enter = new UnityEvent();

	[SerializeField]
	private UnityEvent Exit = new UnityEvent();

	[SerializeField]
	private UnityEvent Click = new UnityEvent();

	// Variables that the user does not need to change
	private Menu_controller _menuc;			// Menu Control Component
	private Image _effectSelected;		// Underline Component
	private Text _text;					// Text Component
	private Vector3 _initPos;			// Initial position
	private RectTransform _rect;		// RectTransform component

	void Start()
	{
		getComponents();
		basicSettings();
	}

	void Update()
	{
		if (initAnim == true)
		{
			updateAnimation();
		}
	}

	#region START METHODS MENUITEM
	void getComponents()
	{
		_rect = this.GetComponent<RectTransform>();				// Get the RectTransform 
		_menuc = FindObjectOfType<Menu_controller>();				// Get the Control Menu 
		_text = this.GetComponent<Text>();						// Get the Text component of children
		_effectSelected = this.GetComponentInChildren<Image>();	// Get the Image component of children
	}
	
	void basicSettings()			// Basic and necessary settings
	{
		_initPos = _rect.localPosition;		
		if (initAnim == true)		// Get initial position
		{
			// If the initial animation is true
			_rect.localPosition = new Vector3(menuXStart, _initPos.y, _initPos.z);
			// Arrow the position of the object to the X axis of the variable "menuXStart"
		}

		//Set Default Color
		_text.color = mouseExit;
		_effectSelected.color = mouseExit;
				
		if (active == false)				// If the button is not active
		{
			_text.color = deactivatedColor; // Set color to "deactivatedColor"
		}
		
		if (enableLine == false)			// If the underline is false it defines the effect of the underline as false too
		{
			_effectSelected.enabled = false;
		}

	}// END
		
	void updateAnimation()					// Update initial animation
	{

		if (timerInitAnim <= 0)
		{
			_rect.transform.localPosition = Vector2.MoveTowards(_rect.transform.localPosition, new Vector2(menuXEnd, _initPos.y), speedAnim * Time.deltaTime); // Starts Animation
		}
		if (timerInitAnim >= 0)
		{
			timerInitAnim -= Time.deltaTime;
		}
	}
	#endregion END METHODS MENUITEM

	#region START METHODS ON/OFF	
	public void menuDisable()		// (mouseExit)
	{
		if (active == true)
		{																							// If the button is active
			_text.color = mouseExit;																// Sets the default color
			_effectSelected.color = mouseExit;														// Sets the default color (underline)
			if (enableLineEffect == true)
			{																						// If the underline effect is active
				_effectSelected.rectTransform.sizeDelta = new Vector2(widthLine, heightLineMin);	// Set a new size
			}
			_text.fontSize = minFontSize;															// Sets the default font size
		}
	}
	
	public void menuEnable()		// (mouseEnter)
	{
		if (active == true)
		{																							// If the button is active
			_text.color = mouseEnter;																// Sets new color (mouseEnter)
			_effectSelected.color = mouseEnter;														// Sets new color for underline (mouseEnter)
			if (enableLineEffect == true)
			{																						// If the underline effect is active
				_effectSelected.rectTransform.sizeDelta = new Vector2(widthLine, heightLineMax);	// Set a new size
			}
			_text.fontSize = maxFontSize;															// Sets font size to max font size
		}
	}
	
	public void setMenuLine(bool value)					// Set underline is active or no
	{
		enableLine = value;								// Set value
		_effectSelected.gameObject.SetActive(value);	// Set value
	}
	
	public void enableObject(GameObject obj)			// Activate an object and mask it
	{
		obj.SetActive(true);							// Active the object
		if (_menuc.inGame == false)
		{
			_menuc.mask.SetActive(true);				// Active the mask
			_menuc.setAlphaMask(0.5f);					// Set alpha of mask to 0.5f
		}
	}
	#endregion END METHODS ON/OFF

	public void CallTheEvent(int index)
	{
		if (index == 0)
		{
			Enter.Invoke();
		}
		else if (index == 1)
		{
			Exit.Invoke();
		}
		else if (index == 2)
		{
			Click.Invoke();
		}
	}

	public void SetText(string newText)
	{
		this.GetComponent<Text>().text = newText;
	}

	public void showMessageInConsole(string s)
	{
		Debug.Log(s);
	}
}