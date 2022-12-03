using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D enemy;

    private Transform eyes;
    private float eyesStartX;
    private float eyesStartY;

    private Transform eyebrows;
    private float eyebrowsStartX;
    private float eyebrowsStartY;

    private Transform mouth;
    private float mouthStartX;
    private float mouthStartY;

    public Animator eyebrowsAnimate;
    public Animator mouthAnimate;
    public float distance = 120f;

    private void Awake()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");


    }
    // Start is called before the first frame update
    void Start()
    {
        var children = enemy.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.name == "eyes")
            {
                eyes = child;
                eyesStartX = eyes.position.x;
                eyesStartY = eyes.position.y;
            }
            else if (child.name == "eyebrows")
            {
                eyebrows = child;
                eyebrowsStartX = eyebrows.position.x;
                eyebrowsStartY = eyebrows.position.y;
            }
            else if (child.name == "mouth")
            {
                mouth = child;
                mouthStartX = mouth.position.x;
                mouthStartY = mouth.position.y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= enemy.transform.position.x - distance && player.transform.position.x < enemy.transform.position.x)
        {
            mouthAnimate.SetBool("isClose", true);
            eyebrowsAnimate.SetBool("isClose", true);
            eyes.position = new Vector3(eyesStartX - 3f, eyesStartY - 2f, eyes.position.z);
            eyebrows.position = new Vector3(eyebrowsStartX - 3f, eyebrowsStartY - 2f, eyebrows.position.z);
            mouth.position = new Vector3(mouthStartX - 3f, mouthStartY - 2f, mouth.position.z);
        }
        else
        {
            mouthAnimate.SetBool("isClose", false);
            eyebrowsAnimate.SetBool("isClose", false);
            eyes.position = new Vector3(eyesStartX, eyesStartY, eyes.position.z);
            eyebrows.position = new Vector3(eyebrowsStartX , eyebrowsStartY , eyebrows.position.z);
            mouth.position = new Vector3(mouthStartX, mouthStartY, mouth.position.z);
        }
    }
}
