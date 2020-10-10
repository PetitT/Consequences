using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectileMove : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private float remainingLifetime;

    private void OnEnable()
    {
        remainingLifetime = lifeTime;
    }

    private void Update()
    {
        Move();
        Lifetime();
    }

    private void Lifetime()
    {
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Move()
    {
        gameObject.transform.position += gameObject.transform.right * speed * Time.deltaTime;
    }
}
