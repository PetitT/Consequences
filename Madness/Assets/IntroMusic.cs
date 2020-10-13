using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMusic : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneCheck;
    }

    private void SceneCheck(Scene arg0, Scene arg1)
    {
        if (arg1.name == "GameScene")
        {
            StartCoroutine("Fadeout");
        }
    }

    private IEnumerator Fadeout()
    {
        AudioSource src = GetComponent<AudioSource>();
        while(src.volume > 0)
        {
            src.volume -= Time.deltaTime * 0.33f;
            yield return null;
        }
        Destroy(gameObject);
    }
}
