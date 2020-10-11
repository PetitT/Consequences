using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SoulManager : LTSingleton<SoulManager>
{
    public float currentSouls;
    public float maxSouls;

    public float percentageDivisionBuff = 2;

    public TextMeshProUGUI text;

    [Header("Events")]
    public int doorSoulsThreshold;
    public UnityEvent onDoorSoulsGot;
    private bool hasLaunchedDoorEvent;

    public void AddToMaxSouls(int souls)
    {
        maxSouls += souls;
        ResetDisplay();
    }

    public void AddCurrentSouls(int souls)
    {
        currentSouls += souls;
        ResetDisplay();
        //CheckDoor();
    }

    private void ResetDisplay()
    {
        text.text = currentSouls.ToString() + " / " + maxSouls.ToString();
    }

    public float CalculateTetherTime(float baseTetherTime)
    {
        float percentOfSouls = (currentSouls / maxSouls) * 100;
        float divided = percentOfSouls / percentageDivisionBuff;
        float time = baseTetherTime - (baseTetherTime / 100 * divided);
        return time;
    }

    private void CheckDoor()
    {
        if (hasLaunchedDoorEvent) { return; }
        if(currentSouls >= doorSoulsThreshold)
        {
            onDoorSoulsGot?.Invoke();
            hasLaunchedDoorEvent = true;
        }
    }
}
