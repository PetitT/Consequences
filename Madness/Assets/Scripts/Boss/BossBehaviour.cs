using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject fireCarpetRight;
    public GameObject fireCarpetLeft;
    public GameObject fallingFireBall;

    public Transform carpetRightPos;
    public Transform carpetLeftPos;

    public List<Transform> fireBallsPos;

    public float timeAfterFireBall;
    public float timeAfterRain;
    public float timeAfterCarpet;

    private bool canAttack = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
    }

    private void Update()
    {
        if (canAttack)
        {
            int random = UnityEngine.Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    StartCoroutine(ThrowFireBall());
                    break;

                case 1:
                    StartCoroutine(RainFireBalls());
                    break;

                case 2:
                    StartCoroutine(MoveFireCarpet());
                    break;

                default:
                    break;
            }
            canAttack = false;
        }
    }

    private IEnumerator ThrowFireBall()
    {
        GameObject newProjectile = LTPool.Instance.GetItemFromPool(fireBall, transform.position, Quaternion.identity);
        newProjectile.GetComponent<ProjectileBehaviour>().SetDestination(CharPosition.Instance.position);
        yield return new WaitForSeconds(timeAfterFireBall);
        canAttack = true;
    }

    private IEnumerator RainFireBalls()
    {
        foreach (var pos in fireBallsPos)
        {
            GameObject newProjectile = LTPool.Instance.GetItemFromPool(fallingFireBall, pos.position, Quaternion.Euler(0, 0, -90));
        }
        yield return new WaitForSeconds(timeAfterRain);
        canAttack = true;
    }

    private IEnumerator MoveFireCarpet()
    {
        int random = UnityEngine.Random.Range(0, 2);
        if (random == 0)
        {
            GameObject newProjectile = LTPool.Instance.GetItemFromPool(fireCarpetRight, carpetRightPos.position, Quaternion.identity);
        }
        if (random == 1)
        {
            GameObject newProjectile = LTPool.Instance.GetItemFromPool(fireCarpetLeft, carpetLeftPos.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(timeAfterCarpet);
        canAttack = true;
    }
}
