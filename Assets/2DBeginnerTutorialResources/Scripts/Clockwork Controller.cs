using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockworkController : MonoBehaviour
{
    public float speed = 4f;

    private Animator animator;
    private Rigidbody2D rb2d;

    private Vector2 lookDirection = new Vector2(1, 0);
    private Vector2 direction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position += direction * speed * Time.deltaTime;
        rb2d.MovePosition(position);
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        direction = new Vector3(h, v);
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
}
