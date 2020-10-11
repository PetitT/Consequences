using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroSceneText : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public float timeBetweenLetters;
    public float waitTime;
    public string sceneToLoad;
    private string text1 = "Oh, vous devez etre la pour le contrat... Un mal ancien s\'est etabli dans la cathedrale qui surplombe notre village.";
    private string text2 = "Mais regardez-vous... Dans votre etat actuel, vous n\'avez pas la moindre chance...";
    private string text3 = "Vous n\'etes pas le premier a repondre a l\'appel. D'autres heros vous ont precede... et d\'autres vous succederont peut-etre... Tout ne depend plus que de vous.";
    private string text4 = "Aurez-vous ce qu'il faut pour mener le contrat a terme ? A moins que vous ne soyez qu\'un autre de ces pleutres ? Depechez vous ! Les autres sont en train de se battre, plus loin. Vous aurez besoin de leur aide. D'une maniere... Ou d'une autre.";
    private string text5 = "Hahahaha... Et n\'oubliez pas... Le temps joue contre vous.";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WriteText");
    }


    public IEnumerator WriteText()
    {
        char[] charArray1 = text1.ToCharArray();
        char[] charArray2 = text2.ToCharArray();
        char[] charArray3 = text3.ToCharArray();
        char[] charArray4 = text4.ToCharArray();
        char[] charArray5 = text5.ToCharArray();

        string completeText = "";
        for (int i = 0; i < charArray1.Length; i++)
        {
            completeText = completeText + charArray1[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        completeText = completeText + "\n\n";

        for (int i = 0; i < charArray2.Length; i++)
        {   
            completeText = completeText + charArray2[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        completeText = "";

        for (int i = 0; i < charArray3.Length; i++)
        {
            completeText = completeText + charArray3[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        completeText = completeText + "\n\n";

        for (int i = 0; i < charArray4.Length; i++)
        {
            completeText = completeText + charArray4[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        completeText = completeText + "\n\n";

        for (int i = 0; i < charArray5.Length; i++)
        {
            completeText = completeText + charArray5[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        yield return new WaitForSeconds(waitTime);
        tmp.enabled = false;

        SceneManager.LoadScene(sceneToLoad);
    }

}
