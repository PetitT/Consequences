using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySuccDisplay : MonoBehaviour
{
    public Slider slider;
    IEnumerator coroutine = null;

    private void Update()
    {
        slider.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void StartSuccing(float succTime)
    {
        coroutine = Succ(succTime);
        StartCoroutine(coroutine);
    }

    public void StopSuccing()
    {
        StopCoroutine(coroutine);
        slider.normalizedValue = 1;
    }

    private IEnumerator Succ(float succTime)
    {
        float currentTime = succTime;
        while (currentTime >0)
        {
            currentTime -= Time.deltaTime;
            float normalized = currentTime / succTime;
            slider.normalizedValue = normalized;
            yield return null;
        }
    }
}
