using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject deathAnimation;
    
    public int hitPoints;
    
    public float speed;
    public float area;
    public Transform[] patrolPoints;
    public float waitTime;

    private int targetNumber;
    private Rigidbody2D _rigidbody;
    private bool chasing;
    // Start is called before the first frame update
    void Start()
    {
        this.targetNumber = 0;
        this.chasing = true;
        this._rigidbody = GetComponent<Rigidbody2D>();
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xDistance = this.patrolPoints[targetNumber].position.x - this.transform.position.x;
        if (xDistance > 0) GetComponent<SpriteRenderer>().flipX = true;
        if (xDistance < 0) GetComponent<SpriteRenderer>().flipX = false;
        if (this.chasing)
        {
            if (Mathf.Abs(xDistance) >= this.area)
            {
                Vector2 directionVector = new Vector2(xDistance, 0).normalized;
                _rigidbody.velocity =
                    new Vector2(directionVector.x * this.speed, _rigidbody.velocity.y);
                GetComponent<Animator>().SetBool("moving", true);
            }
            else
            {
                this.chasing = false;
                this.targetNumber = (this.targetNumber + 1) % patrolPoints.Length;
                StartCoroutine(WaitForChase());
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("moving", false);
        }
    }

    private IEnumerator WaitForChase()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        yield return new WaitForSeconds(this.waitTime);
        this.chasing = true;
    }
    
    public void TakeHit()
    {
        if (--hitPoints <= 0)
        {
            ParticleSystem ps = Instantiate(deathAnimation, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            var sh = ps.shape;
            var em = ps.emission;
            em.rateOverTime = new ParticleSystem.MinMaxCurve(600f, 600f);
            sh.scale = new Vector3(1f, 1f, 1f);
            sh.position = sh.position + new Vector3(0f, -1.2f, 0f);
            Destroy(gameObject);
        }
    }
}
