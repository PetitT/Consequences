using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulManager : LTSingleton<SoulManager>
{
    public float currentSouls;
    public float maxSouls;

    public float percentageDivisionBuff = 2;

    public TextMeshProUGUI text;

    public void AddToMaxSouls(int souls)
    {
        maxSouls += souls;
        ResetDisplay();
    }

    public void AddCurrentSouls(int souls)
    {
        currentSouls += souls;
        ResetDisplay();
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
}
