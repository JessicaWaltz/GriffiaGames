using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noticed : MonoBehaviour
{
    private float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGameObject", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,speed);
    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
