using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class ClockworkController : MonoBehaviour
{
    public float speed = 1f;
    public float moveTimer = 1f;
    public int damage = 1;

    public int currentHp;
    public int maxHp = 3;

    private float xSpeed = 1;

    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D co2d;

    private Vector2 lookDirection = new Vector2(1, 0);
    private Vector2 direction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        co2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    private void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position += direction * speed * Time.deltaTime;
        rb2d.MovePosition(position);
    }

    private void Update()
    {
        //var h = Input.GetAxis("Horizontal");
        //var v = Input.GetAxis("Vertical");
        TurnLeftRight();

        direction = new Vector3(xSpeed, 0);
        var directionMag = direction.magnitude;

        if (directionMag > 1)
        {
            direction.Normalize();
        }
        if (directionMag > 0)
        {
            lookDirection = direction;
        }

        animator.SetFloat("Speed", directionMag);
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (animator.GetBool("Fix")) return;

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<RubyController>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);

        if (currentHp <= 0)
        {
            animator.SetBool("Fix", true);
            xSpeed = 0f;
            co2d.isTrigger = true;
        }
    }

    private void TurnLeftRight()
    {
        if (animator.GetBool("Fix")) return;

        moveTimer -= Time.deltaTime;

        if (moveTimer < 0)
        {
            moveTimer = 2f;
            xSpeed = -xSpeed;

        }
    }
}
