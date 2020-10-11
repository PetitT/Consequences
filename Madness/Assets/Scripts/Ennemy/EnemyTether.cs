using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTether : MonoBehaviour
{
    public LineRenderer tetherLine;
    public float baseTetherTime;

    public int soulValue;

    private bool isTethering;
    private float remainingTetheringTime;

    public UnityEvent onTetherStart;
    public UnityEvent onDeath;

    public static event Action<Transform> onStaticTetherStart;
    public static event Action onStaticTetherEnd;

    private bool isDying = false;

    private void Start()
    {
        LTHealthManager.onStaticDamageTaken += Stop;
        SoulManager.Instance.AddToMaxSouls(soulValue);
    }

    private void OnDestroy()
    {
        LTHealthManager.onStaticDamageTaken -= Stop;
    }

    private void Stop()
    {
        StopTethering();
    }

    private void Update()
    {
        if (isDying) { return; }
        UpdateTether();
        TetherCooldown();
    }

    public void StartTethering()
    {
        if (isTethering) { return; }
        isTethering = true;
        remainingTetheringTime = SoulManager.Instance.CalculateTetherTime(baseTetherTime);
        ScreenShakeManager.Instance.Shake(remainingTetheringTime);
        GetComponent<EnemySuccDisplay>().StartSuccing(remainingTetheringTime);
        if (TryGetComponent(out EnemyBehaviour behaviour))
        {
            behaviour.StartAttacking();
        }
        onTetherStart?.Invoke();
        onStaticTetherStart?.Invoke(transform);
    }

    public void StopTethering()
    {
        if (isTethering == true)
        {
            GetComponent<EnemySuccDisplay>().StopSuccing();
            isTethering = false;
            onStaticTetherEnd?.Invoke();
            ScreenShakeManager.Instance.StopShake();
        }
    }

    private void UpdateTether()
    {
        if (isTethering)
        {
            tetherLine.positionCount = 2;
            tetherLine.SetPosition(0, transform.position);
            tetherLine.SetPosition(1, CharPosition.Instance.handPosition);
            tetherLine.material.SetTextureOffset("_MainTex", new Vector2(Time.time * -10, 0));
        }
        else
        {
            tetherLine.positionCount = 0;
        }
    }

    private void TetherCooldown()
    {
        if (!isTethering) { return; }
        remainingTetheringTime -= Time.deltaTime;
        if (remainingTetheringTime <= 0)
        {
            isDying = true;
            onStaticTetherEnd?.Invoke();
            Die();
        }
    }

    private void Die()
    {
        SoulManager.Instance.AddCurrentSouls(soulValue);
        StartCoroutine(BurstToPlayer());
    }

    private IEnumerator BurstToPlayer()
    {
        if (gameObject.TryGetComponentInChildren(out LTDamageDealer damageDealer))
        {
            damageDealer.gameObject.SetActive(false);
        }
        GetComponent<EnemyBehaviour>().enabled = false;

        float currentScale = 1;
        while (Vector2.Distance(transform.position, CharPosition.Instance.handPosition) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, CharPosition.Instance.position, 20 * Time.deltaTime);
            currentScale -= Time.deltaTime;
            transform.localScale = new Vector2(currentScale, currentScale);

            tetherLine.SetPosition(0, transform.position);
            tetherLine.SetPosition(1, CharPosition.Instance.handPosition);
            tetherLine.material.SetTextureOffset("_MainTex", new Vector2(Time.time * -10, 0));
            yield return null;
        }
        onDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
