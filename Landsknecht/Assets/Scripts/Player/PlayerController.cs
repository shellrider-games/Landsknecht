using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpStrength;
    public LayerMask groundLayer;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BoxCollider2D _collider;

    private bool facingRight;
    private bool attacking;
    private bool inKnockback;
    
    // Start is called before the first frame update
    void Start()
    {
         _rigidbody = GetComponent<Rigidbody2D>();
         _animator = GetComponent<Animator>();
         _collider = GetComponent <BoxCollider2D>();
         facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(attacking || inKnockback))
        {
            _rigidbody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), _rigidbody.velocity.y);
        
            if (_rigidbody.velocity.x > 0) facingRight = true;
            if (_rigidbody.velocity.x < 0) facingRight = false;

            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpStrength);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                attacking = true;
            }
        }
        else
        {
            if (IsGrounded())
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                inKnockback = false;
            }
        }
        
        
        SetAnimationData();
    }

    private void SetAnimationData()
    {
        _animator.SetBool("facingRight", facingRight);
        _animator.SetBool("walking", Mathf.Abs(_rigidbody.velocity.x) > 0);
        _animator.SetFloat("yVelocity", _rigidbody.velocity.y);
        _animator.SetBool("grounded", IsGrounded());
        _animator.SetBool("attacking", attacking);
    }

    private bool IsGrounded()
    {
        float epsilon = 0.1f;
        RaycastHit2D boxcastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down,
            epsilon, groundLayer);
        return boxcastHit.collider != null;
    }

    private void AttackOver()
    {
        attacking = false;
    }

    

    public void TakeHit()
    {
        Knockback();
    }

    private void Knockback()
    {
        float xValue = 5.0f;
        float yValue = 3.0f;
        if (facingRight)
        {
            xValue *= -1;
        }
        //immediatley shoot up character to avoid is on ground.
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.11f, transform.position.z);
        _rigidbody.velocity = new Vector2(xValue, yValue);
        inKnockback = true;
    }
}
