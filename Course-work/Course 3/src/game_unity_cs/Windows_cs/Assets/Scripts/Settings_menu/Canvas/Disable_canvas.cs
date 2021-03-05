using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Disable_canvas : MonoBehaviour
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            canv.enabled = false;
        }
    }
}