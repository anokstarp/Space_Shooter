using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
