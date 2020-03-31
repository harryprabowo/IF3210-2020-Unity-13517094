using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody2D rigidbody;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    private bool jumpAtt;
    
    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();

        HandleMovement(horizontal);
        Flip(horizontal);
        

        HandleAttacks();

        HandleLayers();

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if(rigidbody.velocity.y < 0)
        {
            animator.SetBool("land", true);
        }

        if(!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            rigidbody.velocity = new Vector2(horizontal * movementSpeed, rigidbody.velocity.y);
        }

        if(isGrounded && jump)
        {
            isGrounded = false;
            rigidbody.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("jump");
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        if(attack && isGrounded && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack");
            rigidbody.velocity = Vector2.zero;
            ThrowKnife(0);
        }

        if(jumpAtt && !isGrounded && !this.animator.GetCurrentAnimatorStateInfo(1).IsName("JumpAtt"))
        {
            animator.SetBool("jumpAtt", true);
            ThrowKnife(1);
        }

        if(!jumpAtt && !this.animator.GetCurrentAnimatorStateInfo(1).IsName("JumpAtt"))
        {
            animator.SetBool("jumpAtt", false);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            jumpAtt = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

    private void ResetValues()
    {
        attack = false;
        jump = false;
        jumpAtt = false;
    }

    private bool IsGrounded()
    {
        if(rigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; ++i)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        animator.ResetTrigger("jump");
                        animator.SetBool("land", false);
                        return true;
                    }

                }
            }
        }
        
        return false;
    }

    private void HandleLayers()
    {
        if(!isGrounded)
        {
            animator.SetLayerWeight(1, 1);
        } 
        else 
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowKnife(int value)
    {
        if(!isGrounded && value == 1 || isGrounded && value == 0)
        {
            base.ThrowKnife(value);
        }

    }
}
