using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBob : MonoBehaviour
{
    // Start is called before the first frame update
    private float startY;
    private float direction = 1;
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 1) 
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startY - 2, transform.position.z), Time.deltaTime *1);
            if (transform.position.y == startY - 2) 
            {
                direction = 0;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startY, transform.position.z), Time.deltaTime*1);
            if (transform.position.y == startY)
            {
                direction = 1;
            }
        }
        
    }
    //mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + mainCamera.GetComponent<CameraFollow>().yOffset, mainCamera.transform.position.z), Time.deltaTime * mainCamera.GetComponent<CameraFollow>().panSpeed*2);
}
