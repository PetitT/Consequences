using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBehaviour
{
    public float attackRange;
    public float dashSpeed;
    public GameObject hitbox;

    protected override IEnumerator Attack(Action onFinish)
    {
        anim.SetBool("IsMoving", false);
        yield return new WaitForSeconds(attackWaitTime);

        float range = transform.rotation == Quaternion.Euler(0, 0, 0) ? attackRange : -attackRange;

        Vector2 targetPos = new Vector2(transform.position.x + range, transform.position.y);
        float distance = 0;
        hitbox.SetActive(true);
        anim.SetTrigger("Attack");
        while (distance < attackRange)
        {
            Vector2 player = new Vector2(playerPos.position.x, transform.position.y);
            transform.LookAt(player);
            transform.Rotate(Vector3.up, -90);

            transform.position = Vector2.Lerp(transform.position, targetPos, dashSpeed * Time.deltaTime);
            distance += dashSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        hitbox.SetActive(false);
        anim.SetTrigger("StopAttack");
        onFinish?.Invoke();
    }
}
