using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private BoxCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartDestroy()
    {
        Destroy(gameObject);
    }
}
