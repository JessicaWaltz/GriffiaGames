using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraYLockTrigger : MonoBehaviour
{
    public float yLockOn;
    public GameObject MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MainCamera.GetComponent<CameraFollow>().yIsLocked = true;
            MainCamera.GetComponent<CameraFollow>().yIsLockedAt = yLockOn;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MainCamera.GetComponent<CameraFollow>().yIsLocked = false;
        }
    }
}
