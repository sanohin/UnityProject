using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    public float speed = 1;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    public Vector3 MoveBy;
    public float dieAnimationTime = 1;
    protected bool isGrounded = false;
    protected bool jumpActive = false;
    protected float jumpTime = 0f;
    protected float timeLeftToDie;
    protected bool isDying = false;
    protected Vector3 pointA, pointB;
    protected Mode mode = Mode.GoToA;
    protected Rigidbody2D body = null;
    protected SpriteRenderer sr = null;
    protected Animator animator = null;
    protected virtual void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        pointA = this.transform.position;
        pointB = this.pointA + MoveBy;
    }
    protected virtual void Update()
    {
        if (!isDying)
        {
            UpdateMove();
            UpdateGrounded();
        }
        else
        {
            UpdateDie();
        }
    }
    protected virtual void UpdateMove()
    {
        float value = this.GetDirection();
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = body.velocity;
            vel.x = value * speed;
            body.velocity = vel;
            animator.SetFloat("speed", speed);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }

        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }
    }
    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }
    protected virtual float GetDirection()
    {
        Vector3 rabbitPosition = Rabbit.lastRabit.transform.position;
        Vector3 orcPosition = this.transform.position;

        switch (mode)
        {
            case Mode.GoToA:
                if (hasArrived(orcPosition, pointA))
                {
                    mode = Mode.GoToB;
                }
                break;
            case Mode.GoToB:
                if (hasArrived(orcPosition, pointB))
                {
                    mode = Mode.GoToA;
                }
                break;
            case Mode.Attack:
                if (orcPosition.x < rabbitPosition.x)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
        }
        switch (mode)
        {
            case Mode.GoToA:
                if (orcPosition.x < pointA.x)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            case Mode.GoToB:
                if (orcPosition.x < pointB.x)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            default:
                return 0;
        }
    }
    protected virtual bool hasArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.5f;
    }
    protected virtual void UpdateGrounded()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    protected virtual void UpdateDie()
    {

        if (isDying)
        {
            timeLeftToDie -= Time.deltaTime;
            if (timeLeftToDie <= 0)
            {
                isDying = false;
                Destroy(this.gameObject);
            }
        }
    }
    public void Die()
    {
        if (isDying)
        {
            return;
        }
        if (this.isGrounded)
        {
            isDying = true;
            animator.SetTrigger("die");
            timeLeftToDie = dieAnimationTime;
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        var heroController = collider.GetComponentInParent<Rabbit>();
        if (heroController != null)
        {
            GameObject rabbit = heroController.gameObject;
            if (rabbit.transform.position.y > this.transform.position.y + 1)
            {
                this.Die();
                heroController.LiftUp();
            }
            else
            {
                this.animator.SetTrigger("attack");
                heroController.Die();
            }
        }
    }
}
