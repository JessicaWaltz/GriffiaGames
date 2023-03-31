using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    Text counter;
    public static int coinCounter;

    // Start is called before the first frame update
    void Start()
    {
        counter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = coinCounter.ToString()+"/6";
    }
}
