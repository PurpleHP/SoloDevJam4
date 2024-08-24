using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraFade : MonoBehaviour
{
    public Image img;
    [SerializeField] private Animator anim;

    public void StartFade(string levelName)
    {
        StartCoroutine(Fading(levelName));
    }

    IEnumerator Fading(string levelName)
    {
        anim.SetBool("Fade",true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Fade",false);

        yield return new WaitForSeconds(1f);
        //yield return new WaitUntil(() => Math.Abs(img.color.a - 1) < 0.00005f);
        SceneManager.LoadScene(levelName);
    }

}
