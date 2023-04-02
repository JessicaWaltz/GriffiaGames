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

    public float lineOfSiteRangeY = 100;
    public float lineOfSiteRangeX = 120;

    public float currentState = 1;
    private bool isOnGround = false;

    public GameObject poof;
    public GameObject exclamation;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        IgnoreThisCollision("Player");
        IgnoreThisCollision("Enemy");
        IgnoreThisCollision("Ground");
        
    }
    private void IgnoreThisCollision(string tag) 
    {
        GameObject[] items= GameObject.FindGameObjectsWithTag(tag);
        if (items != null)
        {
            foreach (GameObject item in items)
            {
                Collider2D[] itemColliders = item.GetComponentsInChildren<Collider2D>();
                Collider2D[] objectColliders = GetComponentsInChildren<Collider2D>();
                foreach (Collider2D itemCollider in itemColliders)
                {
                    foreach (Collider2D objectCollider in objectColliders)
                    {
                        Physics2D.IgnoreCollision(itemCollider, objectCollider);
                    }
                }
            }
        }
    }


    void Update()
    {
        if (currentState == 1)//state 1 move and look for player
        {
            if (SeePlayer())//if player found move to state 2
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                currentState = 2;
            }
            else{ Move(); }
        }
        else if (currentState == 2)//wait .5 seconds then move to state 3
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 20, 0);
            Instantiate(exclamation, spawnPosition, transform.rotation);
            Invoke("TimerToThree", 0.5f);
            currentState = 0;
        }
        else if (currentState == 3) //If the player is within 15 units move to state 4
        {
            if (SeePlayer(15)) {currentState = 4;}
            else {
                Move(true);
            }
        }
        else if (currentState == 4) //wait .5 seconds then move to state 5
        {
            Invoke("TimerToFive", 0.5f);
            currentState = 0;
        }
        else if (currentState == 5) // jump and move to state 6
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 7 * speed);
            isOnGround = false;
            anim.SetBool("isGround", isOnGround);
            currentState = 6;
        }
        else if (currentState == 6 && isOnGround == true) //once on the ground move to state 2
        {
            currentState = 1;
        }
        anim.SetBool("isGround", isOnGround);
    }
    void TimerToThree() {
        currentState = 3;
        Invoke("GiveUpOnThree", 2f);

    }
    void GiveUpOnThree() {
        if(currentState == 3) { 
            currentState = 1;
        }
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
        float distanceFromPlayerX = player.transform.position.x - transform.position.x;
        float distanceFromPlayerY = player.transform.position.y - transform.position.y;
        if (((distanceFromPlayerX <= distance && distanceFromPlayerX > 0) || (-distanceFromPlayerX <= distance && distanceFromPlayerX < 0)) &&
            ((distanceFromPlayerY >= lineOfSiteRangeY && distanceFromPlayerY > 0) || (-distanceFromPlayerY >= lineOfSiteRangeY && distanceFromPlayerY < 0)))
        {
            anim.SetBool("hasNoticed", true);
            return true;
        }
        anim.SetBool("hasNoticed", false);
        return false;

    }

    private void Move(bool toPlayer = false)
    {
        
        if (toPlayer)
        {
            bool playerIsLeft = (player.transform.position.x - transform.position.x) < 0;
            float fastSpeed = (float)(speed * 1.75);
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
        else if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.tag == "waterGround") 
        {
            isOnGround = true;
            anim.SetBool("isGround", isOnGround);
        }
        else if (collision.gameObject.tag == "Turn")
        {
            Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "projectile" && !collision.gameObject.GetComponent<projectileLogic>().isEnemyProjectile)
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
