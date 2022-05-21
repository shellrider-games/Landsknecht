using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMaterialInstance : MonoBehaviour
{
    private Material material;

    public RenderTexture reflectionTexture;
    // Start is called before the first frame update
    void Start()
    {
        material = this.GetComponent<SpriteRenderer>().material;
        material.SetTexture("_ReflectTex", reflectionTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
