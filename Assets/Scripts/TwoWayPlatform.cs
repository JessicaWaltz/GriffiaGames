using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isTouching = false;
    public Collider2D m_ObjectCollider;
    public Collider2D m_ObjectColliderTrigger;
    private GameObject thePlayer;
    private bool currentlyIgnoring = false;
    // Add so instead ignore player collision until no longer touching
    // will need to add a trigger and set the collider to ignore player so we can still check the if the player is there

    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !m_ObjectCollider.isTrigger) {
            //m_ObjectCollider.isTrigger = true;
            Physics2D.IgnoreCollision(thePlayer.GetComponent<Collider2D>(), m_ObjectCollider);
            currentlyIgnoring = true;
        }
        else if (!isTouching && currentlyIgnoring == true) {
            //m_ObjectCollider.isTrigger = false;
            Physics2D.IgnoreCollision(thePlayer.GetComponent<Collider2D>(), m_ObjectCollider, false);
            currentlyIgnoring = false;
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
