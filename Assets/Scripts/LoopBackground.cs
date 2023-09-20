using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    private float height;

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
        var offset = new Vector2(0, height * 2f);
        transform.position = (Vector2)transform.position + offset;
    }
}
