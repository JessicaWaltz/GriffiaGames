using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody2D enemy;
    public float movementSpeed = 150f;
    public string enemyType;
    public GameObject poof;
    public bool dead = false;

    public GameObject leftPoint;
    public GameObject rightPoint;
    public GameObject upPoint;
    public GameObject downPoint;
    public bool vertical = false;
    public string startDirection;
    private string direction;
    private float startingPositionX;
    private float startingPositionY;
    private float counter = 0;
    public float panSpeed = 1f;
    public float traveldistance;

    public bool isVisible = false;
    private GameObject player;
    private bool facingRight = false;

    // Called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        

        if (enemyType != "chronosead") {
            enemy = GetComponent<Rigidbody2D>();
        }
        if (enemyType != "chronobeed")
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else {
            direction = startDirection;
        }

        startingPositionY = transform.position.y;
        startingPositionX = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        //do nothing unless on screen;
        if (enemyType == "buggy")
        {
            ChronoBuggyUpdate();
        }
        else if (enemyType == "copter")
        {
            ChronoBuggyUpdate();
            //ChronoCopterUpdate();
        }
        else if (enemyType == "chronobeed") 
        {
            ChronoBeedUpdate();
        }
        else if (enemyType == "chronosead")
        {
            ChronoSeadUpdate();
        }
        if (dead == true) {
            Instantiate(poof, transform.position, transform.rotation);
            Destroy(gameObject);
        } 
    }
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void ChronoCopterUpdate() {
        if (facingRight == true)
        {
            //go right to right point
            //if passed right point (enemy x is larger then turn around)
            //flip and facing right = false
            enemy.velocity = new Vector2(movementSpeed, enemy.velocity.y);
            if (rightPoint.transform.position.x < transform.position.x)
            {
                Flip();
                facingRight = false;
            }

        }
        else {
            //go left to left point
            //if passed left point (enemy x is smaller then turn around)
            //flip and facing right = true
            enemy.velocity = new Vector2(-movementSpeed, enemy.velocity.y);
            if (leftPoint.transform.position.x > transform.position.x)
            {
                Flip();
                facingRight = true;
            }
        }
    }
    void ChronoBuggyUpdate()
    {
        // if buggy is visble have buggy go in direction of player at that point, do not change direction
        // until enemy leaves screen and returns.
        if (/*GetComponent<Renderer>().isVisible &&*/ player)
        {
            if (player.transform.position.x > transform.position.x)
            {
                enemy.velocity = new Vector2(movementSpeed, enemy.velocity.y);
                if (facingRight == false)
                {
                    Flip();
                    facingRight = true;
                };
            }
            else if (player.transform.position.x < transform.position.x)
            {
                enemy.velocity = new Vector2(-movementSpeed, enemy.velocity.y);
                if (facingRight == true)
                {
                    Flip();
                    facingRight = false;
                };
            }
        }
        else
        {
            enemy.velocity = new Vector2(0, enemy.velocity.y);
        }
    }
    void ChronoBeedUpdate()
    {
        if (vertical == true)
        {
            //up and down
            if (direction == "up")
            {
                enemy.velocity = new Vector2(enemy.velocity.x, movementSpeed);
                if (upPoint.transform.position.y < transform.position.y)
                {
                    direction = "down";
                }
            }
            else {
                enemy.velocity = new Vector2(enemy.velocity.x, -movementSpeed);
                if (downPoint.transform.position.y > transform.position.y)
                {
                    direction = "up";
                }
            }
        }
        else
        {
            if (direction == "left")
            {
                enemy.velocity = new Vector2(-movementSpeed, transform.position.y);
                if (leftPoint.transform.position.x > transform.position.x)
                {
                    Flip();
                    direction = "right";
                }
            }
            else
            {
                enemy.velocity = new Vector2(movementSpeed, transform.position.y);
                if (rightPoint.transform.position.x < transform.position.x)
                {
                    Flip();
                    direction = "left";
                }
            }
        }
        
    
    }
    void ChronoSeadUpdate() {
        //if we are close to seed 
        if (startDirection == "left" || startDirection == "right")
        {
            if ((Mathf.Abs(player.transform.position.y - startingPositionY) <= 48f) && (Mathf.Abs(player.transform.position.x - startingPositionX) <= 48f)) 
            {
                if (counter < traveldistance && startDirection == "left")//if counter is less than the max height
                {
                    counter = counter + panSpeed * Time.deltaTime; 
                }
                else if (counter > traveldistance*-1 && startDirection == "right") {
                    counter = counter - panSpeed * Time.deltaTime;
                }
                if (direction == startDirection) {
                    Flip();
                    if (direction == "left")
                    {
                        direction = "right";
                    }
                    else 
                    {
                        direction = "left";
                    }
                }
            }
            else 
            {
                if (direction != startDirection)
                {
                    Flip();
                    if (direction == "left")
                    {
                        direction = "right";
                    }
                    else
                    {
                        direction = "left";
                    }
                }
                if (counter < 0)
                {
                    if (counter + panSpeed * Time.deltaTime > 0)
                    {
                        counter = 0;
                    }
                    else
                    {
                        counter = counter + panSpeed * Time.deltaTime;
                    }
                }
                else if (counter > 0)
                {
                    if (counter - panSpeed * Time.deltaTime < 0)
                    {
                        counter = 0;
                    }
                    else
                    {
                        counter = counter - panSpeed * Time.deltaTime;
                    }
                }
            }
            transform.position = new Vector3(startingPositionX + counter, transform.position.y, transform.position.z);
        }
        else
        {
            if ((Mathf.Abs(player.transform.position.y - startingPositionY) <= 48f) && (Mathf.Abs(player.transform.position.x - startingPositionX) <= 48f))
            {
                //if we have not gone down all the way
                if (counter < traveldistance)//if counter is less than the max height
                {
                    counter = counter + panSpeed * Time.deltaTime; ;
                }
            }
            else//go back up to starting spot
            {
                if (counter < 0)
                {
                    if (counter + panSpeed * Time.deltaTime > 0)
                    {
                        counter = 0;
                    }
                    else
                    {
                        counter = counter + panSpeed * Time.deltaTime;
                    }
                }
                else if (counter > 0)
                {
                    if (counter - panSpeed * Time.deltaTime < 0)
                    {
                        counter = 0;
                    }
                    else
                    {
                        counter = counter - panSpeed * Time.deltaTime;
                    }
                }
            }
            transform.position = new Vector3(transform.position.x, startingPositionY - counter, transform.position.z);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "projectile")
            dead = true;
    }
}