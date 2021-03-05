using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Скрипт для выбора в меню

public class Menu_select : MonoBehaviour
{

	// Menu
	public List<Menu> menusActives = new List<Menu>();
	public List<Menu> menusDisables = new List<Menu>();

	public int previousMenu = -1;
	public int currentMenu = 0;

	public Menu currentMenuComponent;

	private int _a = 0;
	private Menu_controller menuControl;

	// Use this for initialization
	void Start()
	{
		menuControl = FindObjectOfType<Menu_controller>();
		SeparateActives();

	}

	// Update is called once per frame
	void Update()
	{
		if (menuControl.useKeyboard == false)
		{
			return;
		}

		if (_a == 0)
		{
			SetMenu();
			_a = 1;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (currentMenu <= 0)
			{
				currentMenu = menusActives.Count - 1;
				previousMenu = 0;
			}
			else
			{
				currentMenu--;
				previousMenu = currentMenu + 1;
			}
			SetMenu();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (currentMenu >= menusActives.Count - 1)
			{
				currentMenu = 0;
				previousMenu = menusActives.Count - 1;
			}
			else
			{
				currentMenu++;
				previousMenu = currentMenu - 1;
			}
			SetMenu();
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			currentMenuComponent.CallTheEvent(2);
		}
	}

	void SetMenu()
	{
		int i = 0;

		foreach (Menu menu in menusActives)
		{
			if (i == previousMenu)
			{
				menu.CallTheEvent(1);
			}

			if (i == currentMenu)
			{
				currentMenuComponent = menu;
				currentMenuComponent.CallTheEvent(0);
			}
			i++;
		}
	}

	void SeparateActives()
	{
		menusActives.Clear();
		foreach (Menu menu in menusDisables)
		{
			if (menu.active == true)
			{
				menusActives.Add(menu);
			}
		}
	}
}