using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingCollider : MonoBehaviour
{
    public PolygonCollider2D[] animationColliders;
    public float spriteYsize;
    private GameObject top;
    private GameObject legs;
    // Start is called before the first frame update
    void Start()
    {
         top = gameObject.transform.GetChild(0).gameObject;
         legs = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float x = gameObject.GetComponent<Transform>().position.x;
        float y = gameObject.GetComponent<Transform>().position.y;
        float z = gameObject.GetComponent<Transform>().position.z;
        spriteYsize = legs.GetComponent<SpriteRenderer>().bounds.size.y;
        top.GetComponent<Transform>().position = new Vector3 (x, y+spriteYsize-4, z);
        legs.GetComponent<Transform>().position = new Vector3(x, y, z);
    }
    void setCollider(PolygonCollider2D collider) { }

}
