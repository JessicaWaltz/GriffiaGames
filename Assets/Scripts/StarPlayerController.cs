using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlayerController : MonoBehaviour
{
    public float movementSpeed = 125f;
    private float jumpPower = 275f;

    private Rigidbody2D rb;

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

    public bool isActive;

    //private bool isSpecialJump = false;
    //private bool isJump = false;
    //public bool isGround = false;
    //private bool hascomedown = false;

    private bool didSpring = false;
    private bool hitEnemy = false;

    private bool takeDamage = false;
    private bool nockedBack = false;
    //private float xLocationEnemy;
    //private float xLocationPlayer;

    private float maxVelocity = -275f;

    //public float allGroundCollisions = 0;

    public bool isCutscene = true;

    public AudioClip coin;
    public AudioClip step;
    public AudioClip hit;
    public AudioClip jump;
    public AudioClip spring;

    private AudioSource audioSource;

    public SpriteRenderer myRenderer;
    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;

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

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            isUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
            isDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
            isLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
            isRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
            isThrow = Input.GetKeyDown(KeyCode.J);

            
            m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);

            anim.SetBool("isOppeningCutscene", isCutscene);
            if (!isCutscene)
            {
                AnimationVariables();
                Movement();
                MaxVelocity();
                gameObject.GetComponent<PlayerProjectileController>().CheckThrow(isThrow,isUp,isDown,facing);
            }
            
        }
    }
    void AnimationVariables()
    {
        if (gameObject.name == "StarPlayer") {
            anim.SetBool("isUp", isUp);
            anim.SetBool("isDown", isDown);
            anim.SetBool("isLeft", isLeft);
            anim.SetBool("isRight", isRight);
            anim.SetBool("facingLeft", facing == "left");
            anim.SetBool("facingRight", facing == "right");
            anim.SetBool("isSpace", Input.GetKey(KeyCode.Space));
            anim.SetBool("isJump", gameObject.GetComponent<PlayerMovementController>().IsJump());
            anim.SetBool("takeDamage", takeDamage || nockedBack);
            anim.SetBool("isGround", gameObject.GetComponent<PlayerMovementController>().IsGround());
            anim.SetBool("didSpring", didSpring);
            anim.SetBool("hitEnemy", hitEnemy);
            anim.SetBool("isThrow", isThrow);
            //anim.SetBool("sprung", gameObject.GetComponent<SpringController>().isSpring());
            anim.SetFloat("YSpeed", rb.velocity.y);

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

    }
}



/*
 if (collision.gameObject.tag == "NeedWall")
        {
            AudioPlayerSoft(hit);
            nockedBack = true;
            isSpecialJump = true;
            xLocationEnemy = collision.gameObject.GetComponent<Rigidbody2D>().transform.position.x;
            xLocationPlayer = transform.position.x;
        }
*/

/*void DamageCheck() {
        if (takeDamage == true)//if enemy hit player
        {
            if (xLocationEnemy > xLocationPlayer)
            {
                // enemy is to right so player damage jumps left
                rb.velocity = new Vector2(-175, jumpPower * 0.75f);
            }
            else
            {
                // enemy is to left so player damage jumps right
                rb.velocity = new Vector2(175, jumpPower * 0.75f);
            }
            anim.SetBool("takeDamage", takeDamage);
            takeDamage = false;
            isSpecialJump = true;
        }
        else if (nockedBack == true)//if enemy hit player
        {
            //this is currently adding to current y velocity we want it to replace
            if (xLocationEnemy > xLocationPlayer)
            {
                // enemy is to right so player damage jumps left
                rb.velocity = new Vector2(-125, jumpPower * 1.25f);
            }
            else
            {
                // enemy is to left so player damage jumps right
                rb.velocity = new Vector2(125, jumpPower * 1.25f);
            }
            anim.SetBool("takeDamage", nockedBack);
            nockedBack = false;
            isSpecialJump = true;
        }
    }*/