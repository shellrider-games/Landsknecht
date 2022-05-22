using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public LayerMask _PlayerLayerMask;
    
    public int hitPoints;
    public float detectDistance;
    public float movementSpeed;

    public GameObject deathAnimation;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private bool risen;

    private bool chasing;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        risen = false;
        chasing = false;
        Physics2D.IgnoreLayerCollision(7,7);
    }

    // Update is called once per frame
    void Update()
    {
        if (!risen)
        {
            CheckRaise();
        } else
        {
            if (PlayerInLineOfSight() && chasing)
            {
                _rigidbody.velocity = new Vector2(movementSpeed * DirectionFactor(),0);
            }
            else
            {
                if(chasing) _spriteRenderer.flipX = !_spriteRenderer.flipX;
                if (!PlayerInLineOfSight()) Destroy(gameObject);
            }
        }
    }

    private bool PlayerInLineOfSight()
    {
        float directionFactor = 1.0f;
        if (_spriteRenderer.flipX) directionFactor *= -1.0f;
        RaycastHit2D boxCastHit = Physics2D.BoxCast(transform.position, new Vector2(8.0f, 10.0f),
            0, Vector2.right * DirectionFactor(), detectDistance,_PlayerLayerMask.value);
        if (boxCastHit.collider == null)
        {
            return false;
        }
        return true;
    }

    private float DirectionFactor()
    {
        float directionFactor = 1.0f;
        if (_spriteRenderer.flipX) directionFactor *= -1.0f;
        return directionFactor;
    }
    
    private void CheckRaise()
    {
        RaycastHit2D hitLeft =
            Physics2D.Raycast(transform.position, Vector2.left, detectDistance,_PlayerLayerMask);
        RaycastHit2D hitRight =
            Physics2D.Raycast(transform.position, Vector2.right, detectDistance,_PlayerLayerMask);
        if (hitLeft.collider != null)
        {
            _spriteRenderer.flipX = true;
            _animator.SetTrigger("raiseUp");
            risen = true;
            chasing = true;
        }
        if (hitRight.collider != null)
        {
            _animator.SetTrigger("raiseUp");
            risen = true;
            chasing = true;
        }
    }
    
    public void TakeHit()
    {
        if (--hitPoints <= 0)
        {
            ParticleSystem ps = Instantiate(deathAnimation, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            var sh = ps.shape;
            var em = ps.emission;
            em.rateOverTime = new ParticleSystem.MinMaxCurve(600f, 600f);
            sh.scale = new Vector3(1f, 2f, 1f);
            sh.position = sh.position + new Vector3(0f, -0.8f, 0f);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        var position = transform.position;
        Gizmos.DrawRay(position,Vector2.left*detectDistance);
        Gizmos.DrawRay(position, Vector2.right*detectDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(position, Vector2.down*2.0f);
    }
}
