using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

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

#region
public enum PlaysType
{
	Direct, Modes, LevelIsIncomplete
}

public enum CreditsAnimationType
{
    Direction, Scale
}

public enum DirectionCredits
{
	up, down
}
#endregion

[RequireComponent(typeof(OptionsControl))]
[RequireComponent(typeof(LanguageControl))]
[RequireComponent(typeof(MenuSelect))]
[RequireComponent(typeof(LevelSelect))]
[RequireComponent(typeof(UITransition))]
public class MenuControl : CustomInput {

	[Header("Objects Settings")]
	[Space]
	public GameObject mask;								// Mask to make non clicable and dark effect
	public GameObject mainScreen;						// Object of the main screen
	public GameObject creditsScreen;					// Object of the credits screen
	public GameObject modesScreen;
	public GameObject levelScreen;
	public GameObject optionsScreen;
	public GameObject creditsTextBack;					// Object of text credits "Back to go main screen"
    public GameObject Canvas;

    [Header("Global Menu Settings")]
	[Space]
	//[Tooltip("This option disables underscores in the menu items")]
	//public bool disableLine;							// Underline in the menu items
	public bool useCustomCursor=true;   				// Enable custom cursor?
	public bool useKeyboard = false;					// Enable navigation using keyboard
    [HideInInspector]
    public bool isActiveCanvas = false;                  // Enable canvas/pause?
    public bool inGame = false;
	public Dropdown language;
	public Texture2D cursor;							// Custom Cursor Image
	public Menu[] menu = new Menu[0];					// List of all menu item

	[Header("Play Settings")]
	[Space]
	[Tooltip("If there is a game mode (example: Singleplayer, multiplayer, training), it is disabled")]
	//public bool noModes = true;							// There are game modes
	public PlaysType playsType;
	[Tooltip("If you turn on every time you click play, the initial animation in the mode screen is active")]
	public bool resetAnim = true;						// Do you want to enable / disable animation of the modes screen whenever you click play
	public string nameLoadLevel;						// Name of the game level you want to load
	public Mode[] modes;                                // List of all modes of game

    [Header("Credits Settings")]
    [Space]
    public CreditsAnimationType animationType;          // Type of animations for Credits Screen
	//public bool animCreditsScale = true;				// Enable scaling animation
	//public bool animDirectionCredits = true;			// Enable direction animation
	public float YCreditsEnd;							// Y axis where the direction animation ends
	public DirectionCredits dirCredits;					// Direction where credits go
	public float initScale = 0.2f;						// Initial Scale
	public float maxScale = 1f;							// Maximum scale
	public float speedAnimScaleCredits = 0.75f;			// Scale animation speed
	public float speedAnimDirCredits = 50f;				// Steering animation speed

	[Header("Audio Manager")]
	[Space]
	public bool canPlayBackgroundSong=true;
	public AudioSource backgroundAudio;
	public AudioSource[] audio = new AudioSource[0];

	[Header("Custom Input")]
	[Space]

	// Variables that the user does not need to change
	private UITransition _uitransition;

	// Cursor
	private Vector2 _hotSpot = Vector2.zero;

	// Credits
	private Vector3 _initCreditsPos;					// Variable responsible for the initial position of creditScreen
	private RectTransform _rectCredits;					// Variable responsible for the RectTransform component
	private float _currentScale;						// Variable responsible for the current scale of Animation Credits Scale
	private bool _startScaleAnimCredits = false;		// Responsible variable if scaling animation can start
	private bool _startDirAnimCredits = false;			// Responsible variable if the steering animation can start
	private bool _inCreditsScreen = false;				// Responsible variable if the credits screen is active or not
	private bool _inModesScreen = false;				// Responsible variable if the modes screen is active or not
	private bool _inLevelScreen = false;				// Responsible variable if the level screen is active or not
	private bool _inOptionsScreen = false;				// Responsible variable if the options screen is active or not

