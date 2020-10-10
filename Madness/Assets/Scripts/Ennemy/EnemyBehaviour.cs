using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public EnemyState currentState = EnemyState.idle;

    [Header("Wander")]
    public float wanderRange;
    public float securityDistance;
    public Vector2 wanderWaitTime;
    public float moveSpeed;

    [Header("Search")]
    public float aggroRange;
    public LayerMask playerLayer;

    [Header("Attack")]
    public float attackMoveSpeed;
    public float distanceToAttackPlayer;
    public float minDistanceToPlayer;
    public Vector2 attackCooldownRange;
    public float attackWaitTime;

    [Header("Animator")]
    public Animator anim;

    private Vector2 wanderRight;
    private Vector2 wanderLeft;
    private Vector2 targetPos;
    private bool isWandering = true;

    protected Transform playerPos;
    private float currentPlayerDistance;
    private float remainingAttackCooldown;
    private bool isAttacking = false;

    protected bool IsAgressive = true;

    private void Awake()
    {
        wanderRight = new Vector2(transform.position.x + wanderRange, transform.position.y);
        wanderLeft = new Vector2(transform.position.x - wanderRange, transform.position.y);
        targetPos = wanderRight;
        anim.SetBool("IsMoving", true);
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
                CheckPlayerDistance();
                MoveTowardsPlayer();
                AttackCooldown();
                AttackCheck();
                break;
            default:
                break;
        }
    }

    private void CheckAggro()
    {
        if (!IsAgressive) { return; }
        Collider2D col = Physics2D.OverlapCircle(transform.position, aggroRange, playerLayer);
        if (col != null)
        {
            playerPos = col.transform;
            currentState = EnemyState.attack;
            anim.SetBool("IsMoving", true);
        }
    }

    private void Wander()
    {
        if (!isWandering) { return; }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if (targetPos == wanderRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (transform.position.x >= wanderRight.x - securityDistance)
            {
                targetPos = wanderLeft;
                StartCoroutine(WanderPause());
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (transform.position.x <= wanderLeft.x + securityDistance)
            {
                targetPos = wanderRight;
                StartCoroutine(WanderPause());
            }
        }
    }

    private IEnumerator WanderPause()
    {
        anim.SetBool("IsMoving", true);
        isWandering = false;
        yield return new WaitForSeconds(wanderWaitTime.RandomRange());
        isWandering = true;
        anim.SetBool("IsMoving", false);
    }

    private void CheckPlayerDistance()
    {
        currentPlayerDistance = Vector2.Distance(transform.position, new Vector2(playerPos.position.x, transform.position.y));
    }

    private void MoveTowardsPlayer()
    {
        if (isAttacking) { return; }
        if(currentPlayerDistance <= minDistanceToPlayer) { return; }
        Vector2 targetPos = new Vector2(playerPos.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, attackMoveSpeed * Time.deltaTime);
        transform.LookAt(targetPos);
        transform.Rotate(Vector3.up, -90);
    }

    private void AttackCooldown()
    {
        remainingAttackCooldown -= Time.deltaTime;
        remainingAttackCooldown = Mathf.Clamp(remainingAttackCooldown, 0, remainingAttackCooldown);
    }

    private void AttackCheck()
    {
        if (currentPlayerDistance <= distanceToAttackPlayer && remainingAttackCooldown == 0)
        {
            isAttacking = true;
            StartCoroutine(Attack(() => isAttacking = false));
            remainingAttackCooldown = attackCooldownRange.RandomRange();
        }
    }

    protected abstract IEnumerator Attack(Action onFinish);

    public void StartAttacking()
    {
        playerPos = CharPosition.Instance.gameObject.transform;
        currentState = EnemyState.attack;
        anim.SetBool("IsMoving", true);
    }

    private void OnDrawGizmos()
    {
        if (currentState == EnemyState.attack) { return; }
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
