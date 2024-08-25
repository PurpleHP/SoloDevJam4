using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private CameraFade _cameraFade;

    public void Pause()
    {
        PausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void UnPause()
    {
        PausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
    
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        _cameraFade.GetComponent<Image>().enabled = true;
        _cameraFade.StartFade("Main Menu");
    }

    
    
}
