using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedHopperController : MonoBehaviour
{
    public float hopForce = 50f; // how high the enemy hops
    public float hopInterval = 2f; // how often the enemy hops
    public float hopRange = 30f; // how far the enemy hops
    public float playerRange = 150f; // how far the enemy can detect the player
    public float playerHopForce = 100f; // how hard the enemy hops towards the player

    private Rigidbody2D rb;
    private Transform playerTransform;
    public GameObject poof;
    private bool isPlayerInRange = false;
    private bool isJumping = true;
    private bool coolDownIsOver = true;
    private bool facingLeft = false;

    private Animator anim;
    private AnimatorClipInfo[] m_CurrentClipInfo;

    public float health = 3;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("HopRandomly", 0f, hopInterval);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        facingLeft = !facingLeft;
    }
    void HopRandomly()
    {
        if (!isJumping && !isPlayerInRange)
        {
            int direction = Random.Range(0, 2) == 0 ? -1 : 1; // choose a random direction
            if ((direction > 0 && !facingLeft) ||(direction < 0 && facingLeft)) {//going right
                Flip();
            }
            Vector2 hopVector = new Vector2(direction * hopRange, hopForce); // create the hop vector
            rb.AddForce(hopVector, ForceMode2D.Impulse); // apply the hop force
            isJumping = true;
        }
    }
    void HopCoolDown()
    {
        coolDownIsOver = true;
    }

    void Update()
    {
        // check if the player is in range
        anim.SetBool("onGround", !isJumping);
        if (Vector2.Distance(transform.position, playerTransform.position) < playerRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }

        // if the player is in range, jump towards them
        if (isPlayerInRange && !isJumping && coolDownIsOver)
        {
            //Vector2 playerDirection = ((playerTransform.position - transform.position).normalized);

            // calculate the hop force vector with increased y value for higher jump
            int direction = playerTransform.position.x > transform.position.x ? 1 : -1; // choose a random direction
            if ((direction > 0 && !facingLeft) || (direction < 0 && facingLeft))
            {//going right
                Flip();
            }
            float distanceToPlayer = Mathf.Abs(playerTransform.position.x - transform.position.x);
            Vector2 hopVector = new Vector2(direction * distanceToPlayer, playerHopForce); // create the hop vector
            //Vector2 hopVector = (playerDirection) * hopForce * 1.5f + Vector2.up * playerHopForce;
            rb.AddForce(hopVector, ForceMode2D.Impulse);
            isJumping = true;
            coolDownIsOver = false;
            Invoke("HopCoolDown", 1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            anim.SetBool("onGround", !isJumping);
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
