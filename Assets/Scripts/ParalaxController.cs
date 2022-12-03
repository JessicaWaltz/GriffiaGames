using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    //list of all backgrounds to be paralaxed
    public Transform[] backgrounds;
    public float[] ySpeeds;
    // this storres all the parallax scales aka how fast they move based on camera
    //in tut he is using z axis, we are in 2d so we cant do that? instead go based 
    //off of name of game eobject nvm we can do that
    private float[] parallaxScales;
    private float[] parallaxScalesY;
    private float[] parallaxStartingY;
    // how smooth the parallax is going to be, (above 0)
    public float smoothing = 1f;

    // ref to the main game cameras transform
    private Transform myCamera;
    //stores camera position in previous frame
    private Vector2 previousCameraPosition;
    private float startingCameraPositionY;
    //called before start, great for references
    void Awake() {
        //set up the reference for camera
        myCamera = Camera.main.transform;

    }
    // Start is called before the first frame update
    void Start()
    {
        // the previous frame had the current frames camera postition
        previousCameraPosition = myCamera.position;
        startingCameraPositionY = myCamera.position.y;

        parallaxScales = new float[backgrounds.Length];
        parallaxScalesY = new float[backgrounds.Length];
        parallaxStartingY = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z*-1;
            parallaxScalesY[i] = ySpeeds[i];
            parallaxStartingY[i] = backgrounds[i].position.y;
            //Debug.Log(backgrounds[i].position.z * -1);
        }

    }
 

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if (!GameObject.Find("Main Camera").GetComponent<CameraFollow>().inCutscene)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                //the parallax is the opposite of the camera movement because the previous frame 
                //multiplied by the scale
                float parallax = (previousCameraPosition.x - myCamera.position.x) * parallaxScales[i];
                float parallaxY = ((previousCameraPosition.y - myCamera.position.y) * (-parallaxScalesY[i]));
                //set a trget x postion which is the current position + paralax
                float backgroundTargetPositionX = backgrounds[i].position.x + parallax;
                float backgroundTargetPositionY = backgrounds[i].position.y + parallaxY;
                //Debug.Log("CameraY is: " + myCamera.position.y + " starting Y position was " + startingCameraPositionY);
                if (myCamera.position.y == startingCameraPositionY)
                {
                    backgroundTargetPositionY = parallaxStartingY[i];
                }

                //create a target position which is the backgrounds current position 
                //Vector2 backgroundTargetPosition = new Vector2(backgroundTargetPositionX, backgrounds[i].position.y);
                Vector2 backgroundTargetPosition = new Vector2(backgroundTargetPositionX, backgroundTargetPositionY);

                //fade between current position and the target position using lerp
                backgrounds[i].position = Vector2.Lerp(backgrounds[i].position, backgroundTargetPosition, smoothing);


            }
            //set prevcameraposition to the cameras position at the end of the frame
            
        }
        previousCameraPosition = myCamera.position;
    }
}
