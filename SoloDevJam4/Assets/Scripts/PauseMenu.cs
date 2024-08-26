using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private CameraFade _cameraFade;
    [SerializeField] private AudioSource walking;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private AudioSource click;
    public void Pause()
    {
        click.Play();
        PausePanel.SetActive(true);
        pauseButton.SetActive(false);
        if(pm._canChangeGravity)
            walking.Pause();
        Time.timeScale = 0;
    }
    
    public void UnPause()
    {
        click.Play();
        PausePanel.SetActive(false);
        pauseButton.SetActive(true);
        if(pm._canChangeGravity)
            walking.UnPause();
        Time.timeScale = 1;
    }
    
    public void ToggleFullScreen()
    {
        click.Play();
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        click.Play();
        _cameraFade.GetComponent<Image>().enabled = true;
        _cameraFade.StartFade("Main Menu");
    }

    
    
}
