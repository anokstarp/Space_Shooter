using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPlanet : MonoBehaviour
{
    private float height = 14f;

    private void Awake()
    {
        var bc2d = GetComponent<BoxCollider2D>();
        height = bc2d.size.y;
    }

    private void Update()
    {
        if (transform.position.y < -height)
        {
            RePosition();
        }
    }

    private void RePosition()
    {
        var offset = new Vector2(0, Random.Range(height, height * 3) * 2f);
        transform.position = (Vector2)transform.position + offset;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Planet"))
        {
            var offset = new Vector2(0, Random.Range(height, height * 3) * 2f);
            transform.position = (Vector2)transform.position + offset;
        }
    }
}
