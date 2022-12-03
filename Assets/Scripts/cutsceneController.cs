using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject BadGuy;
    public GameObject mainCamera;
    public GameObject[] supportingcast;
    public GameObject infobar;
    public int cutsceneNum = 0;
    private int part = 0;
    public GameObject[] movementDestination;
    public int level;
    public string playerName;
    public float counter;

    public bool sceneStart;
    public bool sceneEnd = false;
    void Start()
    {
        if (playerName == "Star")
        {
            if (level == 1)
            {
                //this is stars story level 1 
                //disable character movement 
                player.GetComponent<StarPlayerController>().isCutscene = true;
                //hide infobar
                infobar.SetActive(false);
                //disable camera follow
                mainCamera.GetComponent<CameraFollow>().inCutscene = true;
                sceneStart = true;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerName == "Star" && Time.timeScale != 0)
        {
            if (level == 1)
            {
                if (cutsceneNum == 0) {
                    StarLevel01Opening();
                }
            }
        }
    }

    void StarLevel01Opening()
    {
        //part 0 pan down to scene 
        if (part == 0)
        {
            if (mainCamera.transform.position.y > movementDestination[0].transform.position.y)
            {

                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, movementDestination[0].transform.position.y, -10), Time.deltaTime * mainCamera.GetComponent<CameraFollow>().panSpeed);
            }
            else
            {
                part = 1;
                counter = 0;
            }
        }
        // Part 1 wait 200 counts to enjoy the scene
        else if (part == 1)
        {
            //wait for x amount of time 
            if (counter < 200)
            {
                counter = counter + (mainCamera.GetComponent<CameraFollow>().panSpeed) * Time.deltaTime;//wait a little while before moving on to bad guy kidnapping
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
            Vector3 aboveScreenPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + (height / 2) + 24, 0);
            BadGuy.transform.position = aboveScreenPosition;
            BadGuy.GetComponent<BadguyScript>().grabZog = true;
            //set badguy into motion
            part = 3;
        }
        else if (part == 3 )
        {
            //waiting for badguy to finish kidnapping 

            if (sceneEnd == true)
            {
                part = 4;
            }
        }
        else if (part == 4  )
        {
            //move to player
            Vector3 lastPosition = mainCamera.transform.position;
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + mainCamera.GetComponent<CameraFollow>().yOffset, mainCamera.transform.position.z), Time.deltaTime * mainCamera.GetComponent<CameraFollow>().panSpeed*2);
            if (lastPosition == mainCamera.transform.position)
            {
                infobar.SetActive(true);
                mainCamera.GetComponent<CameraFollow>().inCutscene = false;
                player.GetComponent<StarPlayerController>().isCutscene = false;
                cutsceneNum += 1;
                part = 0;
                counter = 0;
                //sceneEnd = true;
            }
        }
    }
}
