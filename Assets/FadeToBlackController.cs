using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlackController : MonoBehaviour
{
    public GameObject BlackoutSquare;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(FadeBlackoutSquare());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeBlackoutSquare(false));
        }
    }

    public IEnumerator FadeBlackoutSquare(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        Image image = BlackoutSquare.GetComponent<Image>();
        Color objectColor = image.color;
        float targetAlpha = fadeToBlack ? 1.0f : 0.0f;

        if (fadeToBlack)
        {
            while (objectColor.a < targetAlpha)
            {
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, objectColor.a + (fadeSpeed * Time.deltaTime));
                image.color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (objectColor.a > targetAlpha)
            {
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, objectColor.a - (fadeSpeed * Time.deltaTime));
                image.color = objectColor;
                yield return null;
            }
        }
    }
}
