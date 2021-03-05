using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    private Canvas canv;
    
    void Start()    // Start is called before the first frame update
    {
        canv = GetComponentInChildren<Canvas>();
    }
    
    void Update()   // Update is called once per frame
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            canv.enabled = !canv.enabled;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            canv.enabled = !canv.enabled;
        }
    }
}