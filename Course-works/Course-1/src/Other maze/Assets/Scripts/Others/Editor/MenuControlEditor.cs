using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

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

[CustomEditor(typeof(MenuControl))]
public class MenuControlEditor : Editor
{

	private MenuControl menuControl;
	private SerializedObject script;

	// Objects Settings
	public SerializedProperty mask;
	public SerializedProperty mainScreen;
	public SerializedProperty creditsScreen;
	public SerializedProperty modesScreen;
	public SerializedProperty levelScreen;
	public SerializedProperty optionsScreen;
	public SerializedProperty creditsTextBack;
    public SerializedProperty Canvas;

    // Global Menu Settings
    public SerializedProperty menu;
	public SerializedProperty useCustomCursor;
	public SerializedProperty useKeyboard;
    public SerializedProperty inGame;
    public SerializedProperty isActiveCanvas;
    public SerializedProperty language;
	public SerializedProperty cursor;
	public SerializedProperty spaceMenu;
	public SerializedProperty xSpace;
	public SerializedProperty yAdjust;
	ReorderableList menu2;

	// Play Settings
	//public SerializedProperty noModes;
	public SerializedProperty playsType;
	public SerializedProperty resetAnim;
	public SerializedProperty nameLoadLevel;
	public SerializedProperty spaceModes;
	public SerializedProperty xStart;
	public SerializedProperty modes;
	ReorderableList modes2;

	// Credits Settings
	//public SerializedProperty animCreditsScale;
	//public SerializedProperty animDirectionCredits;
	public SerializedProperty animationType;
	public SerializedProperty YCreditsEnd;
	public SerializedProperty dirCredits;
	public SerializedProperty initScale;
	public SerializedProperty maxScale;
	public SerializedProperty speedAnimScaleCredits;
	public SerializedProperty speedAnimDirCredits;

	// Audio Settings
	public SerializedProperty canPlayBackgroundSong;
	public SerializedProperty backgroundAudio;
	public SerializedProperty audio;
	ReorderableList audio2;

	// Custom Input
	public SerializedProperty forwardDefaultKey;
	public SerializedProperty backDefaultKey;
	public SerializedProperty leftDefaultKey;
	public SerializedProperty rightDefaultKey;
	public SerializedProperty crouchDefaultKey;
	public SerializedProperty jumpDefaultKey;

	public void OnEnable()
    {
		menuControl = (MenuControl)target;
		script = new SerializedObject (target);

		// Objects Settings
		mask = script.FindProperty ("mask");
		mainScreen = script.FindProperty ("mainScreen");
		creditsScreen = script.FindProperty ("creditsScreen");
		modesScreen = script.FindProperty ("modesScreen");
		levelScreen = script.FindProperty ("levelScreen");
		optionsScreen = script.FindProperty ("optionsScreen");
		creditsTextBack = script.FindProperty ("creditsTextBack");
        Canvas = script.FindProperty ("Canvas");

        // Global Menu Settings
        useCustomCursor = script.FindProperty ("useCustomCursor");
		useKeyboard = script.FindProperty ("useKeyboard");
        inGame = script.FindProperty("inGame");
        language = script.FindProperty ("language");
		cursor = script.FindProperty ("cursor");
		menu = script.FindProperty ("menu");
		spaceMenu = script.FindProperty ("spaceMenu");
		xSpace = script.FindProperty ("xSpace");
		yAdjust = script.FindProperty ("yAdjust");
		this.menu2 = new ReorderableList (script, menu);
        isActiveCanvas = script.FindProperty("isActiveCanvas");

        // Play Settings
        //noModes = script.FindProperty ("noModes");
        playsType = script.FindProperty ("playsType");
		resetAnim = script.FindProperty ("resetAnim");
		nameLoadLevel = script.FindProperty ("nameLoadLevel");
		spaceModes = script.FindProperty ("spaceModes");
		xStart = script.FindProperty ("xStart");
		modes = script.FindProperty ("modes");
		this.modes2 = new ReorderableList (script, modes);

        // Credits Settings
        //animCreditsScale = script.FindProperty ("animCreditsScale");
        //animDirectionCredits = script.FindProperty ("animDirectionCredits");
        animationType = script.FindProperty ("animationType");
		YCreditsEnd = script.FindProperty ("YCreditsEnd");
		dirCredits = script.FindProperty ("dirCredits");
		initScale = script.FindProperty ("initScale");
		maxScale = script.FindProperty ("maxScale");
		speedAnimScaleCredits = script.FindProperty ("speedAnimScaleCredits");
		speedAnimDirCredits = script.FindProperty ("speedAnimDirCredits");

		// Audio Settings
		canPlayBackgroundSong = script.FindProperty ("canPlayBackgroundSong");
		backgroundAudio = script.FindProperty ("backgroundAudio");
		audio = script.FindProperty ("audio");
		this.audio2 = new ReorderableList (script, audio);

		// Custom Input
		forwardDefaultKey = script.FindProperty ("forwardDefaultKey");
		backDefaultKey = script.FindProperty ("backDefaultKey");
		leftDefaultKey = script.FindProperty ("leftDefaultKey");
		rightDefaultKey = script.FindProperty ("rightDefaultKey");
		crouchDefaultKey = script.FindProperty ("crouchDefaultKey");
		jumpDefaultKey = script.FindProperty ("jumpDefaultKey");

		this.menu2.drawElementCallback = RectNewMenu;
		this.menu2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Menus");
		};

