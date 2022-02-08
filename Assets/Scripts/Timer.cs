using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float time;
    public Text timerText;

    void Update()
    {
        time += Time.deltaTime;
        timerText.text = time.ToString("F2");
    }

    public float getTime()
    {
        return time;
    }
}

