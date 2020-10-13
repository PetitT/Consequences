using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public string FRtext;
    public string ENtext;
    public float timeBetweenLetters;
    public float waitTime;
    void Start()
    {
        StartCoroutine("Write");
    }

    private IEnumerator Write()
    {
        Language currentLanguage = LanguageSelect.Instance.currentLanguage;
        char[] charArray1 =  currentLanguage == Language.FR? FRtext.ToCharArray() : ENtext.ToCharArray();

        string completeText = "";
        for (int i = 0; i < charArray1.Length; i++)
        {
            completeText = completeText + charArray1[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(0);
    }


}
