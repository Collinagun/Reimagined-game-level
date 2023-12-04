using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

     [Header("Component")]
      public float currentTime;
    public bool countDown;

    [Header("LimitSettings")]
    public bool hasLimit;
    public float timerLimit;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.time: currentTime += Time.deltaTime;
        
        if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0.0");
    }
}
