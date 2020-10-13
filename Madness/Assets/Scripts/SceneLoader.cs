using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float timeBeforeLoad = 3f;

    public void LoadScene(string sceneToLoad)
    {
        StartCoroutine(Load(sceneToLoad));
    }

    private IEnumerator Load(string sceneToLoad)
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
