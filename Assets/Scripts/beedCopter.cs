using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// add y range functionality
// add sound effects for when hit
// add sounf effect for when jump 
// add vroom sound?
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
    public float shootInterval = 3f;

    public float health = 2;

    private float coolDownTimer = 1f;
    private bool coolDownComplete = true;
    private float firingRange = 150f;
    private float distanceToPlayer;



    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        startingY = gameObject.transform.position.y - decendAmount;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //want to move up and down while firing always looking at player
    
    void Update()
    {
        anim.SetBool("shoot",shoot);
        shoot = false;

        //if havent finished decend then keep decending else go up and down
        if (!decendComplete)
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

        //Change direction 
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

        //if player is within range and shoot cooldown reached then shoot
        distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (coolDownComplete && distanceToPlayer < firingRange) {
            LaunchProjectile();
            shoot = true;
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

        coolDownComplete = false;
        Invoke("CoolDown", coolDownTimer);
    }
    void CoolDown() {
        coolDownComplete = true;
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectile") {
            health -= 1;
            gameObject.GetComponent<damageFlash>().StartDamageAnimation(0.1f);
            coolDownTimer = coolDownTimer / 1.5f;
            if (health <= 0) {
                Instantiate(poof, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
            
    }
}

/*
Pead behavior
- on spawn travel towards set direction 
- if player is in direction and within sight range(and line of sight):
    - stop moving and give indicator that player was seen
    - charge in direction of player
    - once charge is over turn around 
    - repeat
- if player after certain amount of time is not in range and not in line or site
    - turn around
    - repeat
 
Beed behavior 
- On spawn decend from above camera
- move up and down 
- always face player 
- if player is within range then interval shooting
    - (optional- if player is super close shoot not so far)

Sead behavior
- spawn in river
- move back and forth horizontally (width of area)
- when pass player for the x time is up stop moving
    - after brief pause, shoot out of water 
    - gravity takes them back down
    - repeat

Leed Behavior
- does not move. 
- sends out multiple gravity-less spikey projectiles

Need Behavior
- spawn on ground
- hop to player

Farmer Pead mini-boss behavior


 */