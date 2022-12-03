using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beedCopter : MonoBehaviour
{
    private bool facingLeft = true;
    private GameObject player;

    public Animator anim;
    public bool shoot = false;
    private SpriteRenderer mySpriteRenderer;
    public GameObject projectileObject;
    public GameObject poof;

    private float startingY;
    public float range = 50;
    public float speed = 50f;
    public bool goingUp = false;

    public bool decendComplete = true;
    public float decendAmount = 50;
    private bool noShootYet = true;
    public float shootInterval = 1.5f;

    public float health = 2;



    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        startingY = gameObject.transform.position.y - decendAmount;
        player = GameObject.FindGameObjectWithTag("Player");
        if(decendComplete != true)
            Invoke("LaunchProjectile", 2f);
    }
    //want to move up and down while firing always looking at player
    
    void Update()
    {
        anim.SetBool("shoot",shoot);
        if (shoot) {
            Invoke("LaunchProjectile", 0f);
            shoot = false;
        }

        if (noShootYet && decendComplete)
        {
            noShootYet = false;
            Invoke("ShootisTrue", shootInterval);
        }
        else if (!decendComplete)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            if (gameObject.transform.position.y <= startingY){ decendComplete = true; }
        }
        else
        {
            if (gameObject.transform.position.y < (startingY + range) && goingUp){ GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed); }
            else if (gameObject.transform.position.y > startingY - range && !goingUp){ GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed); }
            else { goingUp = !goingUp; }
        }

        if (facingLeft == true && player.transform.position.x > transform.position.x)
        {
            Flip();
            facingLeft = false;
        }
        else if(facingLeft == false && player.transform.position.x < transform.position.x)
        {
            Flip();
            facingLeft = true;
        }
    }
    void Flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void LaunchProjectile()
    {
        float startX = 20;
        float startY = 7;
        float speedX = 100;
        float speedY = 175;
        
        var projectile = facingLeft == false
                ? Instantiate(projectileObject, new Vector3(transform.position.x + startX, transform.position.y + startY, transform.position.z), transform.rotation)
                : Instantiate(projectileObject, new Vector3(transform.position.x - startX, transform.position.y + startY, transform.position.z), transform.rotation);
        projectile.GetComponent<projectileLogic>().velocityX = facingLeft == false
            ? speedX
            : -speedX;
        projectile.GetComponent<projectileLogic>().velocityY = speedY;
    }
    void ShootisTrue()
    {
        shoot = true;
        Invoke("ShootisTrue", 1.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectile") {
            health -= 1;
            if (health <= 0) {
                Instantiate(poof, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
            
    }
}
