using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{

    public float speed = 1;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    Rigidbody2D body = null;
    SpriteRenderer sr = null;
    Animator animator = null;

    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        LevelController.current.setStartPosition (transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0.0)
        {
            Vector2 vel = body.velocity;
            vel.x = value * speed;
            body.velocity = vel;
        }

        animator.SetFloat("speed", Mathf.Abs(value) * speed);

        if (value > 0)
        {
            sr.flipX = false;
        }
        else if (value < 0)
        {
            sr.flipX = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f; Vector3 to = transform.position + Vector3.down * 0.1f; int layer_id = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.DrawLine(from, to, Color.red);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }

        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають 
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = body.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime); body.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        Animator animator = GetComponent<Animator>();
        animator.SetBool("grounded", this.isGrounded);
    }
}
