using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{

    public GameObject projectileObject;

    public void CheckThrow(bool isThrowing,bool isUp,bool isDown,string facing) {
        if (isThrowing)
        {
            StartCoroutine(LaunchProjectile(0.15f, isUp, isDown, facing));

        }
    }
    private IEnumerator LaunchProjectile(float delayTime, bool isUp, bool isDown, string facing) {
        yield return new WaitForSeconds(delayTime);
        float startX = 15;
        float startY;
        float speedX;
        float speedY;

        if (isUp)
        {
            startY = 7;
            speedX = 175;
            speedY = 250;
        }
        else if (isDown)
        {
            startY = -10;
            speedX = 375;
            speedY = 70;
        }
        else
        {
            startY = 7;
            speedX = 275;
            speedY = 150;
        }

        var projectile = facing == "right"
                ? Instantiate(projectileObject, new Vector3(transform.position.x + startX, transform.position.y + startY, transform.position.z), transform.rotation)
                : Instantiate(projectileObject, new Vector3(transform.position.x - startX, transform.position.y + startY, transform.position.z), transform.rotation);
        projectile.GetComponent<projectileLogic>().velocityX = facing == "right"
            ? speedX
            : -speedX;
        projectile.GetComponent<projectileLogic>().velocityY = speedY;
    }
   
}
