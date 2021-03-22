using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SettingsButtonManager : MonoBehaviour
{
    public GameObject Canvas;

    public bool isActiveCanvas = false; //Открыто ли меню

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideMenu();
        }
    }

    public void Show_panel(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Hide_panel(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ShowHideMenu()
    {
        isActiveCanvas = !isActiveCanvas;
        if (isActiveCanvas == false)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            Time.timeScale = 0;
        }
        //GetComponent<Canvas>().enabled = isOpened; //Включение или отключение Canvas. Ещё тут можно использовать метод SetActive()
        Canvas.SetActive(isActiveCanvas);
    }

    public void QuitGame()
    {
        Application.Quit(); //Закрытие игры. В редакторе, кончено, она закрыта не будет, поэтому для проверки можно использовать Debug.Log();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Main_screen"); //Переход на сцену с названием Menu
    }

    public void GoBack()
    {
        ShowHideMenu();
    }
}