	//Editor
	[HideInInspector]
	public int currentTab;
	[HideInInspector]
	public int currentTabTwo;
	[HideInInspector]
	public int previousTab = -1;
	[HideInInspector]
	public int previousTabTwo = -1;

	[Header("Organize Menu")]
	public int spaceMenu=45;
	public int xSpace=100;
	public int yAdjust=0;

	[Header("Organize Modes")]
	public int spaceModes=250;
	public int xStart=250;

	// Use this for initialization
	void Start ()
    {
		basicSettings ();
		firstCreditsSettings ();
		reportErrors ();
        if(inGame == true)
            Canvas.SetActive (isActiveCanvas);
    }

	// Update is called once per frame
	void Update ()
    {
		//-------------------------------------------------------------------------START MODES/LEVEL: METHODS CALL---------------------------------------------------------------\\
		quitModesScreen ();
		quitLevelScreen ();
		//-------------------------------------------------------------------------END MODES: METHODS CALL-----------------------------------------------------------------------\\
        
		//-------------------------------------------------------------------------START OPTIONS: METHODS CALL-------------------------------------------------------------------\\
		quitOptionsCredits ();
		//-------------------------------------------------------------------------END OPTIONS: METHODS CALL---------------------------------------------------------------------\\
            
		//-------------------------------------------------------------------------START CREDITS: METHODS CALL-------------------------------------------------------------------\\
		updateAnimScaleCredits ();     // Animation of scale for the Credits
		updateAnimDirectionCredits (); // Animation of direction for the Credits
		quitScreenCredits ();          // Exit credit screen
        //-------------------------------------------------------------------------END CREDITS: METHODS CALL---------------------------------------------------------------------\\

        UpdateCanvasEnable();
	}


    //-----------------------------------------------------------------------------START METHODS MENU CONTROL--------------------------------------------------------------------\\
    #region
    // Set the basics settings
    void basicSettings(){
		// Active or deactivated first objets in scene
		mainScreen.SetActive (true);
		creditsScreen.SetActive (false);
		creditsTextBack.SetActive (false);
		modesScreen.SetActive (false);
		levelScreen.SetActive (false);
		optionsScreen.SetActive (false);
        QualitySettings.masterTextureLimit = 1;


        if (!File.Exists (Application.persistentDataPath + "/gamesettings.json"))
        {
			GameConfig _gameConfig;
			_gameConfig = new GameConfig ();
			string jsonData = JsonUtility.ToJson(_gameConfig, true);								// Converts the class to the json file
			File.WriteAllText (Application.persistentDataPath + "/gamesettings.json", jsonData);	// Save the jsonData (class _gameConfig) in persistenceData
			Debug.LogWarning ("MENU_KIT: gamesettings.json was created!");
		}

        if (inGame == false)  setAlphaMask (0.5f);
        else                  setAlphaMask (0.7f);

		// Get all components
		_rectCredits = creditsScreen.GetComponent<RectTransform>();
		_uitransition = this.GetComponent<UITransition> ();

		// Set Custom Cursor
		if(useCustomCursor == true && inGame == false)
			Cursor.SetCursor(cursor, _hotSpot, CursorMode.ForceSoftware);

		// Play background song
		if (canPlayBackgroundSong)
        {
			playBackgroundAudio ();
			Debug.Log ("Background Audio is Play");
		}

	}// END

