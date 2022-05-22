using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skeleton : MonoBehaviour
{
    public GameObject bone;
    private SpriteRenderer _spriteRenderer;

    private bool throwBone = true;

    public float hitPoints;
    public GameObject deathAnimation;
    
    public float xThrowStrength, yThrowStrength, variability;

    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (throwBone)
        {
            StartCoroutine(ThrowBone());
        }
    }

    public IEnumerator ThrowBone()
    {
        throwBone = false;
        if (_spriteRenderer.flipX)
        {
            Bone b = Instantiate(bone, new Vector2(transform.position.x - 0.5f, transform.position.y), Quaternion.identity).GetComponent<Bone>();
            b.SetVelocity(new Vector2(Random.Range(xThrowStrength-variability,xThrowStrength+variability)*-1.0f, Random.Range(yThrowStrength-variability,yThrowStrength+variability)));
        }
        else
        {
            Bone b = Instantiate(bone, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity).GetComponent<Bone>();
            b.SetVelocity(new Vector2(Random.Range(xThrowStrength-variability,xThrowStrength+variability), Random.Range(yThrowStrength-variability,yThrowStrength+variability)));
        }

        yield return new WaitForSeconds(waitTime);
        throwBone = true;
    }

    public void TakeHit()
    {
        if (--hitPoints <= 0)
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
