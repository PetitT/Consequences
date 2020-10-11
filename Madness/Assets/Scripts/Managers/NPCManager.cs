using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCManager : LTSingleton<NPCManager>
{
    public float numberOfNPC;
    public float deadNPC;

    public float NPCDeathThreshold;

    public static event Action onNPCBecomeAgressive;
    public UnityEvent onGoodEnding;
    public UnityEvent onBadEnding;

    public void AddNPC()
    {
        numberOfNPC++;
    }

    public void RemoveNPC()
    {
        deadNPC++;
        float percent = deadNPC / numberOfNPC * 100;
        if(percent >= NPCDeathThreshold)
        {
            onNPCBecomeAgressive.Invoke();
        }
    }

    public void TriggerFinalBossEvent()
    {
        if(deadNPC == 0)
        {
            onGoodEnding?.Invoke();
        }
        else
        {
            onBadEnding?.Invoke();
        }
    }
}
