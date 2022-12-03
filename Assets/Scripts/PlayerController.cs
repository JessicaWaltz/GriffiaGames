using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed = 1.25f;
    private float JumpPower = 3.5f;
    //protected Vector2 velocity;
    //private float gravityModifier = 1f;

    public Animator anim;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    private Rigidbody2D rb;

    private string facing = "right";
    private bool isUp;
    private bool isDown;
    private bool isLeft;
    private bool isRight;

    private bool isJump = false;
    private bool isGround = false;

    private bool didSpring = false;

    private bool hitEnemy = false;
    private bool takeDamage = false;
    private float xLocationEnemy;
    private float xLocationPlayer;

    private bool touchingPushable = false;
    private bool canPush = false;

    private bool canClimb = false;
    private bool isClimbing = false;


    public int coinCount = 0;



    // Called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();// will look for a component on this game object
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().GetInstanceID());
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the current clip info and name;
        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        m_ClipName =m_CurrentClipInfo[0].clip.name;
        

        isUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        isDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        isLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        isRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        animationVariables();
        Movement();
        //animationVariables();

    }
    void animationVariables() {
        anim.SetBool("isUp", isUp);
        anim.SetBool("isDown", isDown);
        anim.SetBool("isLeft", isLeft);
        anim.SetBool("isRight", isRight);
        anim.SetBool("isSpace", Input.GetKey(KeyCode.Space));
        anim.SetBool("isJump",isJump);
        anim.SetBool("isGround", isGround);
        anim.SetBool("takeDamage", takeDamage);
        anim.SetBool("didSpring", didSpring);
        anim.SetBool("hitEnemy",hitEnemy);
        anim.SetBool("canPush", canPush);
        anim.SetBool("touchingPushable", touchingPushable);
        anim.SetFloat("YSpeed", rb.velocity.y);
        anim.SetFloat("XSpeed", rb.velocity.x);
        anim.SetFloat("playerX", transform.position.x);
        anim.SetFloat("playerY", transform.position.y);
        anim.SetBool("canClimb", canClimb);
        anim.SetBool("isClimbing", isClimbing);
        //anim.SetFloat("pushableX", pushableX);
        // anim.SetFloat("pushableY", pushableY);
    }
    void Movement() { 
        
        Walk();
        Jump();
        Climbing();
        //Look();
    }
    void Climbing() {
        if (canClimb && (isUp || isDown || isClimbing))
        {
            isClimbing = true;
            rb.gravityScale = 0;
            if (isUp)
            {
                //have character climb up
                rb.velocity = new Vector2(rb.velocity.x, movementSpeed * 0.75f);
            }
            else if (isDown)
            {
                //have character climb down
                rb.velocity = new Vector2(rb.velocity.x, -movementSpeed * 0.75f);
            }
            else {
                //have character sit still
               
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

        }
        else if (!canClimb) {
            isClimbing = false;
            rb.gravityScale = 1;
        }
    }
    void Walk()
    {
        //for left and right movement
        //if the player is not looking up or crouching then they can move
        // need to add if player is not taking damage then they can move as well
        if (isLeft && m_ClipName != "Player_lookup_Animation" && m_ClipName != "Player_crouch_Animation")// && m_ClipName != "Player_hurt_Animation"
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        else if (isRight && m_ClipName != "Player_lookup_Animation" && m_ClipName != "Player_crouch_Animation")
        {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        else
        {
            //this is for damage jump if they are taking damage then the x movement stayes what it was perviously, if not set x to 0 
            //because they stopped pressing a direction
            if (m_ClipName != "Player_hurtup_Animation" && m_ClipName != "Player_hurtdown_Animation")
            {
                rb.velocity = new Vector2(0, rb.velocity.y);

            }
        }
        // for correct animations
        if (isLeft && (m_ClipName == "Player_Idle_Animation" || m_ClipName == "Player_run_Animation"))
        {
            if (facing == "right")
            {
                facing = "left";
                Flip();
            }
        }
        else if (isRight && (m_ClipName == "Player_Idle_Animation" || m_ClipName == "Player_run_Animation"))
        {
            if (facing == "left")
            {
                facing = "right";
                Flip();
            }
        }
    }
    void Jump() 
    {
        //if hitting jump and on ground
        if (Input.GetKey(KeyCode.Space) && isGround == true && (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0.001 && gameObject.GetComponent<Rigidbody2D>().velocity.y >= -0.001))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower), ForceMode2D.Impulse);
            isGround = false;

        }
        if (takeDamage == true)//if enemy hit player
        {
            if (xLocationEnemy > xLocationPlayer)
            {
                // enemy is to right so player damage jumps left
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, JumpPower * 0.75f), ForceMode2D.Impulse);
            }
            else
            {
                // enemy is to left so player damage jumps right
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, JumpPower * 0.75f), ForceMode2D.Impulse);
            }
            isGround = false;
            takeDamage = false;
        }
        //if player is falling animation (where velocity would be very negative) but velocity is 0 then 
        if (rb.velocity.y >= 0 && (m_ClipName == "Player_falldown_Animation" || m_ClipName == "Player_hurtdown_Animation"))
        {
            if (didSpring == true)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower * 1.75f), ForceMode2D.Impulse);
                isGround = false;
                didSpring = false;
            }
            else if (hitEnemy == true)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower * 0.75f), ForceMode2D.Impulse);
                isGround = false;
                hitEnemy = false;
            }

        }
        if (rb.velocity.y < -0.001) { isGround = false; }

    }
 
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //if collision with enemy happens check if we were jumping and location
    // if jumping (on way down) and were above enemy then we poofed enemy
    // else enemy got us and we get nocked back
    // for nocked back check if we hit from left or right of enemy
    // bounce back diagonaly from enemy / or \ 
    void OnCollisionEnter2D(Collision2D collision)
    {   

        if (collision.gameObject.tag == "Enemy")
        {
            if (m_ClipName == "Player_falldown_Animation")
            {
                hitEnemy = true;
            }
            else {//take damage 
                takeDamage = true;
                xLocationEnemy = collision.gameObject.GetComponent<Rigidbody2D>().transform.position.x;
                xLocationPlayer = transform.position.x;
                //Debug.Log("we have taken damage");

            }
            //Debug.Log("been got!");
        }
        else if (collision.gameObject.tag == "Spring" && m_ClipName == "Player_falldown_Animation")
        {
            didSpring = true;
        }
        else if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        else if (collision.gameObject.tag == "Coin")
        {
            coinCount = coinCount + 1;
            Debug.Log(coinCount);
        }
        else if (collision.gameObject.tag == "Push")
        {
            isGround = true;
            touchingPushable = true;
            var pushableX = collision.gameObject.GetComponent<Rigidbody2D>().transform.position.x;
            var pushableY = collision.gameObject.GetComponent<Rigidbody2D>().transform.position.y;
            if (pushableY < transform.position.y - .1f)
            {
                Debug.Log("No push!");
                canPush = false;
            }
            else {
                Debug.Log("Yes Push!");
                canPush = true;
            }
            //set touching pushable object to true
            //get height of pushable object 
            // if player is higher than pushable object then no push
            // if player is on same level as pushable then push
        }
        

    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Push")
        {
            touchingPushable = false;
            
            //set touching pushable object to false


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climb")
        {
            canClimb = true;
            Debug.Log("Oh boy!");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climb")
        {
            canClimb = false;
        }
    }
    //climbing
    //if hit up while colliding with climbable object then climbing is true


















    void Jump2()
    {

        //if hitting jump and on ground
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.y);
        if (Input.GetKey(KeyCode.Space) && isGround == true && (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0.001 && gameObject.GetComponent<Rigidbody2D>().velocity.y >= -0.001))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower), ForceMode2D.Impulse);
            anim.Play("Player_jumpup_Animation");
            isGround = false;
            //Debug.Log("First Jump");

        }
        if (takeDamage == true)//if enemy hit player
        {
            if (xLocationEnemy > xLocationPlayer)
            {
                // enemy is to right so player damage jumps left
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, JumpPower * 0.75f), ForceMode2D.Impulse);
            }
            else
            {
                // enemy is to left so player damage jumps right
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, JumpPower * 0.75f), ForceMode2D.Impulse);
            }
            anim.Play("Player_hurtup_Animation");
            //Debug.Log("hurt animation starts playing");
            isGround = false;
            takeDamage = false;
        }
        //if player is falling animation (where velocity would be very negative) but velocity is 0 then 
        if (rb.velocity.y >= 0 && (m_ClipName == "Player_falldown_Animation" || m_ClipName == "Player_hurtdown_Animation"))
        {
            if (didSpring == true)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower * 1.75f), ForceMode2D.Impulse);
                anim.Play("Player_jumpup_Animation");
                isGround = false;
                didSpring = false;
            }
            else if (hitEnemy == true)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower * 0.75f), ForceMode2D.Impulse);
                anim.Play("Player_jumpup_Animation");
                isGround = false;
                hitEnemy = false;
            }
            else if (isGround == true)
            {
                anim.Play("Player_Idle_Animation");
            }
        }

        //else if player is in jump animation and velocity is under o then also fall 
        else if (rb.velocity.y < 0 && (m_ClipName == "Player_jumpup_Animation" || m_ClipName == "Player_hurtup_Animation"))
        {
           
            if (m_ClipName == "Player_jumpup_Animation")
            {
                anim.Play("Player_falldown_Animation");
            }
            else
            {
                anim.Play("Player_hurtdown_Animation");
            }

        }
        //else if player velocity is lower than 0 they are falling so play falling animation
        else if (rb.velocity.y < -0.01 && (m_ClipName != "Player_hurtup_Animation" && m_ClipName != "Player_hurtdown_Animation"))
        {
            anim.Play("Player_falldown_Animation");

        }
    }

}


