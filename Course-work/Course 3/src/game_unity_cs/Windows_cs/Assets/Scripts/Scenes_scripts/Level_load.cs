using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_load : MonoBehaviour
{
    public int Level;
    private bool ready_b = false;
    
    void Start()    // Start is called before the first frame update
    {

    }
    
    void Update()   // Update is called once per frame
    {

    }
    private void PressedButton()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ready_b = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            SceneManager.LoadScene(Level);
            //Application.LoadLevel(Level);
        }
    }
}