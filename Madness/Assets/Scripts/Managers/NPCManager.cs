using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : LTSingleton<NPCManager>
{
    public float numberOfNPC;
    public float deadNPC;

    public float NPCDeathThreshold;

    public static event Action onNPCBecomeAgressive;

    public void AddNPC()
    {
        numberOfNPC++;
    }

    [ContextMenu("Remove")]
    public void RemoveNPC()
    {
        deadNPC++;
        float percent = deadNPC / numberOfNPC * 100;
        if(percent >= NPCDeathThreshold)
        {
            onNPCBecomeAgressive.Invoke();
        }
    }
}
