using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private BoxCollider2D bc2d;

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.SendMessage("UpgradeAtk", SendMessageOptions.DontRequireReceiver); //첫번째 매개변수에 해당하는 메소드를 전부 호출
        }
    }
}
