using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDeathDialog : MonoBehaviour
{
    public TextMeshPro tmp;
    public float timeBetweenLetters;
    public float waitTime;
    public string FRtextToWrite;
    public string ENtextToWrite;

    public void Write()
    {
        StartCoroutine(WriteText());
    }

    private void Update()
    {
        tmp.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public IEnumerator WriteText()
    {
        Language currentLanguage = LanguageSelect.Instance.currentLanguage;

        char[] charArray = currentLanguage == Language.FR ? FRtextToWrite.ToCharArray() : ENtextToWrite.ToCharArray();
        string completeText = "";
        for (int i = 0; i < charArray.Length; i++)
        {
            tmp.transform.rotation = Quaternion.Euler(0, 0, 0);
            completeText = completeText + charArray[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        tmp.enabled = false;
    }
}
