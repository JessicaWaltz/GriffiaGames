using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatformEdge : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isTouching = false;
    private Collider2D[] m_ObjectCollider;
    private bool[] wasTrigger;

    void Start()
    {
        m_ObjectCollider = gameObject.GetComponents<EdgeCollider2D>();
        
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
