using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlayerController : MonoBehaviour
{
    public float movementSpeed = 125f;
    private float jumpPower = 275f;

    private Rigidbody2D rb;
    private HealthController healthCounter;

    public Animator anim;
    private AnimatorClipInfo[] m_CurrentClipInfo;

    public float jumpForce = 400f;
    public float damageJumpForce = 300f;


    public string facing = "left";
    private bool isUp;
    private bool isDown;
    private bool isLeft;
    private bool isRight;
    private bool isThrow;
    private bool dead;

    public bool isActive;

    private bool didSpring = false;
    private bool hitEnemy = false;


    private float maxVelocity = -275f;

    public bool isCutscene = true;

    public AudioClip coin;
    public AudioClip step;
    public AudioClip hit;
    public AudioClip jump;
    public AudioClip spring;

    private AudioSource audioSource;
    public Vector3 respawnLocation;


    //Have this be StarPlayerController, This is the only one that has an update, All controllers that Star uses will be in here
    //all related animations should be here and audio clips to be called

    // sounds

    //movement,jump, flip, ground collisions all in one controller but different functions to call if used

    //Damage taken should be its own controller

    //Coin collection

    //Audio Player

    //InCutscene should be own controller to stop all controllers when cutscene

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        healthCounter = FindObjectOfType<HealthController>();
        respawnLocation = transform.position;
        IgnoreThisCollision("waterGround");
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            //Only allow inputs when not dead and not when falling due to taking damage
            if (gameObject.GetComponent<PlayerMovementController>().isDamageFall == false && !healthCounter.IsDead()) 
            {
                isUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
                isDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
                isLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
                isRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
                isThrow = Input.GetKeyDown(KeyCode.J);
            }

            
            m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);

            //Player set to be in cutscene at beginning 
            anim.SetBool("isOppeningCutscene", isCutscene);
            //If the cutscene is over then:
            if (!isCutscene)
            {
                //Check other animation variables and apply max speed of gravity
                AnimationVariables();
                MaxVelocity();
                //If the player is not currently in a damage fall and they are not dead then
                if (gameObject.GetComponent<PlayerMovementController>().isDamageFall == false && !healthCounter.IsDead()) { 
                    //Allow player to move and shoot
                    Movement();
                    gameObject.GetComponent<PlayerProjectileController>().CheckThrow(isThrow, isUp, isDown, facing);
                }
                
            }
            
        }
    }
    void AnimationVariables()
    {
        //These are the Animation Variables for the Character Star, Others might have slightly differing animations/conditions
        if (gameObject.name == "StarPlayer") {
            anim.SetBool("isUp", isUp);
            anim.SetBool("isDown", isDown);
            anim.SetBool("isLeft", isLeft);
            anim.SetBool("isRight", isRight);
            anim.SetBool("facingLeft", facing == "left");
            anim.SetBool("facingRight", facing == "right");
            anim.SetBool("isSpace", Input.GetKey(KeyCode.Space));
            anim.SetBool("isJump", gameObject.GetComponent<PlayerMovementController>().IsJump());
            anim.SetBool("takeDamage", gameObject.GetComponent<PlayerMovementController>().isDamageFall);
            anim.SetBool("isGround", gameObject.GetComponent<PlayerMovementController>().IsGround());
            anim.SetBool("didSpring", didSpring);
            anim.SetBool("hitEnemy", hitEnemy);
            anim.SetBool("isThrow", isThrow);
            //anim.SetBool("sprung", gameObject.GetComponent<SpringController>().isSpring());
            anim.SetFloat("YSpeed", rb.velocity.y);
            anim.SetBool("dead", healthCounter.IsDead());

            didSpring = false;

        }
    }
    void Movement()
    {
        facing = gameObject.GetComponent<PlayerMovementController>().Walk(isLeft,isRight, isUp, isDown,  rb,  movementSpeed,  facing,  m_CurrentClipInfo);
        gameObject.GetComponent<PlayerMovementController>().Jump(jump,rb,jumpPower);
    }


    void MaxVelocity() {
        if (rb.velocity.y < maxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
        }
        if (rb.velocity.y > jumpForce)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            gameObject.GetComponent<PlayerAudioController>().AudioPlayerSoft(coin);
        }
        else if (collision.gameObject.tag == "trigger") {
            // save location of player when triggered
            respawnLocation = transform.position;
        }
    }

    private void IgnoreThisCollision(string tag)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(tag);
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
    //In the case of death we want to:
    //- Wait 3 seconds on dead frame
    //- call Fade to Black controller to fade to black
    //- While screen is black we want to check if lives are left.
    //  - If there are lives left
    //      - we want to add +5 to health and -1 life
    //      - reset all spawn point triggers to isTriggered = false;
    //      - destroy all spawned enemies
    //      - place Player right before last spawn point trigger
    //      - fade from black
    //  - If no lives are left
    //      - Display Game over screen over the black out
    //      - have continue button and quit button
    //      - continue button restarts the scene completely
    //      - quit button returns you to menu
}