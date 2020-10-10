using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnGround : MonoBehaviour
{
    public LayerMask groundMask;
    private void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, 0.5f, groundMask))
        {
            gameObject.SetActive(false);
        }
    }
}
