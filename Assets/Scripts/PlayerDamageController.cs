using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //AudioPlayerSoft(hit);
            Debug.Log("IVE BEEN HIT!!");//takeDamage = true;
            //isSpecialJump = true;
            //xLocationEnemy = collision.gameObject.GetComponent<Rigidbody2D>().transform.position.x;
            //xLocationPlayer = transform.position.x;
        }
        else if (collision.gameObject.tag == "projectile" && collision.gameObject.name.Contains("Bullet"))
        {
            Debug.Log("IVE BEEN HIT!!");
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
