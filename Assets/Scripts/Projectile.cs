using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    private float destructTime = 3f;
    private float timer;
    private int damage = 1;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        timer = destructTime;
    }

    private void Update()
    {
        Destruct();
    }

    public void ShotProjectile(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void Destruct()
    {
        timer -= Time.deltaTime;
        if(timer < 0 )
        {
            Destroy(gameObject);       
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver); //첫번째 매개변수에 해당하는 메소드를 전부 호출
        }
    }
}
