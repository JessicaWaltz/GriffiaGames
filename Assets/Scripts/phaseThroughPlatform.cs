using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseThroughPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private bool playerLadderStatus;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) { 
       }
    }
}
