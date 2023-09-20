using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyProjectile ProjectilePrefab;
    public ParticleSystem HitEffect;
    public ParticleSystem DestructEffect;

    private Rigidbody2D rb2d;

    public int damage;
    public int HealthPoint;
    public int bulletForce;
    public float Speed;

    private float atkTimer;
    private float maxTime = 2.5f;
    private float minTime = 1f;
    private bool atk = false;

    private float moveTime = 2.5f;
    private float timer;

    private Vector3 start;
    private Vector3 end;
    private Vector3 mid;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        HealthPoint = 2;
        atkTimer = Random.Range(minTime, maxTime);
        timer = 0f;
    }

    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    private void Update()
    {
        Attack();

        if(HealthPoint <= 0 && !DestructEffect.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        HealthPoint -= damage;
        if(HealthPoint > 0)
        {
            HitEffect.transform.position = transform.position;
            HitEffect.Stop();
            HitEffect.Play();
        }
        else if(HealthPoint <= 0)
        {
            DestructEffect.transform.position = transform.position;
            DestructEffect.Stop();
            DestructEffect.Play();

            GetComponent<Renderer>().enabled = false;
            gameObject.tag = "Untagged";
            //Destroy(gameObject);  
        }
    }

    private void Attack()
    {
        if (HealthPoint <= 0) return;
        if (atk) return;

        atkTimer -= Time.deltaTime;
        if(atkTimer < 0)
        {
            Shot(gameObject, Quaternion.identity);
            atk = true;
        }
    }

    private void Shot(GameObject position, Quaternion rotate)
    {
        var projectile = Instantiate(ProjectilePrefab, position.transform.position, rotate);
        projectile.ShotProjectile(projectile.transform.up, -bulletForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver); //첫번째 매개변수에 해당하는 메소드를 전부 호출
        }
    }

    private void Move()
    {
        timer += Time.deltaTime;
        float t = timer / moveTime;
        if (t > 1) Destroy(gameObject);

        rb2d.transform.position = Bezier(start, mid, end, t);
    }

    public void SetTarget(Vector3 target, Vector3 middle)
    {
        start = rb2d.position;
        end = target;
        mid = middle;
    }

    public Vector3 Bezier(Vector3 P0, Vector3 P1, Vector3 P2, float t)
    {
        Vector3 M0 = Vector3.Lerp(P0, P1, t);
        Vector3 M1 = Vector3.Lerp(P1, P2, t);

        return Vector3.Lerp(M0, M1, t);
    }
}
