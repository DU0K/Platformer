using System;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    private TMP_Text tmp;
    private float timer;


    public static string timerSeconds;
    public static float timerMinutes;

    public static bool TimerLoop = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerLoop)
        {
            timer += Time.deltaTime;
            timerSeconds = timer.ToString("00.00").Replace(',', ':');

            if (timer >= 60)
            {
                timer = 0;
                timerMinutes++;
            }

            tmp.text = ($"{timerMinutes:00}:{timerSeconds}");
        }
    }
}
