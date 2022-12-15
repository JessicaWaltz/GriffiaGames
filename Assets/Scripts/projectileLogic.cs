using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocityX;
    public float velocityY;
    public GameObject starPoof;

    public GameObject[] AllEnds;
    public GameObject[] AllEnemies;

    private Rigidbody2D rb;
    public bool isEnemyProjectile = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velocityX, velocityY);
        AllEnds = GameObject.FindGameObjectsWithTag("endScroll");
        AllEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; AllEnds.Length >i ; i++){
            Physics2D.IgnoreCollision(AllEnds[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (isEnemyProjectile) 
        {
            for (int i = 0; AllEnemies.Length > i; i++)
            {
                Physics2D.IgnoreCollision(AllEnemies[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.rotation = rb.rotation - 10;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "endScroll")
        {
            Instantiate(starPoof, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "trigger" && collision.gameObject.tag != "endScroll")
        {
            Instantiate(starPoof, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
