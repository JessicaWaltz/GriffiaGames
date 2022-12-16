using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeadBuggy : MonoBehaviour
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
    public bool isMoving = false;
    private bool isOnGround = false;
    private bool cancelTurnAround = false;

    public GameObject poof;

    // Start is called before the first frame update
    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // when in state 1 we move back and forth
        if (currentState == 1)
        {
            if (SeePlayer())
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                currentState = 2;
            }
            else
            {
                Move();
                if (!isMoving)
                {
                    isMoving = true;
                    Invoke("TurnAround", 5f);
                }
            }
        }
        //state 2 we notice the player, do a tiny jump
        else if (currentState == 2)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3*speed);
            currentState = 3;
            isOnGround = false;
            isMoving = false;
            cancelTurnAround = true;

        }
        //state 3 we charge for 1.5f soon as we hit ground
        else if (currentState == 3) {
            if (isOnGround) {
                if (!isMoving)
                {
                    isMoving = true;
                    Invoke("TurnAround2", 1.5f);
                }
                MoveFast();
            }
        }
        //state 4 charge is over, we turn around, start state 1 again
    }
    private void TurnAround() {
        if (currentState == 1 && !cancelTurnAround) {
            Flip();
            facingLeft = !facingLeft;
            isMoving = false;
        }
        else { cancelTurnAround = false;}
    }
    private void TurnAround2() {
        if (currentState == 3)
        {
            Flip();
            facingLeft = !facingLeft;
            isMoving = false;
            currentState = 1;
        }

    }
    private void Move()
    {
        if (facingLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0); }
        else if (!facingLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0); }
    }
    private void MoveFast()
    {
        if (facingLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(-speed * 3, 0); }
        else if (!facingLeft) { GetComponent<Rigidbody2D>().velocity = new Vector2(speed *3, 0); }
    }
    bool SeePlayer() {
        float distanceFromPlayer = player.transform.position.x - transform.position.x;
        if (facingLeft && -distanceFromPlayer <= lineOfSiteRangeX && distanceFromPlayer < 0)
        {
            return true;
        }
        else if (!facingLeft && distanceFromPlayer <= lineOfSiteRangeX && distanceFromPlayer > 0) 
        {
            return true;
        }
        return false;

    }
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    /*
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
    */
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
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
