using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public Animator animator;
    
    public float speed = 8f;
    public float jumpingPower = 16f;
    private float _horizontal;
    private bool _isFacingRight = true;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Start()
    {
        var health = GetComponent<Health>();
        // add
        health.onDefeat.AddListener(DeathAnimation);
        
        // remove
        health.onDefeat.RemoveListener(DeathAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(_horizontal * speed, rb.velocity.y);

        switch (_isFacingRight)
        {
            case false when _horizontal > 0f:
            case true when _horizontal < 0f:
                Flip();
                break;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void DeathAnimation()
    {
        Destroy(gameObject);
    }
    
    private bool IsGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        return colliders.Any(collider1 => collider1.GetComponent<CanStandOn>());
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        animator.SetBool(IsWalking, true);

        if (context.canceled)
        {
            animator.SetBool(IsWalking, false);
        }
        
        _horizontal = context.ReadValue<Vector2>().x;
    }
    
}