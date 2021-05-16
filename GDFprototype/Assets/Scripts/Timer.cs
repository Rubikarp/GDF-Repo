using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float chrono = 0f;
    public bool timeerOn = false;
    public TextMeshProUGUI chrnotext;

    void Update()
    {
        if (timeerOn)
        {
            chrono += Time.deltaTime;
        }

        DisplayTime(chrono);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSec = Mathf.FloorToInt(timeToDisplay * 1000 % 1000);

        chrnotext.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSec);
    }
    public void StopChrono()
    {
        timeerOn = false;
    }
    public void RebootChrono()
    {
        chrono = 0f;
    }
    public void StartChrono()
    {
        timeerOn = true;
    }
}
