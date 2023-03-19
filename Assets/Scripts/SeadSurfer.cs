using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeadSurfer : MonoBehaviour
{
    public float health = 5;
    private SpriteRenderer mySpriteRenderer;
    public Animator anim;
    private GameObject player;
    public bool facingLeft = true;
    public float speed = 50f;

    public float lineOfSiteRangeY;
    public float lineOfSiteRangeX = 120;

    public float currentState = 1;
    private bool isOnGround = false;

    public GameObject poof;
    public GameObject exclamation;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == 1)
        {
            if (SeePlayer())
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                currentState = 2;
            }
            else
            {
                Move();
            }
        }
        else if (currentState == 2)
        {
            /* have noticed animation then...
               move to player
               have about to jump animation
               then jump 
            */

            //play exclamation for notice
            //pause briefly
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 20, 0);
            Instantiate(exclamation, spawnPosition, transform.rotation);
            Invoke("TimerToThree", 0.5f);
            currentState = 0;
        }
        else if (currentState == 3) 
        {
            if (SeePlayer(15)) {
                currentState = 4;
            }
            else {
                Move(true);
            }
        }
        else if (currentState == 4) 
        {
            Invoke("TimerToFive", 0.5f);
            currentState = 0;
        }
        else if (currentState == 5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 7 * speed);
            isOnGround = false;
            currentState = 6;
        }
        else if (currentState == 6 && isOnGround == true) {
            currentState = 1;
        }
    }
    void TimerToThree() {
        currentState = 3;
    }
    void TimerToFive()
    {
        currentState = 5;
    }


    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        facingLeft = !facingLeft;
    }
    bool SeePlayer(float distance = -1)
    {
        if (distance < 0) {
            distance = lineOfSiteRangeX;
        }
        float distanceFromPlayer = player.transform.position.x - transform.position.x;
        if ((distanceFromPlayer <= distance && distanceFromPlayer > 0) || (-distanceFromPlayer <= distance && distanceFromPlayer < 0))
        {
            return true;
        }
        return false;

    }

    private void Move(bool toPlayer = false)
    {
        
        if (toPlayer)
        {
            bool playerIsLeft = (player.transform.position.x - transform.position.x) < 0;
            float fastSpeed = (float)(speed * 1.5);
            if (facingLeft && playerIsLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(-fastSpeed, 0); }
            else if (!facingLeft && !playerIsLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(fastSpeed, 0); }
            else if (facingLeft && !playerIsLeft) { Flip(); GetComponent<Rigidbody2D>().velocity = new Vector2(-fastSpeed, 0); }
            else if (!facingLeft && playerIsLeft) { Flip(); GetComponent<Rigidbody2D>().velocity = new Vector2(fastSpeed, 0); }
        }
        else
        {
            if (facingLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0); }
            else if (!facingLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0); }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "projectile")
        {
            health -= 1;
            gameObject.GetComponent<damageFlash>().StartDamageAnimation(0.1f);
            if (health <= 0)
            {
                Instantiate(poof, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.tag == "waterGround") 
        {
            isOnGround = true;
        }
        else if (collision.gameObject.tag == "Turn")
        {
            Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "projectile")
        {
            health -= 1;
            gameObject.GetComponent<damageFlash>().StartDamageAnimation(0.1f);
            if (health <= 0)
            {
                Instantiate(poof, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

}
