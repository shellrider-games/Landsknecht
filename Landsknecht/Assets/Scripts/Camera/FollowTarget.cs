using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public Transform staticBackground;

    public Transform parallaxFar;

    public Transform parallaxMid;

    public Transform parallaxNear;

    public float farParallaxFactor, midParallaxFactor, nearParallaxFactor;
    public float minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x + 6, Mathf.Clamp(minY, target.position.y , maxY), -10);
        staticBackground.transform.position = new Vector3(transform.position.x, transform.position.y);
        parallaxFar.position = new Vector3(transform.position.x * farParallaxFactor, 0,0);
        parallaxMid.position = new Vector3(transform.position.x * midParallaxFactor, 0,0);
        parallaxNear.position = new Vector3(transform.position.x * nearParallaxFactor, 0,0);
    }
}
