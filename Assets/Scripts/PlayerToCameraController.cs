using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToCameraController : MonoBehaviour
{
    public GameObject gameCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "endScroll")
        {
            gameCamera.GetComponent<CameraFollow>().inEndArea = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "endScroll")
        {
            gameCamera.GetComponent<CameraFollow>().inEndArea = false;
        }
    }

}