		this.modes2.drawElementCallback = RectNewModes;
		this.modes2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Modes");
		};

		this.audio2.drawElementCallback = RectNewAudio;
		this.audio2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Audios");
		};
	}

	public override void OnInspectorGUI()
    {
		//DrawDefaultInspector ();

		script.Update ();
		EditorGUI.BeginChangeCheck ();
		//EditorGUILayout.LabelField 
		menuControl.currentTab = GUILayout.Toolbar(menuControl.currentTab, new string[] {"Objects Settings", "Menu Settings", "Play Settings", "Credits Settings"});
		menuControl.currentTabTwo = GUILayout.Toolbar(menuControl.currentTabTwo, new string[] {"Audio Manager", "Custom Input"});

		if (menuControl.currentTab != -1)
        {
			if (menuControl.currentTab == 0)
            {
				ObjectSettingsGUI ();
				FocusFix (true);
			}
            else if (menuControl.currentTab == 1)
            {
				MenuSettingsGUI ();
				FocusFix (true);
				if (GUILayout.Button ("Organize Menus"))
                {
					GUI.FocusControl (null);
					menuControl.OrganizeMenus ();
				}
			}
            else if (menuControl.currentTab == 2)
            {
				PlaySettingsGUI ();
				FocusFix (true);
				if (GUILayout.Button ("Organize Modes"))
                {
					GUI.FocusControl (null);
					menuControl.OrganizeModes ();
				}
			}
            else if (menuControl.currentTab == 3)
            {
				CreditsSettingsGUI ();
				FocusFix (true);
			}
            else if (menuControl.currentTab == 4)
            {
				AudioSettingsGUI ();
				FocusFix (true);
			}
		}
		if (menuControl.currentTabTwo != -1)
        {	
			if (menuControl.currentTabTwo == 0)
            {
				AudioSettingsGUI ();
				FocusFix (false);
			}
            else if (menuControl.currentTabTwo == 1)
            {
				CustomInputGUI ();
				FocusFix (false);
			}
		}

		if (GUILayout.Button ("Set Default Values"))
        {
			GUI.FocusControl (null);
			menuControl.SetDefaultValues ();
		}

		if (EditorGUI.EndChangeCheck ())
        {
			script.ApplyModifiedProperties ();
			//GUI.FocusControl (null);
		}

	}

	public void FocusFix(bool firstRow)
    {
		if (firstRow)
        {
			if (menuControl.currentTab != menuControl.previousTab)
            {
				menuControl.currentTabTwo = -1;
				menuControl.previousTabTwo = -1;
				GUI.FocusControl (null);
				menuControl.previousTab = menuControl.currentTab;
			}
		}
        else
        {
			if (menuControl.currentTabTwo != menuControl.previousTabTwo)
            {
				menuControl.currentTab = -1;
				menuControl.previousTab = -1;
				GUI.FocusControl (null);
				menuControl.previousTabTwo = menuControl.currentTabTwo;
			}
		}
	}

	public void ObjectSettingsGUI()
    {
		EditorGUILayout.PropertyField (mask);
		EditorGUILayout.PropertyField (mainScreen);
		EditorGUILayout.PropertyField (creditsScreen);
		EditorGUILayout.PropertyField (modesScreen);
		EditorGUILayout.PropertyField (levelScreen);
		EditorGUILayout.PropertyField (optionsScreen);
		EditorGUILayout.PropertyField (creditsTextBack);
        EditorGUILayout.PropertyField (Canvas);
    }

	public void MenuSettingsGUI()
    {
		EditorGUILayout.PropertyField (useCustomCursor);
		EditorGUILayout.PropertyField (useKeyboard);
        EditorGUILayout.PropertyField (inGame);
        EditorGUILayout.PropertyField(isActiveCanvas);
        EditorGUILayout.PropertyField (language);
		EditorGUILayout.PropertyField (cursor);
		menu2.DoLayoutList ();
		EditorGUILayout.PropertyField (spaceMenu);
		EditorGUILayout.PropertyField (xSpace);
		EditorGUILayout.PropertyField (yAdjust);
	}

	public void PlaySettingsGUI()
    {
		//EditorGUILayout.PropertyField (noModes);
		EditorGUILayout.PropertyField (playsType);
		EditorGUILayout.PropertyField (resetAnim);
		EditorGUILayout.PropertyField (nameLoadLevel);
		modes2.DoLayoutList ();
		EditorGUILayout.PropertyField (spaceModes);
		EditorGUILayout.PropertyField (xStart);
	}

	public void CreditsSettingsGUI()
    {
		//EditorGUILayout.PropertyField (animCreditsScale);
		//EditorGUILayout.PropertyField (animDirectionCredits);
		EditorGUILayout.PropertyField (animationType);
		EditorGUILayout.PropertyField (YCreditsEnd);
		EditorGUILayout.PropertyField (dirCredits);
		EditorGUILayout.PropertyField (initScale);
		EditorGUILayout.PropertyField (maxScale);
		EditorGUILayout.PropertyField (speedAnimScaleCredits);
		EditorGUILayout.PropertyField (speedAnimDirCredits);
	}

	public void AudioSettingsGUI()
    {
		EditorGUILayout.PropertyField (canPlayBackgroundSong);
		EditorGUILayout.PropertyField (backgroundAudio);
		audio2.DoLayoutList ();
	}

	public void CustomInputGUI()
    {
		EditorGUILayout.PropertyField (forwardDefaultKey);
		EditorGUILayout.PropertyField (backDefaultKey);
		EditorGUILayout.PropertyField (leftDefaultKey);
		EditorGUILayout.PropertyField (rightDefaultKey);
		EditorGUILayout.PropertyField (crouchDefaultKey);
		EditorGUILayout.PropertyField (jumpDefaultKey);
	}


    
    [MenuItem("GameObject/Wilgner's Studio/Add/Main Menu", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/Main Menu %#m")]
	private static void CreateCompleteMenu()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/MenuKit_Prefab.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }

    [MenuItem("GameObject/Wilgner's Studio/Add/Pause Menu", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/Pause Menu %#p")]
    private static void PauseMenu()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/MenuKit_InGame.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }

    [MenuItem("GameObject/Wilgner's Studio/Add/New Menu", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/New Menu %#n")]
    private static void NewMenu()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/Screens/menuNew.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }

    [MenuItem("GameObject/Wilgner's Studio/Add/Credits Screen", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/Credits Screen %#c")]
    private static void CreditsScreenMenu()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/Screens/CreditsScreen.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }

    [MenuItem("GameObject/Wilgner's Studio/Add/Dropdown Example", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/Dropdown Example %#d")]
    private static void DropdownExample()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/Screens/Row_DropdownExample.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }

    [MenuItem("GameObject/Wilgner's Studio/Add/Slider Example", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/Slider Example %#s")]
    private static void SliderExample()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/Screens/Row_SliderMK.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }

    [MenuItem("GameObject/Wilgner's Studio/Add/Toggle Example", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/Toggle Example %#t")]
    private static void ToggleExample()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/Screens/Row_ToggleExample.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }

    [MenuItem("GameObject/Wilgner's Studio/Add/Custom Input Example", false, 0)]
    [MenuItem("Wilgner's Studio/MenuKit/Add/Custom Input Example %#i")]
    private static void CustomInputExample()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WS_MenuKit/Prefabs/Screens/Row_CustomInput.prefab", typeof(GameObject));
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }


    private void RectNewMenu(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), menu.GetArrayElementAtIndex (index));
	}

	private void RectNewModes(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), modes.GetArrayElementAtIndex (index));
	}

	private void RectNewAudio(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), audio.GetArrayElementAtIndex (index));
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