using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFader : MonoBehaviour
{
    public AudioSource src;
    public float fadeSpeed;

    public void Fade(AudioClip clip)
    {
        StartCoroutine(FadeToMusic(clip));
    }

    public IEnumerator FadeToMusic(AudioClip clip)
    {
        float basevolume = src.volume;
        while(src.volume > 0)
        {
            src.volume -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        src.clip = clip;
        src.Play();
        while(src.volume < basevolume)
        {
            src.volume += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        src.volume = basevolume;
    }
}
