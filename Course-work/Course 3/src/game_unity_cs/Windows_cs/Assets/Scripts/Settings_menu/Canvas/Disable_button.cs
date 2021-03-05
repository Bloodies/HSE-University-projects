using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Disable_button : MonoBehaviour
{
    private Canvas canv;
    // Start is called before the first frame update
    void Start()
    {
        canv = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            canv.enabled = false;
            SceneManager.LoadScene(0);
        }
    }
}