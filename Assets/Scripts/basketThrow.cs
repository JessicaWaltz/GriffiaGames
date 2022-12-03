using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketThrow : MonoBehaviour
{
    // Start is called before the first frame update
    public bool throwing = false;
    public float yPower;
    public float xPower;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (throwing) {
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0));
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPower, yPower), ForceMode2D.Impulse);
            throwing = false;

        }
    }
}
