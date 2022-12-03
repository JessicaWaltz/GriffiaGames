using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadguyScript : MonoBehaviour
{
    public bool grabZog = false;
    public bool readytofight = false;
    public bool fighting = false;
    GameObject Zog;
    public GameObject[] kidnapDestination;
    private int counter = 0;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Zog = GameObject.Find("Zog");
        speed = GameObject.Find("Main Camera").GetComponent<CameraFollow>().panSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabZog && Time.timeScale != 0) {
            KidnapZog();
        }
    }

    void KidnapZog() {

        Vector3 lastPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(kidnapDestination[counter].transform.position.x, kidnapDestination[counter].transform.position.y, transform.position.z), Time.deltaTime * speed);
        if (lastPosition == transform.position)
        {
            if (counter == 0) 
            {
                gameObject.AddComponent<HingeJoint2D>();
                gameObject.GetComponent<HingeJoint2D>().connectedBody = Zog.GetComponent<Rigidbody2D>();
                gameObject.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
            }
            counter += 1;
            if (counter == 3) {
                grabZog = false;
                GameObject.Find("_CutSceneController").GetComponent<cutsceneController>().sceneEnd = true;
                Destroy(Zog);
                Destroy(gameObject);
            }
        }
        
            
    }
}
