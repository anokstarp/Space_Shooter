using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    public float AtkDelay;
    public float followSpeed;
    public float bulletForce;

    private float atkTimer;
    private int atkLevel = 1;

    public GameObject EngineEffect;
    public Projectile ProjectilePrefab;

    public GameObject LeftGun;
    public GameObject RightGun;
    public GameObject CentralGun;

    private Quaternion Left = Quaternion.Euler(0, 0, 10f);
    private Quaternion Right = Quaternion.Euler(0, 0, -10f);

    private ParticleSystem LeftGunEffect;
    private ParticleSystem RightGunEffect;
    private ParticleSystem CentralGunEffect;

    private BoxCollider2D bc2d;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        LeftGunEffect = LeftGun.gameObject.GetComponent<ParticleSystem>();  
        RightGunEffect = RightGun.gameObject.GetComponent<ParticleSystem>();
        CentralGunEffect = CentralGun.gameObject.GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        FollowMouse();
    }

    private void Start()
    {
        atkTimer = AtkDelay;
    }

    private void Update()
    {
        Attack();

        if(Input.GetKeyDown(KeyCode.PageUp))
        {
            atkLevel++;
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            atkLevel--;
        }
    }

    private void Attack()
    {
        atkTimer -= Time.deltaTime;
        if (atkTimer > 0) return;
        atkTimer = AtkDelay;

        switch (atkLevel)
        {
            case 1:
                Shot(CentralGunEffect, CentralGun, Quaternion.identity);
                break;
            case 2:
                Shot(LeftGunEffect, LeftGun, Quaternion.identity);
                Shot(RightGunEffect, RightGun, Quaternion.identity);
                break;
            case 3:
                Shot(CentralGunEffect, CentralGun, Quaternion.identity);
                Shot(LeftGunEffect, LeftGun, Left);
                Shot(RightGunEffect, RightGun, Right);
                break;
            case 4:
                Shot(CentralGunEffect, CentralGun, Quaternion.identity);
                Shot(LeftGunEffect, LeftGun, Left);
                Shot(RightGunEffect, RightGun, Right);
                Shot(LeftGunEffect, LeftGun, Left * Left);
                Shot(RightGunEffect, RightGun, Right * Right);
                break;
            default:
                break;
        }
    }

    private void Shot(ParticleSystem effect, GameObject position, Quaternion rotate)
    {
        var projectile = Instantiate(ProjectilePrefab, position.transform.position, rotate);
        projectile.ShotProjectile(projectile.transform.up, bulletForce);

        effect.Stop();
        effect.Play();
    }
    
    public void UpgradeAtk()
    {
        atkLevel++;
        if (atkLevel >= 4) atkLevel = 4;
    }

    private void FollowMouse()
    {
        if (!Input.GetMouseButton(0)) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
  
        Vector3 position = rb2d.position;

        rb2d.transform.position = Vector3.Lerp(position, mousePos, Time.deltaTime * followSpeed);
    }
}
