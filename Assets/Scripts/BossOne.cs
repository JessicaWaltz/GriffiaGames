using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    public GameObject orbitPrefab; // prefab for the orbiting objects
    public float orbitRadius = 2f; // radius of the orbit
    public float orbitSpeed = 2f; // speed of the orbit
    public float throwForce = 10f; // force of the thrown objects
    public float attackCooldown = 2f; // cooldown between attacks
    public float throwCooldown = 0.5f; // cooldown between thrown objects
    public int numOrbits = 8; // number of orbiting objects
    public bool isAttacking = false; // flag to indicate if boss is currently attacking
    public bool isThrowing = false; // flag to indicate if boss is currently throwing

    private GameObject[] orbits; // array to store the orbiting objects
    private float nextAttackTime; // time until next attack
    private int throwIndex; // index of the orbiting object to throw
    private Vector3 throwTarget; // target position for thrown object(s)

    void Start()
    {
        
        // create the orbiting objects
        orbits = new GameObject[numOrbits];
        for (int i = 0; i < numOrbits; i++)
        {
            float angle = i * Mathf.PI * 2f / numOrbits;
            Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * orbitRadius;
            GameObject orbit = Instantiate(orbitPrefab, pos, Quaternion.identity);
            orbit.transform.parent = transform;
            orbits[i] = orbit;
        }
    }

    void Update()
    {
        // orbit the boss and its orbiting objects
        if (!isAttacking && !isThrowing)
        {
            transform.position += new Vector3(Mathf.Cos(Time.time * orbitSpeed), Mathf.Sin(Time.time * orbitSpeed), 0f) * orbitRadius;
            for (int i = 0; i < numOrbits; i++)
            {
                float angle = i * Mathf.PI * 2f / numOrbits + Time.time * orbitSpeed;
                Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * orbitRadius;
                orbits[i].transform.position = pos;
            }
        }

        // check if it's time for a new attack
        if (!isAttacking && !isThrowing && Time.time >= nextAttackTime)
        {
            int attackType = Random.Range(0, 2); // randomly choose an attack
            if (attackType == 0) // throw one orbiting object
            {
                throwIndex = Random.Range(0, numOrbits); // randomly choose an object to throw
                GameObject playerObject = GameObject.Find("Player");
                if (playerObject != null)
                {
                    throwTarget = playerObject.transform.position; //throwTarget = PlayerController.Instance.transform.position; // throw at player
                }
                
                StartCoroutine(ThrowOrbit()); // start coroutine for throwing object
            }
            else // throw all orbiting objects
            {
                throwTarget = transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f); // throw at random location
                StartCoroutine(ThrowOrbits()); // start coroutine for throwing objects
            }
            nextAttackTime = Time.time + attackCooldown; // set time for next attack
        }
    }

    IEnumerator ThrowOrbit()
    {
        isAttacking = true;
        isThrowing = true;
        yield return new WaitForSeconds(throwCooldown); // wait before throwing object
        Vector3 throwDirection = (throwTarget - orbits[throwIndex].transform.position);
        throwDirection.z = 0f;
        orbits[throwIndex].GetComponent<Rigidbody2D>().AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
        isThrowing = false;
        yield return new WaitForSeconds(attackCooldown - throwCooldown); // wait for cooldown before resuming orbit
        isAttacking = false;
    }

    IEnumerator ThrowOrbits()
    {
        isAttacking = true;
        isThrowing = true;
        foreach (GameObject orbit in orbits)
        {
            Vector3 throwDirection = (throwTarget - orbit.transform.position);
            throwDirection.z = 0f;
            orbit.GetComponent<Rigidbody2D>().AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(throwCooldown); // wait before throwing next object
        }
        isThrowing = false;
        yield return new WaitForSeconds(attackCooldown - throwCooldown * numOrbits); // wait for cooldown before resuming orbit
        isAttacking = false;
    }
}
