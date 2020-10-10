using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCorpseDrop : MonoBehaviour
{
    public float speed = 3;
    public LayerMask groundMask;

    private bool isMoving = true;

    private void Update()
    {
        if (!isMoving) { return; }
        gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundMask);
        if(hit.collider != null)
        {
            isMoving = false;
        }
    }
}
