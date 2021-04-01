using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    /* Скрипт для смены языка
     * Изменение языка в меню
     * 
     * Скрипт не правильно работает
     * 
     * Убрал
     */

public class LanguageControl : MonoBehaviour
{

	// [Header("JSON File Names")]
	// public string mainScreenLanguage = "MainScreenLanguage"; 
	// public string optionsScreenLanguage = "OptionsScreenLanguages";

	public Text[] main_titles;
	public Text[] buttons_options;
	public Text[] options_video;
	public Text[] options_audio;
	public Text[] options_game;

	public WS_MAIN_LANGUAGE ws_main_language;
	public WS_OPTIONS_LANGUAGE ws_options_language;

	private MenuControl _menuControl;
	private OptionsControl _optionsControl;

    //Editor
    [HideInInspector]
    public int currentTab;
    [HideInInspector]
    public int currentTabTwo;
    [HideInInspector]
    public int previousTab = -1;
    [HideInInspector]
    public int previousTabTwo = -1;
    
    void Start ()
    {
		ws_main_language = new WS_MAIN_LANGUAGE ();
		ws_options_language = new WS_OPTIONS_LANGUAGE ();
		_menuControl = this.GetComponent<MenuControl> ();
		_optionsControl = this.GetComponent<OptionsControl> ();

	}
	
	void Update ()
    {

	}
   	
	public void SetLanguageInGame()
    {
		SetMainLanguage ();
		SetOptionsLanguage ();
	}

	// MAIN MENU
	public void SetMainLanguage()
    {
		if (_optionsControl != null && _optionsControl._gameConfig != null && ws_main_language != null)
        {
			if (_optionsControl._gameConfig.language == 0)
            { // ENGLISH
                //Debug.Log ("Change To English!");
                if (_menuControl.inGame == false)
                {
                    main_titles[0].text = ws_main_language.play_en;
                    main_titles[1].text = ws_main_language.options_en;
                    _menuControl.menu[0].SetText(ws_main_language.continue_en);
                    _menuControl.menu[1].SetText(ws_main_language.play_en);
                    _menuControl.menu[2].SetText(ws_main_language.options_en);
                    _menuControl.menu[3].SetText(ws_main_language.credits_en);
                    _menuControl.menu[4].SetText(ws_main_language.exit_en);

                }
                else
                {
                    _menuControl.menu[0].SetText(ws_main_language.resume_en);
                    _menuControl.menu[1].SetText(ws_main_language.options_en);
                    _menuControl.menu[2].SetText(ws_main_language.exit_en);
                }
			}
            else if (_optionsControl._gameConfig.language == 1)
            { // RUS
                //Debug.Log ("Change To PTBR");
                if (_menuControl.inGame == false) // not Pause Menu
                {
                    main_titles[0].text = ws_main_language.play_rus;
                    main_titles[1].text = ws_main_language.options_rus;
                    _menuControl.menu[0].SetText(ws_main_language.continue_rus);
                    _menuControl.menu[1].SetText(ws_main_language.play_rus);
                    _menuControl.menu[2].SetText(ws_main_language.options_rus);
                    _menuControl.menu[3].SetText(ws_main_language.credits_rus);
                    _menuControl.menu[4].SetText(ws_main_language.exit_rus);

                }
                else // In Pause Menu
                {
                    _menuControl.menu[0].SetText(ws_main_language.resume_rus);
                    _menuControl.menu[1].SetText(ws_main_language.options_rus);
                    _menuControl.menu[2].SetText(ws_main_language.exit_rus);
                }

            }
		}
	}

	// OPTIONS LANGUAGE
	public void SetOptionsLanguage()
    {
		if (_optionsControl != null && _optionsControl._gameConfig != null && ws_main_language != null)
        {
			if (_optionsControl._gameConfig.language == 0)
            { // ENGLISH
				// BUTTONS
				buttons_options[0].text = ws_options_language.video_en;
				buttons_options[1].text = ws_options_language.audio_en;
				buttons_options[2].text = ws_options_language.game_en;
				buttons_options[3].text = ws_options_language.apply_en;
				buttons_options[4].text = ws_options_language.return_en;

				// VIDEO
				options_video[0].text = ws_options_language.displayMode_en;
				options_video[1].text = ws_options_language.targetDisplay_en;
				options_video[2].text = ws_options_language.resolution_en;
				options_video[3].text = ws_options_language.graphicsQuality_en;
				options_video[4].text = ws_options_language.antialiasing_en;
				options_video[5].text = ws_options_language.vsync_en;


				// AUDIO
				options_audio [0].text = ws_options_language.masterVolume_en;
				options_audio [1].text = ws_options_language.musicVolume_en;
				options_audio [2].text = ws_options_language.effectsVolume_en;
				options_audio [3].text = ws_options_language.voiceVolume_en;
				options_audio [4].text = ws_options_language.micVolume_en;
				options_audio [5].text = ws_options_language.soundBackground_en;

				// GAME
				options_game [0].text = ws_options_language.horizontalSensitivy_en;
				options_game [1].text = ws_options_language.vericalSensitivy_en;
				options_game [2].text = ws_options_language.difficulty_en;
				options_game [3].text = ws_options_language.language_en;
				options_game [4].text = ws_options_language.forward_en;
				options_game [5].text = ws_options_language.back_en;
				options_game [6].text = ws_options_language.left_en;
				options_game [7].text = ws_options_language.right_en;
				options_game [8].text = ws_options_language.crouch_en;
				options_game [9].text = ws_options_language.jump_en;
				options_game [10].text = ws_options_language.tips_en;

			}
            else if (_optionsControl._gameConfig.language == 1)
            { // RUS
				// BUTTONS
				buttons_options[0].text = ws_options_language.video_rus;
				buttons_options[1].text = ws_options_language.audio_rus;
				buttons_options[2].text = ws_options_language.game_rus;
				buttons_options[3].text = ws_options_language.apply_rus;
				buttons_options[4].text = ws_options_language.return_rus;

				// VIDEO
				options_video[0].text = ws_options_language.displayMode_rus;
				options_video[1].text = ws_options_language.targetDisplay_rus;
				options_video[2].text = ws_options_language.resolution_rus;
				options_video[3].text = ws_options_language.graphicsQuality_rus;
				options_video[4].text = ws_options_language.antialiasing_rus;
				options_video[5].text = ws_options_language.vsync_rus;

				// AUDIO
				options_audio [0].text = ws_options_language.masterVolume_rus;
				options_audio [1].text = ws_options_language.musicVolume_rus;
				options_audio [2].text = ws_options_language.effectsVolume_rus;
				options_audio [3].text = ws_options_language.voiceVolume_rus;
				options_audio [4].text = ws_options_language.micVolume_rus;
				options_audio [5].text = ws_options_language.soundBackground_rus;

				// GAME
				options_game [0].text = ws_options_language.horizontalSensitivy_rus;
				options_game [1].text = ws_options_language.vericalSensitivy_rus;
				options_game [2].text = ws_options_language.difficulty_rus;
				options_game [3].text = ws_options_language.language_rus;
				options_game [4].text = ws_options_language.forward_rus;
				options_game [5].text = ws_options_language.back_rus;
				options_game [6].text = ws_options_language.left_rus;
				options_game [7].text = ws_options_language.right_rus;
				options_game [8].text = ws_options_language.crouch_rus;
				options_game [9].text = ws_options_language.jump_rus;
				options_game [10].text = ws_options_language.tips_rus;
			}
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