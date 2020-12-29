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

[CustomEditor(typeof(LanguageControl))]
public class LanguageControlEditor : Editor
{

    private LanguageControl languageControl;
    private SerializedObject script;

    // Objects Settings
    public SerializedProperty main_titles;
    ReorderableList main_titles2;

    public SerializedProperty buttons_options;
    ReorderableList buttons_options2;

    public SerializedProperty options_video;
    ReorderableList options_video2;

    public SerializedProperty options_audio;
    ReorderableList options_audio2;

    public SerializedProperty options_game;
    ReorderableList options_game2;

    public void OnEnable()
    {
        languageControl = (LanguageControl)target;
        script = new SerializedObject(target);

        main_titles = script.FindProperty("main_titles");
        buttons_options = script.FindProperty("buttons_options");
        options_video = script.FindProperty("options_video");
        options_audio = script.FindProperty("options_audio");
        options_game = script.FindProperty("options_game");

        this.main_titles2 = new ReorderableList(script, main_titles);
        this.buttons_options2 = new ReorderableList(script, buttons_options);
        this.options_video2 = new ReorderableList(script, options_video);
        this.options_audio2 = new ReorderableList(script, options_audio);
        this.options_game2 = new ReorderableList(script, options_game);

        this.main_titles2.drawElementCallback = RectMainTitles;
        this.main_titles2.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Main Titles");
        };

        this.buttons_options2.drawElementCallback = RectButtonOptions;
        this.buttons_options2.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Button Options");
        };

        this.options_video2.drawElementCallback = RectVideoOptions;
        this.options_video2.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Panel Video");
        };

        this.options_audio2.drawElementCallback = RectAudioOptions;
        this.options_audio2.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Panel Audio");
        };

        this.options_game2.drawElementCallback = RectGameOptions;
        this.options_game2.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Panel Game");
        };
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector ();

        script.Update();
        EditorGUI.BeginChangeCheck();

        //EditorGUILayout.LabelField ("This package is made by Wilgner's Studio");
        languageControl.currentTab = GUILayout.Toolbar(languageControl.currentTab, new string[] { "Main Titles", "Button Options", "Options Video", "Options Audio" });
        languageControl.currentTabTwo = GUILayout.Toolbar(languageControl.currentTabTwo, new string[] { "Options Game" });

        if (languageControl.currentTab != -1)
        {
            if (languageControl.currentTab == 0)
            {
                MainTitlesGUI();
                FocusFix(true);
            }
            else if (languageControl.currentTab == 1)
            {
                ButtonsOptionsGUI();
                FocusFix(true);
            }
            else if (languageControl.currentTab == 2)
            {
                OptionsVideoGUI();
                FocusFix(true);
            }
            else if (languageControl.currentTab == 3)
            {
                OptionsAudioGUI();
                FocusFix(true);
            }
        }
        if (languageControl.currentTabTwo != -1)
        {
            if (languageControl.currentTabTwo == 0)
            {
                OptionsGameGUI();
                FocusFix(false);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            script.ApplyModifiedProperties();
            //GUI.FocusControl (null);
        }
    }

    public void FocusFix(bool firstRow)
    {
        if (firstRow)
        {
            if (languageControl.currentTab != languageControl.previousTab)
            {
                languageControl.currentTabTwo = -1;
                languageControl.previousTabTwo = -1;
                GUI.FocusControl(null);
                languageControl.previousTab = languageControl.currentTab;
            }
        }
        else
        {
            if (languageControl.currentTabTwo != languageControl.previousTabTwo)
            {
                languageControl.currentTab = -1;
                languageControl.previousTab = -1;
                GUI.FocusControl(null);
                languageControl.previousTabTwo = languageControl.currentTabTwo;
            }
        }
    }

    public void MainTitlesGUI()
    {
        main_titles2.DoLayoutList();
    }

    public void ButtonsOptionsGUI()
    {
        buttons_options2.DoLayoutList();
    }

    public void OptionsVideoGUI()
    {
        options_video2.DoLayoutList();
    }

    public void OptionsAudioGUI()
    {
        options_audio2.DoLayoutList();
    }

    public void OptionsGameGUI()
    {
        options_game2.DoLayoutList();
    }

    private void RectMainTitles(Rect rect, int index, bool active, bool focus)
    {
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, 16), main_titles.GetArrayElementAtIndex(index));
    }

    private void RectButtonOptions(Rect rect, int index, bool active, bool focus)
    {
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, 16), buttons_options.GetArrayElementAtIndex(index));
    }

    private void RectVideoOptions(Rect rect, int index, bool active, bool focus)
    {
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, 16), options_video.GetArrayElementAtIndex(index));
    }

    private void RectAudioOptions(Rect rect, int index, bool active, bool focus)
    {
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, 16), options_audio.GetArrayElementAtIndex(index));
    }

    private void RectGameOptions(Rect rect, int index, bool active, bool focus)
    {
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, 16), options_game.GetArrayElementAtIndex(index));
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