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
            collision.SendMessage("UpgradeAtk", SendMessageOptions.DontRequireReceiver); //ù��° �Ű������� �ش��ϴ� �޼ҵ带 ���� ȣ��
        }
    }
}
