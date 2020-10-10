using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNPCBehaviour : MeleeEnemy
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
