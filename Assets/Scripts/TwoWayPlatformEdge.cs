using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatformEdge : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isTouching = false;
    private Collider2D[] m_ObjectCollider;
    private bool[] wasTrigger;
    public bool ignoreEnemy = false;
    void Start()
    {
        m_ObjectCollider = gameObject.GetComponents<EdgeCollider2D>();
        if (ignoreEnemy == true) {
            //Debug.LogWarning("We wish to ignore");
            int enemyLayer = LayerMask.NameToLayer("Enemy");

            // Disable collision between this GameObject's layer and the "Enemy" layer
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);

            // Ignore collisions with GameObjects that have the "enemy" tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(), true);
                //Debug.LogWarning("Ignoring Something");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Collider2D ObjectCollider in m_ObjectCollider) { 
            if (isTouching && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !ObjectCollider.isTrigger)
            {
                ObjectCollider.isTrigger = true;
            }
            else if (!isTouching && ObjectCollider.isTrigger)
            {
                ObjectCollider.isTrigger = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;
        }
    }
}
