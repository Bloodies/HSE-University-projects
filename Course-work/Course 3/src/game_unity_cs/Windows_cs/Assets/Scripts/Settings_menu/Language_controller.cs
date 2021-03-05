using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/* Скрипт для смены языка
 * Изменение языка в меню
 * 
 * Скрипт не правильно работает
 * 
 * Убрал
 */

public class Language_controller : MonoBehaviour
{
	// [Header("JSON File Names")]
	// public string mainScreenLanguage = "MainScreenLanguage"; 
	// public string optionsScreenLanguage = "OptionsScreenLanguages";

	public Text[] main_titles;
	public Text[] buttons_options;
	public Text[] options_video;
	public Text[] options_audio;
	public Text[] options_game;

	public Main_menu_lang main_menu_language;
	public Settings_lang settings_language;

	private Menu_controller _menuControl;
	private Settings_controller _optionsControl;

	//Editor
	[HideInInspector]
	public int currentTab;
	[HideInInspector]
	public int currentTabTwo;
	[HideInInspector]
	public int previousTab = -1;
	[HideInInspector]
	public int previousTabTwo = -1;

	void Start()
	{
		main_menu_language = new Main_menu_lang();
		settings_language = new Settings_lang();
		_menuControl = this.GetComponent<Menu_controller>();
		_optionsControl = this.GetComponent<Settings_controller>();

	}

	void Update()
	{

	}

	public void SetLanguageInGame()
	{
		SetMainLanguage();
		SetOptionsLanguage();
	}

	// MAIN MENU
	public void SetMainLanguage()
	{
		if (_optionsControl != null && _optionsControl._gameConfig != null && main_menu_language != null)
		{
			if (_optionsControl._gameConfig.language == 0)
			{ // ENGLISH
			  //Debug.Log ("Change To English!");
				if (_menuControl.inGame == false)
				{
					main_titles[0].text = main_menu_language.play_en;
					main_titles[1].text = main_menu_language.options_en;
					_menuControl.menu[0].SetText(main_menu_language.continue_en);
					_menuControl.menu[1].SetText(main_menu_language.play_en);
					_menuControl.menu[2].SetText(main_menu_language.options_en);
					_menuControl.menu[3].SetText(main_menu_language.credits_en);
					_menuControl.menu[4].SetText(main_menu_language.exit_en);

				}
				else
				{
					_menuControl.menu[0].SetText(main_menu_language.resume_en);
					_menuControl.menu[1].SetText(main_menu_language.options_en);
					_menuControl.menu[2].SetText(main_menu_language.exit_en);
				}
			}
			else if (_optionsControl._gameConfig.language == 1)
			{ // RUS
			  //Debug.Log ("Change To PTBR");
				if (_menuControl.inGame == false) // not Pause Menu
				{
					main_titles[0].text = main_menu_language.play_rus;
					main_titles[1].text = main_menu_language.options_rus;
					_menuControl.menu[0].SetText(main_menu_language.continue_rus);
					_menuControl.menu[1].SetText(main_menu_language.play_rus);
					_menuControl.menu[2].SetText(main_menu_language.options_rus);
					_menuControl.menu[3].SetText(main_menu_language.credits_rus);
					_menuControl.menu[4].SetText(main_menu_language.exit_rus);

				}
				else // In Pause Menu
				{
					_menuControl.menu[0].SetText(main_menu_language.resume_rus);
					_menuControl.menu[1].SetText(main_menu_language.options_rus);
					_menuControl.menu[2].SetText(main_menu_language.exit_rus);
				}

			}
		}
	}

	// OPTIONS LANGUAGE
	public void SetOptionsLanguage()
	{
		if (_optionsControl != null && _optionsControl._gameConfig != null && main_menu_language != null)
		{
			if (_optionsControl._gameConfig.language == 0)
			{ // ENGLISH
			  // BUTTONS
				buttons_options[0].text = settings_language.video_en;
				buttons_options[1].text = settings_language.audio_en;
				buttons_options[2].text = settings_language.game_en;
				buttons_options[3].text = settings_language.apply_en;
				buttons_options[4].text = settings_language.return_en;

				// VIDEO
				options_video[0].text = settings_language.displayMode_en;
				options_video[1].text = settings_language.targetDisplay_en;
				options_video[2].text = settings_language.resolution_en;
				options_video[3].text = settings_language.graphicsQuality_en;
				options_video[4].text = settings_language.antialiasing_en;
				options_video[5].text = settings_language.vsync_en;


				// AUDIO
				options_audio[0].text = settings_language.masterVolume_en;
				options_audio[1].text = settings_language.musicVolume_en;
				options_audio[2].text = settings_language.effectsVolume_en;
				options_audio[3].text = settings_language.voiceVolume_en;
				options_audio[4].text = settings_language.micVolume_en;
				options_audio[5].text = settings_language.soundBackground_en;

				// GAME
				options_game[0].text = settings_language.horizontalSensitivy_en;
				options_game[1].text = settings_language.vericalSensitivy_en;
				options_game[2].text = settings_language.difficulty_en;
				options_game[3].text = settings_language.language_en;
				options_game[4].text = settings_language.forward_en;
				options_game[5].text = settings_language.back_en;
				options_game[6].text = settings_language.left_en;
				options_game[7].text = settings_language.right_en;
				options_game[8].text = settings_language.crouch_en;
				options_game[9].text = settings_language.jump_en;
				options_game[10].text = settings_language.tips_en;

			}
			else if (_optionsControl._gameConfig.language == 1)
			{ // RUS
			  // BUTTONS
				buttons_options[0].text = settings_language.video_rus;
				buttons_options[1].text = settings_language.audio_rus;
				buttons_options[2].text = settings_language.game_rus;
				buttons_options[3].text = settings_language.apply_rus;
				buttons_options[4].text = settings_language.return_rus;

				// VIDEO
				options_video[0].text = settings_language.displayMode_rus;
				options_video[1].text = settings_language.targetDisplay_rus;
				options_video[2].text = settings_language.resolution_rus;
				options_video[3].text = settings_language.graphicsQuality_rus;
				options_video[4].text = settings_language.antialiasing_rus;
				options_video[5].text = settings_language.vsync_rus;

				// AUDIO
				options_audio[0].text = settings_language.masterVolume_rus;
				options_audio[1].text = settings_language.musicVolume_rus;
				options_audio[2].text = settings_language.effectsVolume_rus;
				options_audio[3].text = settings_language.voiceVolume_rus;
				options_audio[4].text = settings_language.micVolume_rus;
				options_audio[5].text = settings_language.soundBackground_rus;

				// GAME
				options_game[0].text = settings_language.horizontalSensitivy_rus;
				options_game[1].text = settings_language.vericalSensitivy_rus;
				options_game[2].text = settings_language.difficulty_rus;
				options_game[3].text = settings_language.language_rus;
				options_game[4].text = settings_language.forward_rus;
				options_game[5].text = settings_language.back_rus;
				options_game[6].text = settings_language.left_rus;
				options_game[7].text = settings_language.right_rus;
				options_game[8].text = settings_language.crouch_rus;
				options_game[9].text = settings_language.jump_rus;
				options_game[10].text = settings_language.tips_rus;
			}
		}
	}
}