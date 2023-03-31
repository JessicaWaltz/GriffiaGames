using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{
    public int spawnAmount;
    public GameObject[] spawnWho;
    public float[] spawnWhereX;
    public float[] spawnWhereY;
    public float[] spawnDelay;
    public float[] health;

    //bead points
    public float[] range;
    public float[] speed;
    public bool[] goingPositiveDirection;
    public bool[] decendComplete;
    public float[] decendAmount;
    public float[] shootInterval;

    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach (int amount in numberOfEachTypeOfEnemyFront) {
            sizeOfFront += amount;
        }
        foreach (int amount in numberOfEachTypeOfEnemyBack)
        {
            sizeOfBack += amount;
        }
        leftToSpawn = sizeOfFront+sizeOfBack;*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isTriggered != true)
        {
            isTriggered = true;
            for (int i = 0; i < spawnAmount; i++)
            {
                if (spawnWho[i].GetComponent<beedCopter>() != null)
                {
                    StartCoroutine(SpawnBeed(spawnWho[i], spawnWhereX[i], spawnWhereY[i], spawnDelay[i], health[i], range[i], speed[i], goingPositiveDirection[i], decendComplete[i], decendAmount[i], shootInterval[i]));
                }
                else if (spawnWho[i].GetComponent<PeadBuggy>() != null)
                {
                    StartCoroutine(SpawnPead(spawnWho[i], spawnWhereX[i], spawnWhereY[i], spawnDelay[i], health[i], range[i], speed[i], goingPositiveDirection[i]));
                }
                else if (spawnWho[i].GetComponent<SeadSurfer>() != null)
                {
                    StartCoroutine(SpawnSead(spawnWho[i], spawnWhereX[i], spawnWhereY[i], spawnDelay[i], health[i], range[i], speed[i], goingPositiveDirection[i]));
                }
                //else if (spawnWho[i].GetComponent<Leed>() != null)
                //{

                //}
                //else if (spawnWho[i].GetComponent<Need>() != null)
                //{

                //}
            }
        }
    }
    IEnumerator SpawnPead(GameObject spawning, float locationX, float locationY, float delayTime, float health, float range, float speed, bool goingRight) 
    {
        yield return new WaitForSeconds(delayTime);

        Vector3 position = new Vector3(locationX, locationY, 0);
        Quaternion rotation = new Quaternion();

        spawning.GetComponent<PeadBuggy>().health = health;
        spawning.GetComponent<PeadBuggy>().speed = speed;
        spawning.GetComponent<PeadBuggy>().lineOfSiteRangeX = range;
        spawning.GetComponent<PeadBuggy>().facingLeft = !goingRight;

        Instantiate(spawning, position, rotation);

    }
    IEnumerator SpawnBeed(GameObject spawning, float locationX, float locationY,
        float delayTime, float health, float range, float speed, bool goingUp, bool decendComplete,
        float decendAmount, float shootInterval)
    {
        yield return new WaitForSeconds(delayTime);

        Vector3 position = new Vector3(locationX, locationY, 0);
        Quaternion rotation = new Quaternion();

        spawning.GetComponent<beedCopter>().health = health;
        spawning.GetComponent<beedCopter>().range = range;
        spawning.GetComponent<beedCopter>().speed = speed;
        spawning.GetComponent<beedCopter>().goingUp = goingUp;
        spawning.GetComponent<beedCopter>().decendComplete = decendComplete;
        spawning.GetComponent<beedCopter>().decendAmount = decendAmount;
        spawning.GetComponent<beedCopter>().shootInterval = shootInterval;

        Instantiate(spawning, position, rotation);
    }
    IEnumerator SpawnSead(GameObject spawning, float locationX, float locationY, float delayTime, float health, float range, float speed, bool goingRight) {
        yield return new WaitForSeconds(delayTime);

        Vector3 position = new Vector3(locationX, locationY, 0);
        Quaternion rotation = new Quaternion();

        spawning.GetComponent<SeadSurfer>().health = health;
        spawning.GetComponent<SeadSurfer>().speed = speed;
        spawning.GetComponent<SeadSurfer>().lineOfSiteRangeX = range;
        spawning.GetComponent<SeadSurfer>().facingLeft = !goingRight;

        Instantiate(spawning, position, rotation);
    }
}
/*
 Boss ideas

- chrono tbd floating chrono with chrono shields around it circling that come off 
  and fall from ceiling to ground, shields have health but main body  
  health must be 0 to defeat.
    - RotateAround()
    - Vector3.back or Vector3.forward
    - Vector3 point = new Vector3(5,0,0);
    - Vector3 axis = new Vector3(0,0,1);
    - transform.RotateAround(point, axis, Time.deltaTime * 10);
- Pead Buggy big with drill 
- spikey lead ball that charges and is invulnerable when balled up and charging
- sead kracken 
 
 
 
 
 
 */