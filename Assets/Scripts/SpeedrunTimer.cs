using System;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    private TMP_Text tmp;
    private float timer;

    public static float timerSeconds;
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
            timerSeconds = Mathf.RoundToInt(timer % 60);
            timerMinutes = Mathf.RoundToInt(timerSeconds / 60);
            tmp.text = timerMinutes + ":" + timerSeconds;
            Debug.Log(timerSeconds);
        }
    }
}
