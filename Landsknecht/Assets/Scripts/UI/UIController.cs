using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider hpbar;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHPValue(int hp)
    {
        hpbar.value = hp;
    }

    public void SetMaxHealth(int maxHealth)
    {
        hpbar.maxValue = maxHealth;
        hpbar.value = maxHealth;
    }
    
}
