﻿using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : LTSingleton<ScreenShakeManager>
{
    public CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin perlin;
    private IEnumerator coroutine;

    public float maxShake;

    private void Start()
    {
        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float shakeTime)
    {
        coroutine = Shakey(shakeTime);
        StartCoroutine(coroutine);
    }

    public void StopShake()
    {
        StopCoroutine(coroutine);
        perlin.m_AmplitudeGain = 0;
    }

    private IEnumerator Shakey(float shakeTime)
    {
        float shakePerSec = maxShake / shakeTime;
        float elapsedTime = 0;
        perlin.m_AmplitudeGain = 0;

        while (elapsedTime < shakeTime)
        {
            perlin.m_AmplitudeGain += shakePerSec * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (perlin.m_AmplitudeGain > 0)
        {
            perlin.m_AmplitudeGain -= shakePerSec * 10 * Time.deltaTime;
            yield return null;
        }
        perlin.m_AmplitudeGain = 0;
    }
}
