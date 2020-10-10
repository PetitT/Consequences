using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttackObject : MonoBehaviour
{
    public float waitTime;
    public float attackWidth;
    public float disableTime;
    public GameObject particleObject;
    public GameObject pentacleObject;
    public LayerMask enemyLayer;

    private void OnEnable()
    {
        particleObject.GetComponent<ParticleSystem>().Stop();
        pentacleObject.SetActive(true);
        particleObject.SetActive(false);
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(waitTime);
        particleObject.SetActive(true);
        particleObject.GetComponent<ParticleSystem>().Play();
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, attackWidth, Vector2.up, 100, enemyLayer);
        if(hit.collider != null)
        {
            if(hit.collider.TryGetComponent(out EnemyTether tether))
            {
                tether.StartTethering();
            }
            else
            {
                hit.collider.GetComponentInParent<EnemyTether>().StartTethering();
            }
        }
        yield return new WaitForSeconds(disableTime);
        pentacleObject.SetActive(false);
        particleObject.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);        
    }
}
