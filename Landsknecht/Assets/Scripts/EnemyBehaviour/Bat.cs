using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public int hitPoints;
    public float speed;

    public GameObject deathAnimation;
    
    private Rigidbody2D _rigidbody;
    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            _rigidbody.velocity = new Vector2(-speed, 0);
            if (transform.position.x <= -20) Destroy(gameObject);
        }
    }
    public void TakeHit()
    {
        if (--hitPoints <= 0)
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void StartUP()
    {
        started = true;
    }
}
