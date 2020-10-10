using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : EnemyBehaviour
{
    public GameObject hitBox;
    public AnimationCurve jumpCurve;
    public float jumpSpeed;
    public float jumpHeight;
    public Vector2 additionalJumpRange;
    private float baseHeight;

    protected override IEnumerator Attack(Action onFinish)
    {
        baseHeight = transform.position.y;
        float additionnal = additionalJumpRange.RandomRange();
        float addedRange = playerPos.position.x < transform.position.x ? -additionnal : additionnal;
       
        Vector2 target = new Vector2(playerPos.position.x + addedRange, transform.position.y);

        float distance = Vector2.Distance(transform.position, target);
        float elapsedDistance = 0;
        hitBox.SetActive(true);
        while (elapsedDistance < distance)
        {
            float X = Mathf.MoveTowards(transform.position.x, target.x, jumpSpeed * Time.deltaTime);
            elapsedDistance += jumpSpeed * Time.deltaTime;
            float perOne = elapsedDistance / distance;
            float Y = jumpCurve.Evaluate(perOne) * jumpHeight;
            Y += baseHeight;
            transform.position = new Vector2(X, Y);
            yield return new WaitForEndOfFrame();
        }
        hitBox.SetActive(false);
        yield return new WaitForSeconds(attackWaitTime);
        onFinish?.Invoke();
    }
}
