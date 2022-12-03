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
    public bool[] goingUp;
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
                    StartCoroutine(SpawnBeed(spawnWho[i], spawnWhereX[i], spawnWhereY[i], spawnDelay[i], health[i], range[i], speed[i], goingUp[i], decendComplete[i], decendAmount[i], shootInterval[i]));
                }
                //else if (pead) { }
                //else if (sead) { }
                //else if (leed) { }
                //else if (need) { }
            }
        }
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
}
