using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround; 
    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float jumpForce = 7f;
    private float dirX = 0f;

    private enum MovementState { idle, run, jump, fall, hit}

    [SerializeField] private AudioSource jumpSoundEffect;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        anim= GetComponent<Animator>();
        sprite= GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimation();
    }
    void UpdateAnimation()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.run;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.run;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jump;
        }
        if (rb.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }
        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
