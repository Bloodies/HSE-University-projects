using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponent : MonoBehaviour
{
    private SpriteRenderer sprt;
    private BoxCollider2D box;
    
    void Start()    // Start is called before the first frame update
    {
        sprt = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }
    
    void Update()   // Update is called once per frame
    {
        OnTriggerEnter2D(box);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            sprt.enabled = false;
        }
    }
}