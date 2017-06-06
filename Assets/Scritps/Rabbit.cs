using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public sealed class Rabbit : MonoBehaviour
{
    public float speed = 1;
    public static Rabbit lastRabit = null;
    public bool IsProtected
    {
        get
        {
            return protectionTime > 0;
        }
    }
    public bool Big
    {
        get
        {
            return big;
        }
        private set
        {
            big = value;
        }
    }
    public bool disabled = false;
    public float maxJumpTime = 2f;
    public float jumpSpeed = 2f;
    private bool jumpActive = false;
    private float jumpTime = 0f;
    private bool dead = false;
    private bool big = false;
    private bool isGrounded = false;
    private Transform heroParent = null;
    private float protectionTime = 0;
    private Rigidbody2D body = null;
    private SpriteRenderer sr = null;
    private Animator animator = null;
    void Start()
    {
        AddLastRabbit();
        body = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (LevelController.current != null)
        {
            LevelController.current.setStartPosition(transform.position);
        }
        this.heroParent = this.transform.parent;
    }
    private void AddLastRabbit()
    {
        lastRabit = this;
    }
    void Update()
    {
        if (!this.dead && !disabled)
        {
            UpdateMovement();
            UpdateProtecion();
            UpdatePosition();
            UpdateJump();
            animator.SetBool("dead", this.dead);
        }
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
            this.jumpActive = true;
        }
        if (this.jumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                this.jumpTime += Time.deltaTime;
                if (this.jumpTime < this.maxJumpTime)
                {
                    Vector2 vel = body.velocity;
                    vel.y = jumpSpeed * (1.0f - jumpTime / maxJumpTime); body.velocity = vel;
                }
            }
            else
            {
                this.jumpActive = false;
                this.jumpTime = 0;
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

    public void LiftUp(int y = 5)
    {
        this.body.velocity += new Vector2(0, y);
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
