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

    private string enText1 = "Oh, you must be there for the contract... An ancient evil established itself in the cathedral overlooking our village.";
    private string enText2 = "But look at you... In your current state, you don't stand any chance...";
    private string enText3 = "You are not the first to answer our call. Many other heroes preceeded you... and other might succeed you... It all depends on you.";
    private string enText4 = "Will you have what it takes to carry out our contract ? Unless you're just another coward. Hurry! Others are already fighting. You are going to need their help. One way... or another...";
    private string enText5 = "Hehehehe... And don't forget... Time is against you.";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WriteText");
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StopCoroutine("WriteText");
            SceneManager.LoadScene(sceneToLoad);
        }
    }


    public IEnumerator WriteText()
    {
        Language currentLanguage = LanguageSelect.Instance.currentLanguage;

        char[] charArray1 = currentLanguage == Language.FR? text1.ToCharArray() : enText1.ToCharArray();
        char[] charArray2 = currentLanguage == Language.FR ? text2.ToCharArray() : enText2.ToCharArray();
        char[] charArray3 = currentLanguage == Language.FR ? text3.ToCharArray() : enText3.ToCharArray();
        char[] charArray4 = currentLanguage == Language.FR ? text4.ToCharArray() : enText4.ToCharArray();
        char[] charArray5 = currentLanguage == Language.FR ? text5.ToCharArray() : enText5.ToCharArray();

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
