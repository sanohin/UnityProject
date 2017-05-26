using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rabbit : MonoBehaviour
{

    public float speed = 1;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    private bool dead = false;
    private bool big = false;
    public bool Big
    {
        get
        {
            return big;
        }
        protected set
        {
            big = value;
        }
    }
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    private Transform heroParent = null;
    private float protectionTime = 0;
    public bool IsProtected
    {
        get
        {
            return protectionTime > 0;
        }
    }
    Rigidbody2D body = null;
    SpriteRenderer sr = null;
    Animator animator = null;

    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        LevelController.current.setStartPosition(transform.position);
        this.heroParent = this.transform.parent;
    }

    void Update()
    {
        if (!this.dead)
        {
            UpdateMovement();
        }
        animator.SetBool("dead", this.dead);
    }

    private void UpdateMovement()
    {
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0.0)
        {
            Vector2 vel = body.velocity;
            vel.x = value * speed;
            body.velocity = vel;
        }
        animator.SetFloat("speed", Mathf.Abs(value) * speed);
        sr.flipX = value < 0;
    }

    void FixedUpdate()
    {
        if (!this.dead)
        {
            UpdateProtecion();
            UpdatePosition();
            UpdateJump();
        }
    }
    private void UpdatePosition()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            isGrounded = true;
            if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            isGrounded = false;
            SetNewParent(this.transform, this.heroParent);
        }
    }
    private void UpdateJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
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
        animator.SetBool("grounded", this.isGrounded);
    }

    public void Grow()
    {
        if (!this.Big)
        {
            this.transform.localScale += new Vector3(0.5f, 0.5f);
            this.Big = true;
        }
    }

    public void ShrinkSize()
    {
        if (this.Big)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.Big = false;
        }
    }

    public void SetProtection(float time = 4)
    {
        this.protectionTime = 4;
    }

    public void Die()
    {
        this.dead = true;
        Invoke("OnDeath", 1);
    }

    private void OnDeath()
    {
        LevelController.current.onRabbitDeath(this);
    }

    public void Reborn()
    {
        this.dead = false;
    }

    private void UpdateProtecion()
    {
        if (protectionTime > 0)
        {

            sr.color = Color.red;
            protectionTime -= Time.deltaTime;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    static void SetNewParent(Transform obj, Transform newParent)
    {
        if (obj.transform.parent != newParent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = newParent;
            obj.transform.position = pos;
        }
    }
}
