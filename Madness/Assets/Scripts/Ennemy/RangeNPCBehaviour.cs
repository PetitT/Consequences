using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNPCBehaviour : RangeEnemy
{
    private void Start()
    {
        IsAgressive = false;
        NPCManager.Instance.AddNPC();
        NPCManager.onNPCBecomeAgressive += OnBecomeAgressiveHandler;
    }

    private void OnBecomeAgressiveHandler()
    {
        IsAgressive = true;
    }
}
