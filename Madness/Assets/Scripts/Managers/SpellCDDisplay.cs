using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCDDisplay : LTSingleton<SpellCDDisplay>
{
    public Slider slider;

    public void Display(float displayTime)
    {
        StartCoroutine(Show(displayTime));
    }

    public IEnumerator Show(float displayTime)
    {
        float elapsedTime = 0;

        while(elapsedTime < displayTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / displayTime;
            slider.normalizedValue = normalizedTime;
            yield return null;
        }
        slider.normalizedValue = 1;
    }
}
