using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class CameraFade : MonoBehaviour
{
    public Image img;
    [SerializeField] private Animator anim;
    private static readonly int Fade = Animator.StringToHash("Fade");
    [SerializeField] private AudioSource src;
    private float fadeDuration = 0.3f;


    public void StartFade(string levelName)
    {
        StartCoroutine(Fading(levelName));
    }
    
    public void StartFade()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading(string levelName)
    {
        StartCoroutine(FadeOut(src, fadeDuration));

        anim.SetBool(Fade,true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(Fade,false);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
    IEnumerator Fading()
    {
        StartCoroutine(FadeOutHalf(src, fadeDuration));
        anim.SetBool(Fade,true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(Fade,false);

        yield return new WaitForSeconds(1.2f);
        anim.gameObject.GetComponent<Image>().enabled = false;
    }
    
    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop(); 
        audioSource.volume = startVolume;

    }
    private IEnumerator FadeOutHalf(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > startVolume/2.0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = startVolume/2.0f;
    }
    
}
