using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    private bool goOut = false;
    public float timer;
    public float timerRandomArea;
    private float timerCounter = 0;
    private float maxTime;
    private float maxRadius;

    public Light2D candleLight;
    void Start()
    {
        maxRadius = candleLight.pointLightInnerRadius;
    }

   
    void Update()
    {
        if (timerCounter <= 0)
        {
            timerCounter = Random.Range(timer - timerRandomArea, timer + timerRandomArea);
            maxTime = timerCounter;
            goOut = !goOut;
        }
        else
        {
            timerCounter -= Time.deltaTime;
            if (goOut)
            {
                candleLight.pointLightInnerRadius = Mathf.Lerp(maxRadius, 0, (maxTime - timerCounter) / maxTime);
            }
            else
            {
                candleLight.pointLightInnerRadius = Mathf.Lerp(0, maxRadius, (maxTime - timerCounter) / maxTime);
            }
        }
    }
    
}
