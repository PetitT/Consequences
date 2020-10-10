using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LowTeeGames;

public class CharacterAttack : MonoBehaviour
{
    [Header("Attack Data")]
    public int currentMaxAttacks = 1;
    public float attackCooldown = 5f;

    [Header("Projectile")]
    public GameObject projectile;
    public LayerMask groundLayer;

    CharacterMovement movement;

    private int remainingAttacks;
    private float remainingCooldown;

    private void Awake()
    {
        remainingAttacks = currentMaxAttacks;
        remainingCooldown = attackCooldown;
    }

    private void Update()
    {
        Cooldown();
        GetAttackInput();
    }

    private void GetAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Cooldown()
    {
        if (remainingAttacks < currentMaxAttacks)
        {
            remainingCooldown -= Time.deltaTime;

            if (remainingCooldown <= 0)
            {
                remainingAttacks++;
                remainingCooldown = attackCooldown;
            }
        }
    }

    public void Attack()
    {
        if (remainingAttacks > 0)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.down, Mathf.Infinity, groundLayer);
            if (hit.collider != null)
            {
                Vector3 spawnPos = hit.point;
                GameObject newProjectile = LTPool.Instance.GetItemFromPool(projectile, spawnPos, Quaternion.identity);
                remainingAttacks--;
            }
        }
    }
}
