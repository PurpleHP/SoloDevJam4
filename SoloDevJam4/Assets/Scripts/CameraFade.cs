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
        anim.SetBool(Fade,true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(Fade,false);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
    IEnumerator Fading()
    {
        anim.SetBool(Fade,true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(Fade,false);

        yield return new WaitForSeconds(1.2f);
        anim.gameObject.GetComponent<Image>().enabled = false;
    }

}
