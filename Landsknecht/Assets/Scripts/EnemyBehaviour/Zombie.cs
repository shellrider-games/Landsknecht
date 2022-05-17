using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public LayerMask _PlayerLayerMask;
    
    public int hitPoints;
    public float detectDistance;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    private bool risen;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        risen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!risen)
        {
            CheckRaise();
        }
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
        }
        if (hitRight.collider != null)
        {
            _animator.SetTrigger("raiseUp");
            risen = true;
        }
    }
    
    public void TakeHit()
    {
        if(--hitPoints <= 0) Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position,Vector2.left*detectDistance);
        Gizmos.DrawRay(transform.position, Vector2.right*detectDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.down*2.0f);
    }
}
