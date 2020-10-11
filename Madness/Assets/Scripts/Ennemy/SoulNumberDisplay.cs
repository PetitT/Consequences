using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulNumberDisplay : MonoBehaviour
{
    public TextMeshPro text;
    private RectTransform rect;

    private void Start()
    {
        text.text = GetComponentInParent<EnemyTether>().soulValue.ToString();
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.rotation = Quaternion.Euler(0, 0, 0);
    }
}
