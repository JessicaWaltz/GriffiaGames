using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageFlash : MonoBehaviour
{
    public SpriteRenderer myRenderer;
    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;

    public bool takeDamage;
    private float damagetimer = 0;
    private bool isInvulnerable = false;
    private string objectTag;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
        objectTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
    }
    //InvokeRepeating
    public void StartDamageAnimation(float damagetimer) {
        if (isInvulnerable) return;
        this.damagetimer = damagetimer;
        isInvulnerable = true;
        whiteSprite();
        if (objectTag == "Player") {
            Physics2D.IgnoreLayerCollision(11, 14, true);
            Debug.Log("Bounce back");
        }
    }
    private void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
        damagetimer = damagetimer - 0.1f;
        Invoke("normalSprite", 0.1f);
    }
    private void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
        damagetimer = damagetimer - 0.1f;
        if (damagetimer > 0) { Invoke("whiteSprite", 0.1f); }
        else { 
            isInvulnerable = false;
            if (objectTag == "Player")
            {
                Physics2D.IgnoreLayerCollision(11, 14, false);
                Debug.Log("Bounce back");
            }
        }

            
    }

    void setInvulnerability(bool isInvulnerable) {
        // make object not interact with enemy boxes and projectiles
        this.isInvulnerable = isInvulnerable;
    }
    public bool checkInvulnerability() {
        return isInvulnerable;
    }

    /*
     // Disable collision between Player and Enemy layer
Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

// Enable collision between Player and Enemy layer
Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);

// Disable collision between Player and Projectile layer
Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, true);

// Enable collision between Player and Projectile layer
Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, false);*/

}
