using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isTouching = false;
    private Collider2D m_ObjectCollider;
    
    void Start()
    {
        m_ObjectCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !m_ObjectCollider.isTrigger) {
            m_ObjectCollider.isTrigger = true;
        }
        else if (!isTouching && m_ObjectCollider.isTrigger) {
            m_ObjectCollider.isTrigger = false;
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
