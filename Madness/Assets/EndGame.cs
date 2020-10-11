using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public string text;
    public float timeBetweenLetters;
    public float waitTime;
    void Start()
    {
        StartCoroutine("Write");
    }

    private IEnumerator Write()
    {
        char[] charArray1 = text.ToCharArray();

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
