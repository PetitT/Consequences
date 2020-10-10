using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyState currentState = EnemyState.idle;

    public float wanderRange;
    public float securityDistance;

    public float moveSpeed;

    public float aggroRange;
    public Vector2 attackCooldownRange;

    public LayerMask playerLayer;

    private Vector2 wanderRight;
    private Vector2 wanderLeft;
    private Vector2 targetPos;

    private void Awake()
    {
        wanderRight = new Vector2(transform.position.x + wanderRange, transform.position.y);
        wanderLeft = new Vector2(transform.position.x - wanderRange, transform.position.y);
        targetPos = wanderRight;
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        switch (currentState)
        {
            case EnemyState.idle:
                CheckAggro();
                break;
            case EnemyState.wander:
                CheckAggro();
                Wander();
                break;
            case EnemyState.attack:
                break;
            default:
                break;
        }
    }

    private void CheckAggro()
    {
        if (Physics2D.OverlapCircle(transform.position, aggroRange, playerLayer))
        {
            currentState = EnemyState.attack;
        }
    }

    private void Wander()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if(targetPos == wanderRight)
        {
            if(transform.position.x >= wanderRight.x - securityDistance)
            {
                targetPos = wanderLeft;
            }
        }
        else
        {
            if(transform.position.x <= wanderLeft.x + securityDistance)
            {
                targetPos = wanderRight;
            }
        }
    }
}
