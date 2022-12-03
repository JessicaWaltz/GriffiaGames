using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputChecker : MonoBehaviour
{
    private bool isEsc = false;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isEsc = Input.GetKeyDown(KeyCode.Escape);
        if (isEsc == true) 
        {
            if (PauseMenu.activeSelf)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
                 
        }
    }
}
