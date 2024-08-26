using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private CameraFade _cameraFade;
   [SerializeField] private GameObject leaderBoardPanel;
   [SerializeField] private AudioSource click;
   public void QuitGame()
   {
      Application.Quit();
   }

   public void StartLevel()
   {
      _cameraFade.GetComponent<Image>().enabled = true;
      click.Play();
      _cameraFade.StartFade("Level");
   }
   

   public void ShowLeaderboard()
   {
      click.Play();

      leaderBoardPanel.SetActive(true);
   }

   public void HideLeaderboard()
   {
      click.Play();
      leaderBoardPanel.SetActive(false);
   }

   public void FullScreen()
   {
      click.Play();
      Screen.fullScreen = !Screen.fullScreen;
   }
}
