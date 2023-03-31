using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeedSpikeBotBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private bool isPlayerInRange = false;
    private float currentState = 1;

    public float playerRange = 150f; // how far the enemy can detect the player
    public GameObject projectileObject;
    public GameObject poof;
    private bool facingLeft = true;
    public float shootInterval = 1f;

    public float health = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInRange = Vector2.Distance(transform.position, playerTransform.position) < playerRange ? true : false;

        if (isPlayerInRange && currentState == 1)//notice player
        {
            currentState = 2;
            Invoke("TimerToThree", 1f);
        }
        else if (currentState == 3)//fire
        {
            LaunchProjectiles();
            Invoke("TimerToFive", shootInterval);
            currentState = 4;
        }
        else if (currentState == 5) {
            currentState = isPlayerInRange ? 3 : 1;
        }
    }
    void TimerToThree() {
        currentState = 3;
    }
    void TimerToFive() {
        currentState = 5;
    }
    void LaunchProjectiles()
    {
        float startX = spriteRenderer.sprite.bounds.size.x*0.55f;
        float startY = spriteRenderer.sprite.bounds.size.y*0.55f; 
        float speedX = 100;
        float speedY = 100;
        GameObject[] projectiles = new GameObject[5];
        projectiles[0] = Instantiate(projectileObject, new Vector3(transform.position.x + startX*1.2f, transform.position.y , transform.position.z), Quaternion.Euler(0, 0, -90));
        projectiles[1] = Instantiate(projectileObject, new Vector3(transform.position.x - startX*1.2f, transform.position.y , transform.position.z), Quaternion.Euler(0, 0, 90));
        projectiles[2] = Instantiate(projectileObject, new Vector3(transform.position.x, transform.position.y + startY*1.2f, transform.position.z), transform.rotation);
        projectiles[3] = Instantiate(projectileObject, new Vector3(transform.position.x + startX, transform.position.y + startY, transform.position.z), Quaternion.Euler(0, 0, -45));
        projectiles[4] = Instantiate(projectileObject, new Vector3(transform.position.x - startX, transform.position.y + startY, transform.position.z), Quaternion.Euler(0, 0, 45));

        // Set the velocity of each projectile
        for (int i = 0; i < projectiles.Length; i++)
        {
            Vector2 direction = Vector2.zero;

            // Determine the direction of the projectile
            switch (i)
            {
                case 0:
                    direction = Vector2.right;
                    break;
                case 1:
                    direction =  Vector2.left;
                    break;
                case 2: // up
                    direction = Vector2.up;
                    break;
                case 3: // diagonal up and to the right
                    direction = Vector2.right + Vector2.up;
                    break;
                case 4: // diagonal up and to the left
                    direction =  Vector2.left + Vector2.up;
                    break;
            }
            projectiles[i].GetComponent<projectileLogic>().velocityY = speedY * direction.y;
            projectiles[i].GetComponent<projectileLogic>().velocityX = speedX * direction.x;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
