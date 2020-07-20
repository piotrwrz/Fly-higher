using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    private float time;
    private bool once = false;
    public GameObject exitBar;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && time <= 2 && once)
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            once = true;
            exitBar.SetActive(true);
        }
        if(once)
        {
            time += Time.deltaTime;
        }
        if(time > 2)
        {
            exitBar.SetActive(false);
            time = 0;
            once = false;
        }
    }

}
