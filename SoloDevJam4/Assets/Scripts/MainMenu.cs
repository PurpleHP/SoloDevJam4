using UnityEngine;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private CameraFade _cameraFade;
   [SerializeField] private GameObject leaderBoardPanel;
   public void QuitGame()
   {
      Application.Quit();
   }

   public void StartLevel()
   {
      _cameraFade.StartFade("Level");
   }

   public void ShowLeaderboard()
   {
      leaderBoardPanel.SetActive(true);
   }

   public void HideLeaderboard()
   {
      leaderBoardPanel.SetActive(false);
   }

   public void FullScreen()
   {
      Screen.fullScreen = !Screen.fullScreen;
   }
}
