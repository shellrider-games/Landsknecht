using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform[] bars;
    public float maxWidth;
    void Start()
    {
        Debug.Log(bars);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHPbar(float percentage)
    {
        foreach (var bar in bars)
        {
            bar.sizeDelta = new Vector2(maxWidth * percentage, bar.sizeDelta.y);
        }
    }
    
}
