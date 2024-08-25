using System;
using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;
using UnityEngine.SceneManagement;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardSave : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private TMP_Text _scoreText1;
        [SerializeField] private TMP_Text _scoreText2;

        [SerializeField] private TMP_Text warningText;
        [SerializeField] private GameObject restartMenu;
        [SerializeField] private GameObject lockImg;

// Make changes to this section according to how you're storing the player's score:
// ------------------------------------------------------------

        private int Score => PlayerPrefs.GetInt("Score", 0);
// ------------------------------------------------------------

        private void Start()
        {
            if (_scoreText1 != null)
            {
                _scoreText1.text = "Final Score: " + PlayerPrefs.GetInt("Score");
            }
            if (_scoreText2 != null)
            {
                _scoreText2.text = "Final Score: " + PlayerPrefs.GetInt("Score");

            }
        }

      
        private void Update()
        {
           
            if (_scoreText1 != null)
            {
                _scoreText1.text = "Final Score: " + PlayerPrefs.GetInt("Score");
            }
            if (_scoreText2 != null)
            {
                _scoreText2.text = "Final Score: " + PlayerPrefs.GetInt("Score");

            }
        }

       
        
        public void UploadEntry()
        {
            warningText.text = "Please Wait";
            if(_usernameInputField.text != null)
                lockImg.SetActive(true);
            Leaderboards.SoloDevJa4.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
            {
               
                if (isSuccessful)
                {
                    restartMenu.SetActive(true);
                    // LoadEntries();
                }
                else
                {
                    warningText.text = "Something Wrong Happened. Try Again";
                    lockImg.SetActive(false);
                }
                
            });
        }
    }
}