    void UpdateCanvasEnable()
    {
        if (inGame == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasCloseOpen ();
                setAlphaMask(0.7f);
            }
        }
    }

    public void CanvasCloseOpen()
    {
        isActiveCanvas = !isActiveCanvas;
        if (isActiveCanvas == false) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        else {
            mask.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            Time.timeScale = 0;
        }
        Canvas.SetActive(isActiveCanvas);
        //resetMenu();
    }

	
	void reportErrors()
    {
		if (mask == null) {Debug.LogError ("MENUKIT: Variable 'mask' is not declared");}
		if (mainScreen == null) {Debug.LogError ("MENUKIT: Variable 'mainScreen' is not declared");}
		if (creditsScreen == null) {Debug.LogError ("MENUKIT: Variable 'creditsScreen' is not declared");}
		if (modesScreen == null) {Debug.LogError ("MENUKIT: Variable 'modesScreen' is not declared");}
		if (creditsTextBack == null) {Debug.LogError ("MENUKIT: Variable 'creditsTextBack' is not declared");}
	}

	// RESET ALL MENUS TO STANDARD STATE 
	void resetMenu()
    {
		foreach (Menu m in menu)
        {										
           
			m.menuDisable ();
            
		}
	}

	// RESET ALL MODE TO STANDARD STATE 
	void resetMode()
    {
		foreach (Mode m in modes)
        {									
            // Scrolls through the entire modes list
			m.mouseExit ();													// Become standard
		}
	}

	// Set Alpha Mask with value
	public void setAlphaMask(float value)
    {
		mask.GetComponent<CanvasRenderer>().SetAlpha(value); // Set Alpha of CanvasRenderer
	}
    #endregion
    //-------------------------------------------------------------------------END METHODS MENU CONTROL----------------------------------------------------------------------------\\

    public void ContinuePlay()
    {
        fadeInEffect();
        SLScene.SavingSystem.OpenSavedScene(); //загрузка
    }

    //-------------------------------------------------------------------------START METHODS PLAY, MODES, LEVEL SCREEN--------------------------------------------------------------\\
    #region
    // Play Button
    public void StartGame()
    {
        SLScene.SavingSystem.ResetSceneSave();
       // SLScene.SavingSystem.OpenSavedScene();
		if (playsType == PlaysType.Direct)
        {
			fadeInEffect ();														// Load Level Game
		}
        else if (playsType == PlaysType.LevelIsIncomplete)
        {
			setLevelScreen (true);													// The level screen is active
		}
        else if (playsType == PlaysType.Modes)
        {
			setModesScreen (true);													// The mode screen is active
		}
	}

	// Fade Effect
	void fadeInEffect()
    {
		// Start Fade Effect
		mask.SetActive (true);
		_uitransition.Play();
	}

	// Loads game level
	public void loadLevelGame()
    {
		if (nameLoadLevel != null)
        {                                                                               
            // If the name of the game level to be loaded is not empty
            // Application.LoadLevel (nameLoadLevel);									
            // Load game level
            SceneManager.LoadScene (Application.loadedLevel + 1, LoadSceneMode.Single);
		}
        else
        {																	            // ELSE
			Debug.LogWarning ("Variable nameLoadLevel is "+nameLoadLevel);			    // Warns that the variable is empty
		}
	}

	// Updating the Modes Exit Button
	void quitModesScreen()
    {
		// If you press the "esc" key and the modes screen is active
		if (_inModesScreen == true)
        {
			if (Input.GetKeyDown (KeyCode.Escape))
            {
				setModesScreen (false); // Disable modes screen
			}
		}
	} 

	// Updating the Level Exit Button
	void quitLevelScreen()
    {
		// If you press the "esc" key and the modes screen is active
		if (_inLevelScreen == true)
        {
			if (Input.GetKeyDown (KeyCode.Escape))
            {
				setLevelScreen (false); // Disable level screen
			}
		}
	} 

	// Changes between the main screen and the modes screen
	void setModesScreen(bool value)
    {
		if (value)
        {
			mainScreen.SetActive (false);			// Disables the Main Screen
			modesScreen.SetActive (true);			// Active the Mode Screen
			_inModesScreen = true; 					// In modeScreen
			resetMode();							// Reset all modes for the standart state
			if (resetAnim)
            {										// If the option to enable / reset the initial animation of the modes screen
				foreach (Mode mode in modes)
                {									// It passes through all modes modes within the variable "modes"
					mode.isAnim = true;				// Can move
					mode.resetAnim ();				// And call the method by resetting position and time
				}
			}
		}
        else
        {
			modesScreen.SetActive (false);			// Disables the Modes Screen
			mainScreen.SetActive (true);			// Active the Main Screen
			_inModesScreen = false;					// Quit mode screen
			resetMenu();							// Reset all menus to standart state
		}
	}

	// Changes between the main screen and the level select screen
	void setLevelScreen(bool value)
    {
		if (value)
        {
			mainScreen.SetActive (false);											// Disables the Main Screen
			levelScreen.SetActive (true);											// Active the Level Screen
			_inLevelScreen = true; 													// In levelScreen
		}
        else
        {
			mainScreen.SetActive (true);											// Disables the Modes Screen
			levelScreen.SetActive (false);											// Active the Main Screen
			//_inLevelScreen = false;													// Quit level screen
			resetMenu();															// Reset all menus to standart state
		}
	}
    #endregion
    //-------------------------------------------------------------------------END METHODS PLAY/MODES SCREEN---------------------------------------------------------------------\\

    // УБРАНО ИЗ ИГРЫ
    //-------------------------------------------------------------------------START METHODS CREDITS SCREEN----------------------------------------------------------------------\\
    #region
    // Starts initial credit settings
    void firstCreditsSettings()
    {
		_initCreditsPos = _rectCredits.localPosition; 					 				// Get First Position of init Credits
		if (animationType == CreditsAnimationType.Scale)
        { 
			_rectCredits.localScale = new Vector2 (initScale, initScale); 				// Set scale for initScale
			_currentScale = initScale;
		}
	} 

	// Update the scale animation of credits
	void updateAnimScaleCredits(){
        if (animationType == CreditsAnimationType.Scale)
        {                              // If a scale animation to active
            if (_currentScale <= maxScale && _startScaleAnimCredits == true)
            {			
                // If the scale is smaller than the maximum scale and you can start the scale animation
				_currentScale += speedAnimScaleCredits * Time.deltaTime;				// The "currentScale" variable begins to increase
				_rectCredits.localScale = new Vector2 (_currentScale, _currentScale);	// The scale of the credits object is updated with the variable "currentScale"
			}

			if (_currentScale >= maxScale)
            {											
                // If the scale of the object is already in the defined size
				_startDirAnimCredits = true;											// Move animation can start
			}
		}
	} 

	// Updating the Credits Exit Button
	void quitScreenCredits()
    {
		// If you press the "esc" key or the credits finish the direction animation and the credit screen is active
		if (_inCreditsScreen == true)
        {
			if (Input.GetKeyDown (KeyCode.Escape) || _rectCredits.localPosition.y >= YCreditsEnd)
            {
				setCreditsScreen (false); // Disable credit screen
			}
		}
	} 


	// Update the direction animation of credits
	void updateAnimDirectionCredits()
    {
		if (animationType == CreditsAnimationType.Direction && _startDirAnimCredits == true)
        {				
            // If the steering animation is active and it can start
			if (dirCredits == DirectionCredits.up)
            {									               
                // If the animation is up
				// Rises with speed defined in variable speedAnimDirCredits
				_rectCredits.localPosition = new Vector2 (_rectCredits.localPosition.x, _rectCredits.localPosition.y + (speedAnimDirCredits * Time.deltaTime));
			}
            else
            {																	              
                // If the animation is down
				// Descends with the velocity defined in the variable speedAnimDirCredits
				_rectCredits.localPosition = new Vector2 (_rectCredits.localPosition.x, _rectCredits.localPosition.y - (speedAnimDirCredits * Time.deltaTime));
			}
		}
	} 

	// Changes between the main screen and the credits screen
	public void setCreditsScreen(bool value)
    {
        // Go to Credits Screen
        if (value == true)
        {
            // All back to the initial configuration
            _rectCredits.localPosition = _initCreditsPos;                               // Return to starting position
            if (animationType == CreditsAnimationType.Scale)
            {                                                                           // If scale animation is active
                _rectCredits.localScale = new Vector2(initScale, initScale);            // Back to credits scale for the initial
                _currentScale = initScale;                                              // The current scale has the same value as the initial
                _startScaleAnimCredits = true;                                          // Scaling animation starts
            }
            else
            {                                                                           // ELSE
                _startDirAnimCredits = true;                                            // Direction animation starts
            }

            // Active or deactivated the objects
            creditsTextBack.SetActive(true);                                            // Active text "back to go main screen"
            mainScreen.SetActive(false);                                                // Disables the Main Screen
            creditsScreen.SetActive(true);												// Active the Credits Screen
            if (inGame == false)
            {
                setAlphaMask(0.5f);
                mask.SetActive(true);                                                       // Active the Mask
            }

            _inCreditsScreen = true;                                                    // Credits screen is active
        }
        else
        { // Go to Main Screen
          // Animations are stopped
            _startScaleAnimCredits = false;                                             // Scaling animation is disabled
            _startDirAnimCredits = false;                                               // Direction animation is disabled

            // Active or deactivated the objects
            creditsTextBack.SetActive(false);                                           // Disables text "back to go main screen"
            creditsScreen.SetActive(false);                                             // Disables the Credits Screen
            mainScreen.SetActive(true);												    // Active the Main Screen
            if (inGame == false)
                mask.SetActive(false);                                                  // Disables the Mask

			_inCreditsScreen = false;													// Credits screen is disables
			resetMenu();																// Reset all menus to standart state
		}
	}
    #endregion
    //-------------------------------------------------------------------------END METHODS CREDITS SCREEN------------------------------------------------------------------------\\

    //-------------------------------------------------------------------------START METHODS OPTIONS SCREENS---------------------------------------------------------------------\\
    #region
    public void setOptionsScreen(bool value)
    {
		if (value)
        {											// If true
			mainScreen.SetActive (false);			// Disable main screen
			optionsScreen.SetActive (true);			// Active Options Screen
			_inOptionsScreen = true;				// On in Options Screen
		}
        else
        {											// ELSE
			optionsScreen.SetActive (false);		// Disable Options Screen
			mainScreen.SetActive (true);			// Active Main Screen
			_inOptionsScreen = false;				// Exits of Options Screen
			resetMenu ();							// 
		}
	}

	// Updating the Credits Exit Button
	void quitOptionsCredits()
    {
		// If you press the "esc" key or the credits finish the direction animation and the credit screen is active
		if (_inOptionsScreen == true)
        {
			if (Input.GetKeyDown (KeyCode.Escape))
            {
				setOptionsScreen (false); // Disable credit screen
			}
		}
	}
    #endregion
    //-------------------------------------------------------------------------END METHODS OPTIONS SCREENS-----------------------------------------------------------------------\\

    // Play audio by index in list AUDIO
    public void playAudio(int index)
    {
		audio [index].Play ();
	}

    // Play background song
	public void playBackgroundAudio()
    {
		if (backgroundAudio != null)
        {
			backgroundAudio.Play ();
		}
	}

	public void SetDefaultValues()
    {
		useCustomCursor = true;
		resetAnim = false;
		nameLoadLevel = "Scenes/Home/Home";
        animationType = CreditsAnimationType.Direction;
        YCreditsEnd = 400;
		dirCredits = DirectionCredits.up;
		initScale = 0f;
		maxScale = 1f;
		speedAnimDirCredits = 0.75f;
		speedAnimDirCredits = 50f;

		spaceMenu=45;
		xSpace=100;
		yAdjust=0;

		spaceModes=250;
		xStart=250;
	}

	public void OrganizeMenus()
    {
		for (int i = 0; i < menu.Length; i++)
        { 
			float a = -spaceMenu * i;
			menu [i].transform.localPosition = new Vector3 (xSpace, yAdjust+a, menu[i].transform.localPosition.z);
		}
		Debug.Log ("Menus Organized!");
	}

	public void OrganizeModes()
    {
		for (int i = 0; i < modes.Length; i++)
        { 
			float a = spaceModes * i;
			modes [i].transform.localPosition = new Vector3 (-xStart+a, modes[i].transform.localPosition.y, modes[i].transform.localPosition.z);
		}
		Debug.Log ("Modes Organized!");
	}

	void OnDrawGizmosSelected()
    {
		
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