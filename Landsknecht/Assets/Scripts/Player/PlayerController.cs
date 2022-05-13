using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
         _rigidbody = GetComponent<Rigidbody2D>();
         _animator = GetComponent<Animator>();
         facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), _rigidbody.velocity.y);

        if (_rigidbody.velocity.x > 0) facingRight = true;
        if (_rigidbody.velocity.x < 0) facingRight = false;
        
        _animator.SetBool("facingRight", facingRight);
        _animator.SetBool("walking", Mathf.Abs(_rigidbody.velocity.x) > 0);
    }
}
