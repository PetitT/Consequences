using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public List<GameObject> hearts;

    private void Awake()
    {
        FindObjectOfType<LTHealthManager>().onHealthChanged.AddListener(DisplayHealth);
    }

    private void DisplayHealth(float arg0, float arg1)
    {
        int current = (int)arg0;
        hearts.ForEach(t => t.SetActive(false));

        for (int i = 0; i < arg0; i++)
        {
            hearts[i].SetActive(true);
        }
    }
}
