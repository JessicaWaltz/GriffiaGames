using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public Transform player;
    public GameObject GOplayer;
    public string facing = "right";
    public float flickerSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        facing = GOplayer.GetComponent<StarPlayerController>().facing;
        if (facing == "left") transform.position = new Vector3(player.position.x - 16f, player.position.y);
        else transform.position = new Vector3(player.position.x + 16f, player.position.y);

        //light flicker
        //if(gameObject.GetComponent<Light>)

    }
}
