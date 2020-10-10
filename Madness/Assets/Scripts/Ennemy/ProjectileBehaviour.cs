using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private float remainingLifetime;

    public void SetDestination(Vector2 destination)
    {
        var dir = destination - (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

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
        if(remainingLifetime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Move()
    {
        gameObject.transform.position += gameObject.transform.right * speed * Time.deltaTime;
    }
}
