using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    damageFlash damageFlashObject;
    PlayerMovementController playerMovementController;
    public HealthController healthContoller;
    private HealthController healthCounter;
    // Start is called before the first frame update
    void Start()
    {
        damageFlashObject = gameObject.GetComponent<damageFlash>();
        playerMovementController = gameObject.GetComponent<PlayerMovementController>();
        healthCounter = FindObjectOfType<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damageFlashObject.checkInvulnerability() == true) { 
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "projectile" && collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !healthCounter.IsDead())
        {
            healthContoller.TakeDamage();
            damageFlashObject.StartDamageAnimation(2f);
            int direction = 1;
            if (collision.gameObject.GetComponent<Rigidbody2D>().position.x > gameObject.GetComponent<Rigidbody2D>().position.x)
            {
                direction = direction * -1;
            }
            playerMovementController.BounceBack(direction);

        }
        else {
            Debug.Log("collision with " + collision.gameObject.tag);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !healthCounter.IsDead())
        {

            healthContoller.TakeDamage();
            damageFlashObject.StartDamageAnimation(2f);
            int direction = 1;
            if (collision.gameObject.GetComponent<Rigidbody2D>().position.x > gameObject.GetComponent<Rigidbody2D>().position.x)
            {
                direction = direction * -1;
            }
            playerMovementController.BounceBack(direction);

        }
    }
}



















/*if (collision.gameObject.GetComponent<EnemyBehavior>().enemyType == "copter")
            {
                //if jumping up and below enemy
                if (rb.velocity.y > 0.01 && gameObject.GetComponent<Transform>().position.y < collision.gameObject.GetComponent<Transform>().position.y)
                {
                    collision.gameObject.GetComponent<EnemyBehavior>().dead = true;
                    rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
                }
                else
                {
                    takeDamage = true;
                    isSpecialJump = true;
                    xLocationEnemy = collision.gameObject.GetComponent<Rigidbody2D>().transform.position.x;
                    xLocationPlayer = transform.position.x;
                }
            }
            else if (collision.gameObject.GetComponent<EnemyBehavior>().enemyType == "buggy")
            {
                if (rb.velocity.y < -0.01)
                {
                    hitEnemy = true;
                    collision.gameObject.GetComponent<EnemyBehavior>().dead = true;
                    rb.velocity = new Vector2(rb.velocity.x, JumpPower * 0.75f);
                    //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower * 0.75f), ForceMode2D.Impulse);
                }
                else
                {
                    takeDamage = true;
                    isSpecialJump = true;
                    xLocationEnemy = collision.gameObject.GetComponent<Rigidbody2D>().transform.position.x;
                    xLocationPlayer = transform.position.x;
                }
            }*/
