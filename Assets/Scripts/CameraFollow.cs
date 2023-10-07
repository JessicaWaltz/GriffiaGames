using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isUp;
    private bool isDown;
    private float counter = 0f;
    private float spritebuffer = 12;

    public GameObject player;
    public float panSpeed = 0.5f;
    public float yOffset = 20f;

    public bool inCutscene = true;
    public bool battleTime = false;
    public int cutsceneNum = 0;
    private int part = 0;
    private bool wasBattle = false;
    public GameObject BadGuy;

    public Vector3  myLocation;
    public Vector3 yourLocation;
    private GameObject infobar;
    private GameObject cutsceneLocation0;

    public bool sceneEnd = true;

    public bool inEndArea = false;
    //private float inEndAreaCount = 0f;
    void Start()
    {
        //infobar = GameObject.Find("infoBar");
        //infobar.SetActive(false);
        //cutsceneLocation0 = GameObject.Find("CutsceneLocation0");
    }

    // Update is called once per frame

    void Update()
    {
        if (inCutscene || battleTime)
        {
            if (battleTime)
            {
                //stop camera and put boundries 
                wasBattle = true;
                Vector3 cameraPos = transform.position;
                Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
                float playerLeftx = player.transform.position.x - spritebuffer;
                float playerRightx = player.transform.position.x + spritebuffer;
                Camera cam = Camera.main;
                float height = 2f * cam.orthographicSize;
                float width = height * cam.aspect;
                float cameraLeftx = cameraPos.x - (width / 2);
                float cameraRightx = cameraPos.x + (width / 2);
                if (playerLeftx < cameraLeftx)
                {
                    //transform.position.x = screenPos;
                    //Debug.Log("too far left, move me back");

                    player.transform.position = new Vector3(cameraLeftx + spritebuffer, player.transform.position.y, player.transform.position.z);
                }
                else if (playerRightx > cameraRightx) //we are to the left of the screen
                {
                    //transform.position.x = Screen.width;
                    //Debug.Log("too far right, move me back");
                    player.transform.position = new Vector3(cameraRightx - spritebuffer, player.transform.position.y, player.transform.position.z);
                }

            }
        }
        else if (!battleTime && wasBattle)
        {
            PlayerTransition();
        }
        else if(!inEndArea)
        { 
           FollowPlayer();
        }
        myLocation = transform.position;
        yourLocation = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, transform.position.z);
    }
    void FollowPlayer() {
        if (counter < 0)
        {
            if (counter + panSpeed * Time.deltaTime > 0)
            {
                counter = 0;
            }
            else
            {
                counter = counter + panSpeed * Time.deltaTime;
            }
        }
        else if (counter > 0)
        {
            if (counter - panSpeed * Time.deltaTime < 0)
            {
                counter = 0;
            }
            else
            {
                counter = counter - panSpeed * Time.deltaTime;
            }
        }
        // if players PlayerMovementController has public bool isGround = true; then transform to player.transform.position.y + yOffset + counter
        // else keep current y position
        bool isPlayerOnGround = player.GetComponent<PlayerMovementController>().isGround;
        float newYPosition = transform.position.y;
        if (isPlayerOnGround || player.transform.position.y + yOffset + counter <= transform.position.y)
        {
            newYPosition = Mathf.Lerp(transform.position.y, player.transform.position.y + yOffset + counter, 0.1f);
        } 
        transform.position = new Vector3(player.transform.position.x, newYPosition, -10);
    }
    void StartingCutscene() {
        //part 1 pan down to scene 
        if (part == 0)
        {
            if (transform.position.y > cutsceneLocation0.transform.position.y)
            {
                //counter = counter - (panSpeed / 100) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, cutsceneLocation0.transform.position.y, -10), Time.deltaTime * panSpeed);
            }
            else
            {
                part = 1;
                counter = 0;
            }
        }
        else if (part == 1)
        {
            //wait for x amount of time 
            if (counter < 400)
            {
                counter = counter + (panSpeed) * Time.deltaTime;//wait a little while before moving on to bad guy kidnapping
            }
            else
            {
                part = 2;
                counter = 0;
            }
        }
        else if (part == 2)
        {
            //spawn badguy to kidnap zog
            float height = 2f * Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;
            Vector3 aboveScreenPosition = new Vector3(transform.position.x, transform.position.y + (height / 2)+24, 0);
            BadGuy.transform.position = aboveScreenPosition;
            BadGuy.GetComponent<BadguyScript>().grabZog = true;
            //set badguy into motion
            part = 3;
        }
        else if (part == 3) {
            //waiting for badguy to finish kidnapping 
            
            if (sceneEnd == true) {
                part = 4;
            }
        }
        else if (part == 4)
        {
            //move to player
            Vector3 lastPosition = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + yOffset, transform.position.z), Time.deltaTime * panSpeed);
            if (lastPosition == transform.position)
            {
                infobar.SetActive(true);
                inCutscene = false;
                player.GetComponent<StarPlayerController>().isCutscene = false;
                wasBattle = false;
                cutsceneNum += 1;
                part = 0;
                counter = 0;
            }
        }
    }
    void PlayerTransition() {
        Vector3 lastPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + yOffset, transform.position.z), Time.deltaTime * panSpeed);
        if (lastPosition == transform.position)
        {
            wasBattle = false;
        }
    }
    
}
