using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Time.timeScale = 0;
    void Update()
    {
        
    }/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectile") {
            Invoke("pauseforDamage", 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            Invoke("pauseforDamage", 0f);
        }
    }
    void pauseforDamage() { 
        Time.timeScale = 0.01f;
        Invoke("unpauseforDamage", 0.01f);
    }
    void unpauseforDamage() { Time.timeScale = 1; }*/
}
