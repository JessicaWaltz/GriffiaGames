using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathController : MonoBehaviour
{
    public GameObject togglePath;
    public GameObject player;
    private EdgeCollider2D toggleCollider;
    private int pickPath = 1;
    public string direction;
    // Start is called before the first frame update
    void Start()
    {
        //find the colider with trigger = false and set that one to the
        if (togglePath.GetComponent<EdgeCollider2D>().isTrigger == false)
        {
            
            toggleCollider = togglePath.GetComponent<EdgeCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //when they exit trigger we want to make path 1 accessable or inaccessable
    //start with path being inaccessable to player
    //when triger exited toggel accessable
    //  if toggled to inaccessable make 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction = player.GetComponent<StarPlayerController>().facing;
        if (direction == "right") 
        {
            pickPath += 1;
        }
        
        if (collision.gameObject == player && pickPath%2 == 0) {
            toggleCollider.isTrigger = true;
        }
        //check if player
        //set currently in triger = true;
        // check if player was facing right when
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if player
        // set currently in trigger = false
        // set toggle collider isTrigger to false
        if (collision.gameObject == player)
        {
            toggleCollider.isTrigger = false;
        }

    }
}
