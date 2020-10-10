using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : LTSingleton<SoulManager>
{
    public float currentSouls;
    public float maxSouls;

    public float percentageDivisionBuff = 2;

    public void AddToMaxSouls(int souls)
    {
        maxSouls += souls;
    }

    public void AddCurrentSouls(int souls)
    {
        currentSouls += souls;
    }

    public float CalculateTetherTime(float baseTetherTime)
    {
        float percentOfSouls = (currentSouls / maxSouls) * 100;
        float divided = percentOfSouls / percentageDivisionBuff;
        float time = baseTetherTime - (baseTetherTime / 100 * divided);
        return time;
    }
}
