﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public TextMeshPro tmp;
    public float displayRange;
    public LayerMask playerLayer;
    public float timeBetweenLetters;
    public float waitTime;
    public string FRtextToWrite;
    public string EntextToWrite;

    private bool hasDisplayed = false;

    private void Update()
    {
        tmp.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (hasDisplayed) { return; }
        if (Physics2D.OverlapCircle(transform.position, displayRange, playerLayer))
        {
            hasDisplayed = true;
            StartCoroutine(WriteText());
        }
    }

    public void Stop()
    {
        StopCoroutine(WriteText());
        tmp.enabled = false;
    }

    public IEnumerator WriteText()
    {
        Language currentLanguage = LanguageSelect.Instance.currentLanguage;
        char[] charArray = currentLanguage == Language.FR ? FRtextToWrite.ToCharArray() : EntextToWrite.ToCharArray();
        string completeText = "";
        for (int i = 0; i < charArray.Length; i++)
        {
            completeText = completeText + charArray[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        tmp.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, displayRange);
    }
}
