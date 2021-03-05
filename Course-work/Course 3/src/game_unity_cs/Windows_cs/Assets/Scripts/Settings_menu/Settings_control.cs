using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Settings_controller : MonoBehaviour
{

	[Header("Basic Settings")]
	public CanvasScaler canvasScaler;					// Get CanvasScaler component
	public GameObject[] panelsOptions;					// All options panels(example: Video, Audio and Game)
	[Space]
	public GameObject[] buttons;						// All buttons of menu 
	public Color colorNormal, colorSelected;			// Color(Default ou Selected) of buttons
														//public Button applyButton;											
														// Apply button for confirm changes in settings
	[Header("Events")]
	[SerializeField]
	private UnityEvent InitSettings = new UnityEvent();

	[Header("Options Settings")]
	public Text[] amounts;								// All amounts of sliders
	public Slider[] sliders;							// All sliders
	public Dropdown[] dropdowns;						// All dropdowns
	public Toggle[] toggles;							// All checkbox
	public Vector2[] resolutions;						// All resolutions available

	private Display[] _displays;						// All displays available
	[HideInInspector] public GameConfig _gameConfig;	// GameConfig responsible for storing the settings
	private int _selected = 0;							// Variables responsible for knowing which menu (Video, Audio and Game) is selected
	private Menu_controller _menuControl;
	private Language_controller _languageControl;

	// EDITOR
	[HideInInspector]
	public int currentTab, currentTabTwo, previousTab = -1, previousTabTwo = -1;
	
	void Start()						// Use this for initialization
	{
		basicSettings();										// Call the method
		loadConfig();											// Call the method
	}
	
	void Update()						// Update is called once per frame
	{
		updateAmountsText();									// Call the method
		changeSettingsGame();
	}

	#region START METHODS OPTIONSCONTROL
	void basicSettings()
	{
		changeSelected(_selected);								// Set Menu Selected
		addListeners();											// Add Listerners

		_gameConfig = new GameConfig();							// Create class GameConfig
		_displays = Display.displays;							// Get all displays available
		_menuControl = FindObjectOfType<Menu_controller>();
		_languageControl = this.GetComponent<Language_controller>();

		addResolutions();
		addDisplays();											// Call the method
	}
	#endregion END METHODS OPTIONSCONTROL

	#region START METHODS ADD DROPDOWN	
	public void addResolutions()		// Adds resolutions in dropdown
	{
		dropdowns[2].ClearOptions();
		foreach (Vector2 resolution in resolutions)
		{	// Walks by all resolutions in vector2
			// Add resolution in dropdown and transform to string
			dropdowns[2].options.Add(new Dropdown.OptionData(resolution.x + " x " + resolution.y));     
		}
	}
	
	void addDisplays()					// Adds all displays in dropdown
	{
		for (int i = 0; i < _displays.Length; i++)
		{																				// Walks by all diaplys
			dropdowns[1].options.Add(new Dropdown.OptionData("MONITOR " + (i + 1)));	// Adds the display as "MONITOR" and the identification number
			if (i == 0)
			{																			// If it is the first monitor added
				dropdowns[1].captionText.text = "MONITOR " + (i + 1);					// Makes it selected
			}
		}
	}
	#endregion END METHODS ADD DROPDOWN

	#region START METHODS MENU	
	public void changeSelected(int sel)	// Change menu 
	{
		_selected = sel;													// Put temporary variable in _selected
		foreach (GameObject go in panelsOptions)
		{																	// Walk by all game objects in panelsOptions
			go.SetActive(false);											// Disables
		}

		foreach (GameObject go2 in buttons)
		{																	// Walk by all game objects in buttons
			go2.GetComponent<Image>().color = colorNormal;					// Define to default color(
		}

		panelsOptions[_selected].SetActive(true);							// Activate the panel according to a variable
		buttons[_selected].GetComponent<Image>().color = colorSelected;		// Put colorSelected on the active button
	}
	
	public void updateAmountsText()	// Updates all amount text
	{
		for (int i = 0; i <= amounts.Length - 1; i++)
		{																	// Walks up to the length of the vector amounts
			amounts[i].text = "" + sliders[i].value.ToString("F2");			// Sets the text of the amounts as the value of the slider and I define F2 formatting (example: 0.50)
		}
	}
	
	public void addListeners()			// Adds listeners (You can do it manually)
	{
		//applyButton.onClick.AddListener(delegate {changeButtonApply();});		
		// Add listener by clicking the button
	}
	
	public void changeButtonApply()		// By clicking the apply button
	{
		saveGameConfig();													// Call the method
		saveConfig();														// Call the method
		changeSettingsGame();												// Call the method
	}
	#endregion END METHODS MENU

	#region START METHODS JSON	
	public void saveConfig()			// Saves settings to JSON file
	{
		string jsonData = JsonUtility.ToJson(_gameConfig, true);							// Converts the class to the json file
		File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);	// Save the jsonData (class _gameConfig) in persistenceData
	}
	
	public void loadConfig()			// Load settings JSON file
	{
		// Load the json file in the path where it is located and store it in the variable _gameConfig
		if (File.Exists(Application.persistentDataPath + "/gamesettings.json"))
		{
			_gameConfig = JsonUtility.FromJson<GameConfig>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
		}
		else
		{
			Debug.LogWarning("MENU_KIT: gamesettings.json does not exists!");
		}

		setValues();					// Call the method
	}
	
	public void saveGameConfig()		// Save all values in _gameConfig(Class GameConfig)
	{
		// GRAPHICS
		_gameConfig.displayMode = dropdowns[0].value;
		_gameConfig.targetDisplay = dropdowns[1].value;
		_gameConfig.resulationId = dropdowns[2].value;
		_gameConfig.graphicsQuality = dropdowns[3].value;
		_gameConfig.antialiasing = dropdowns[4].value;
		_gameConfig.vsync = dropdowns[5].value;

		// AUDIO
		_gameConfig.masterVolume = sliders[0].value;
		_gameConfig.musicVolume = sliders[1].value;
		_gameConfig.effectsVolume = sliders[2].value;
		_gameConfig.voiceVolume = sliders[3].value;
		_gameConfig.micVolume = sliders[4].value;
		_gameConfig.soundBackground = toggles[0].isOn;

		// GAME
		_gameConfig.horizontalSensitivy = sliders[5].value;
		_gameConfig.verticalSensitivy = sliders[6].value;
		_gameConfig.difficuly = dropdowns[6].value;
		_gameConfig.language = dropdowns[7].value;
		_gameConfig.tips = toggles[1].isOn;

		_gameConfig.forward = _menuControl.forwardDefaultKey;
		_gameConfig.back = _menuControl.backDefaultKey;
		_gameConfig.left = _menuControl.leftDefaultKey;
		_gameConfig.right = _menuControl.rightDefaultKey;
		_gameConfig.crouch = _menuControl.crouchDefaultKey;
		_gameConfig.jump = _menuControl.jumpDefaultKey;
	}
	
	public void setValues()				// Sets the values according to the variable _gameConfig(class GameConfig)
	{

		// GRAPHICS
		dropdowns[0].value = _gameConfig.displayMode;
		dropdowns[1].value = _gameConfig.targetDisplay;

		string x = resolutions[_gameConfig.resulationId].x.ToString();
		string y = resolutions[_gameConfig.resulationId].y.ToString();
		dropdowns[2].value = _gameConfig.resulationId;
		dropdowns[2].captionText.text = x + " x " + y;

		dropdowns[3].value = _gameConfig.graphicsQuality;
		dropdowns[4].value = _gameConfig.antialiasing;
		dropdowns[5].value = _gameConfig.vsync;

		// AUDIO
		sliders[0].value = _gameConfig.masterVolume;
		sliders[1].value = _gameConfig.musicVolume;
		sliders[2].value = _gameConfig.effectsVolume;
		sliders[3].value = _gameConfig.voiceVolume;
		sliders[4].value = _gameConfig.micVolume;
		toggles[0].isOn = _gameConfig.soundBackground;

		// GAME
		sliders[5].value = _gameConfig.horizontalSensitivy;
		sliders[6].value = _gameConfig.verticalSensitivy;
		dropdowns[6].value = _gameConfig.difficuly;
		dropdowns[7].value = _gameConfig.language;
		toggles[1].isOn = _gameConfig.tips;

		_menuControl.forwardDefaultKey = _gameConfig.forward;
		_menuControl.backDefaultKey = _gameConfig.back;
		_menuControl.leftDefaultKey = _gameConfig.left;
		_menuControl.rightDefaultKey = _gameConfig.right;
		_menuControl.crouchDefaultKey = _gameConfig.crouch;
		_menuControl.jumpDefaultKey = _gameConfig.jump;
		Debug.Log("SET VALUES!");

	}
	#endregion END METHODS JSON

	#region START METHODS SET CONFIG IN GAME
	public void changeSettingsGame()
	{
		InitSettings.Invoke();
		/*
        if(_gameConfig.soundBackground == false)
        {
            _menuControl.backgroundAudio.Stop ();
        }
        else
        {
            if (!_menuControl.backgroundAudio.isPlaying)
            {
                _menuControl.backgroundAudio.Play();
            }
        }
        */
	}
	
	public void changeDisplayMode()		// Changes screen mode in game
	{
		if (dropdowns[0].value == 0)
		{
			Screen.fullScreen = true;
			//Debug.Log ("Screen.fullScreen: true");
		}
		else if (dropdowns[0].value == 1)
		{
			Screen.fullScreen = false;
			//Debug.Log ("Screen.fullScreen: false");
		}
		//Debug.Log (Screen.fullScreen);
		//Debug.Log ("Dropdown[0] Value: "+dropdowns[0].value);
	}
	
	public void changeTargetDisplay()	// Change the target display in the game
	{
		if (Camera.current != null)
		{
			Camera.current.targetDisplay = _gameConfig.targetDisplay;
		}
	}
	
	public void changeResolution()		// Changes resolution in game
	{
		// Sets the resolution according to the resolutions variable and the index defined in the dropdown
		Screen.SetResolution((int)resolutions[_gameConfig.resulationId].x, (int)resolutions[dropdowns[2].value].y, Screen.fullScreen);
		// Update Canvas Scaler with the new resolution
		// canvasScaler.referenceResolution = new Vector2 (resolutions[_gameConfig.resulationId].x, resolutions[dropdowns [2].value].y);
	}
	
	public void changeGraphicsQuality()	// Changes game quality in game
	{
		QualitySettings.masterTextureLimit = _gameConfig.graphicsQuality;   // Sets the quality according to _gameConfig (class GameConfig)

	}
	
	public void changeAntiAliasing()	// Change antialiasing in game
	{
		QualitySettings.antiAliasing = _gameConfig.antialiasing;    // Sets the antialiasing according to _gameConfig (class GameConfig)
	}
	
	public void changeVSYNC()			// Change VSYNC in game
	{
		QualitySettings.vSyncCount = _gameConfig.vsync;     // Sets the vsync to _gameConfig (class GameConfig)
	}

	public void ChangeMasterVolume()
	{
		AudioListener.volume = sliders[0].value;
	}

	public void ChangeMusicVolume()
	{
		_menuControl.backgroundFX.volume = sliders[1].value;
		//Debug.Log (_menuControl.backgroundAudio.volume);
		//Debug.Log (sliders[1].name);
	}

	public void ChangeSoundBackground()
	{
		Application.runInBackground = toggles[0].isOn;
	}

	public void ChangeLanguage()
	{
		_languageControl.SetLanguageInGame();
	}
	#endregion END METHODS SET CONFIG IN GAME
}