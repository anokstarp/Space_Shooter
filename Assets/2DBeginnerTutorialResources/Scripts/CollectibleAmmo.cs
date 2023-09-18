using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAmmo : MonoBehaviour
{
    public int ammo = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<RubyController>().GetAmmo(ammo);
            Destroy(gameObject);
        }
    }
}
