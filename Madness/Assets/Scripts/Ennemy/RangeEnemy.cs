using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyBehaviour
{
    public GameObject projectile;
    public Transform shotPosition;

    protected override IEnumerator Attack(Action onFinish)
    {
        yield return new WaitForSeconds(attackWaitTime);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        GameObject newProjectile = LTPool.Instance.GetItemFromPool(projectile, shotPosition.position, Quaternion.identity);
        newProjectile.GetComponent<ProjectileBehaviour>().SetDestination(playerPos.position);
        onFinish?.Invoke();
    }

}
