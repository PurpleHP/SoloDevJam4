using System;
using System.Collections;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource2;
    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject pauseButton;
    
    void Awake()
    {
        StartCoroutine(PlayAudioSourcesInOrder());
    }

    private IEnumerator PlayAudioSourcesInOrder()
    {
        yield return new WaitForSeconds(1.5f);
        
        //audioSource2.Play();
        yield return new WaitForSeconds(0.2f);


        audioManager.SetActive(true);
        yield return new WaitForSeconds(0.2f);


        pauseButton.SetActive(true);
    }
    
}
