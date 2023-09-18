using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class RubyController : MonoBehaviour
{
    public float speed = 4f;
    private int maxHp = 50;
    private int currentHp;

    public Projectile projectilePrefab;
    private Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private ParticleSystem hitParticle;


    public AudioSource audioSource;
    public AudioClip healingSound;
    public AudioClip hittedSound;

    private Vector2 lookDirection = new Vector2(1, 0);
    private Vector2 direction;

    public float timeInvincible = 1f;
    private bool isInvincible = false;
    private float invincibleTimer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        hitParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position += direction * speed * Time.deltaTime;
        rb2d.MovePosition(position);
    }

    private void Start()
    {
        currentHp = maxHp;
        hitParticle.Stop();
    }

    private void Update()
    {
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0 )
            {
                isInvincible = false;
                spriteRenderer.color = Color.white;
            }
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        direction = new Vector3(h, v);
        var directionMag = direction.magnitude;

        if(directionMag > 1)
        {
            direction.Normalize();
        }
        if(directionMag > 0)
        {
            lookDirection = direction;
        }

        if(Input.GetButtonDown("Fire1")) 
        {
            var lookNormarlized = lookDirection.normalized;

            var pos = rb2d.position;
            pos.y += 0.5f;

            pos += lookNormarlized * 0.7f;

            var projectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
            projectile.Launch(lookNormarlized, 10);

            animator.SetTrigger("Launch");
        }

        animator.SetFloat("Speed", directionMag);
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
    }

    public void TakeDamage(int damage)
    {
        
        //StartCoroutine(Invincible());

        if (isInvincible) return;

        spriteRenderer.color = Color.red;
        animator.SetTrigger("Hit");

        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);
        Debug.Log(currentHp);

        isInvincible = true;
        invincibleTimer = timeInvincible;

        audioSource.PlayOneShot(hittedSound);

        hitParticle.Stop();
        hitParticle.Play();
    }

    public void HealHealth(int health)
    {
        currentHp = Mathf.Clamp(currentHp + health, 0, maxHp);
        Debug.Log(currentHp);

        AudioSource.PlayClipAtPoint(healingSound, transform.position); //위치에 생성해서 소리 내는 전역메소드
    }

    public void GetAmmo(int ammo)
    {
        return;
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTimer);
        isInvincible = false;
    }

}
