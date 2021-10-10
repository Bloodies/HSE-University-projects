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

[CustomEditor(typeof(OptionsControl))]
public class OptionsControlEditor : Editor
{

	private OptionsControl optionsControl;
	private SerializedObject script;

	// BASIC SETTINGS
	public SerializedProperty canvasScaler;
	public SerializedProperty panelsOptions;		
	public SerializedProperty buttons;				
	public SerializedProperty colorNormal;
	public SerializedProperty colorSelected;
	//public SerializedProperty customInput;
    public SerializedProperty InitSettings;
    ReorderableList panelsOptions2;
	ReorderableList buttons2;

	// AMOUNTS TEXT LIST
	public SerializedProperty amounts; 		
	ReorderableList amounts2;

	// SLIDERS TEXT LIST
	public SerializedProperty sliders; 		
	ReorderableList sliders2;

	// DROPDOWNS TEXT LIST
	public SerializedProperty dropdowns; 	
	ReorderableList dropdowns2;

	// TOGGLES TEXT LIST
	public SerializedProperty toggles; 		
	ReorderableList toggles2;

	// RESOLUTIONS TEXT LIST
	public SerializedProperty resolutions; 
	ReorderableList resolutions2;

	public void OnEnable()
    {
		optionsControl = (OptionsControl)target;
		script = new SerializedObject (target);

		// Objects Settings
		canvasScaler = script.FindProperty ("canvasScaler");
		panelsOptions = script.FindProperty ("panelsOptions");
		buttons = script.FindProperty ("buttons");
		colorNormal = script.FindProperty ("colorNormal");
		colorSelected = script.FindProperty ("colorSelected");
		//customInput = script.FindProperty ("customInput");
        InitSettings = script.FindProperty("InitSettings");

        this.panelsOptions2 = new ReorderableList (script, panelsOptions);
		this.buttons2 = new ReorderableList (script, buttons);


        this.panelsOptions2.drawElementCallback = RectNewOptions;
		this.panelsOptions2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Panels Options");
		};

		this.buttons2.drawElementCallback = RectNewButton;
		this.buttons2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Buttons");
		};

		// Amounts
		amounts = script.FindProperty ("amounts");
		this.amounts2 = new ReorderableList (script, amounts);
		this.amounts2.drawElementCallback = RectNewAmounts;
		this.amounts2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Amounts Text");
		};

		// Sliders
		sliders = script.FindProperty ("sliders");
		this.sliders2 = new ReorderableList (script, sliders);
		this.sliders2.drawElementCallback = RectNewSliders;
		this.sliders2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Sliders");
		};

		// Dropdowns
		dropdowns = script.FindProperty ("dropdowns");
		this.dropdowns2 = new ReorderableList (script, dropdowns);
		this.dropdowns2.drawElementCallback = RectNewDropdowns;
		this.dropdowns2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Dropdowns");
		};

		// Toggles
		toggles = script.FindProperty ("toggles");
		this.toggles2 = new ReorderableList (script, toggles);
		this.toggles2.drawElementCallback = RectNewToggles;
		this.toggles2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Toggles");
		};

		// Resolutions
		resolutions = script.FindProperty ("resolutions");
		this.resolutions2 = new ReorderableList (script, resolutions);
		this.resolutions2.drawElementCallback = RectNewResolutions;
		this.resolutions2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Resolutions");
		};

	}

	public override void OnInspectorGUI()
    {
		//DrawDefaultInspector ();

		script.Update ();
		EditorGUI.BeginChangeCheck ();
		//EditorGUILayout.LabelField ("This package is made by Wilgner's Studio");
		optionsControl.currentTab = GUILayout.Toolbar (optionsControl.currentTab, new string[] 
        {
			"Basic Settings",
			"Amounts Text",
			"Sliders",
			"Dropdowns"
		});
		optionsControl.currentTabTwo = GUILayout.Toolbar (optionsControl.currentTabTwo, new string[]
        {
			"Toggles",
			"Resolutions"
		});

		if (optionsControl.currentTab != -1)
        {
			if (optionsControl.currentTab == 0)
            {
				BasicSettingsGUI ();
				FocusFix (true);
			}
            else if (optionsControl.currentTab == 1)
            {
				AmountsTextGUI ();
				FocusFix (true);
			}
            else if (optionsControl.currentTab == 2)
            {
				SlidersTextGUI ();
				FocusFix (true);
			}
            else if (optionsControl.currentTab == 3)
            {
				DropdownsGUI ();
				FocusFix (true);
			}
		}

		if (optionsControl.currentTabTwo != -1)
        {	
			if (optionsControl.currentTabTwo == 0)
            {
				TogglesGUI ();
				FocusFix (false);
			}
            else if (optionsControl.currentTabTwo == 1)
            {
				ResolutionsGUI ();
				FocusFix (false);
			}
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
			if (optionsControl.currentTab != optionsControl.previousTab)
            {
				optionsControl.currentTabTwo = -1;
				optionsControl.previousTabTwo = -1;
				GUI.FocusControl (null);
				optionsControl.previousTab = optionsControl.currentTab;
			}
		}
        else
        {
			if (optionsControl.currentTabTwo != optionsControl.previousTabTwo)
            {
				optionsControl.currentTab = -1;
				optionsControl.previousTab = -1;
				GUI.FocusControl (null);
				optionsControl.previousTabTwo = optionsControl.currentTabTwo;
			}
		}
	}

	void BasicSettingsGUI()
    {
		EditorGUILayout.PropertyField (canvasScaler);
		panelsOptions2.DoLayoutList ();
		buttons2.DoLayoutList ();
		EditorGUILayout.PropertyField (colorNormal);
		EditorGUILayout.PropertyField (colorSelected);
		//EditorGUILayout.PropertyField (customInput);
        EditorGUILayout.PropertyField(InitSettings);
	}

	void AmountsTextGUI()
    {
		amounts2.DoLayoutList ();
	}

	void SlidersTextGUI()
    {
		sliders2.DoLayoutList ();
	}

	void DropdownsGUI()
    {
		dropdowns2.DoLayoutList ();
	}

	void TogglesGUI()
    {
		toggles2.DoLayoutList ();
	}

	void ResolutionsGUI()
    {
		resolutions2.DoLayoutList ();
	}

	private void RectNewOptions(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), panelsOptions.GetArrayElementAtIndex (index));
	}

	private void RectNewButton(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), buttons.GetArrayElementAtIndex (index));
	}

	private void RectNewAmounts(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), amounts.GetArrayElementAtIndex (index));
	}

	private void RectNewSliders(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), sliders.GetArrayElementAtIndex (index));
	}

	private void RectNewDropdowns(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), dropdowns.GetArrayElementAtIndex (index));
	}

	private void RectNewToggles(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), toggles.GetArrayElementAtIndex (index));
	}

	private void RectNewResolutions(Rect rect, int index, bool active, bool focus)
    {
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), resolutions.GetArrayElementAtIndex (index));
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