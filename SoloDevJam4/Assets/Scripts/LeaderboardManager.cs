using System;
using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;
using UnityEngine.SceneManagement;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_Text[] _entryTextObjectsScore;

        [SerializeField] private GameObject usernameContainer;
        [SerializeField] private GameObject scoreContainer;

        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private TMP_Text _scoreText1;
        [SerializeField] private TMP_Text _scoreText2;

        [SerializeField] private TMP_Text warningText;
        [SerializeField] private GameObject restartMenu;

// Make changes to this section according to how you're storing the player's score:
// ------------------------------------------------------------

        private int Score => PlayerPrefs.GetInt("Score", 0);
// ------------------------------------------------------------

        private void Start()
        {
            if (_entryTextObjects.Length != 0)
            {
                LoadEntries();
            }
            if (_scoreText1 != null)
            {
                _scoreText1.text = "Final Score: " + PlayerPrefs.GetInt("Score");
            }
            if (_scoreText2 != null)
            {
                _scoreText2.text = "Final Score: " + PlayerPrefs.GetInt("Score");

            }
        }

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                _entryTextObjects = usernameContainer.GetComponentsInChildren<TMP_Text>();
                _entryTextObjectsScore = scoreContainer.GetComponentsInChildren<TMP_Text>();
            }
        }

        private void Update()
        {
            Vector3 pos = scoreContainer.transform.position;
            scoreContainer.transform.position = new Vector3(pos.x,usernameContainer.transform.position.y,pos.z);
            if (_scoreText1 != null)
            {
                _scoreText1.text = "Final Score: " + PlayerPrefs.GetInt("Score");
            }
            if (_scoreText2 != null)
            {
                _scoreText2.text = "Final Score: " + PlayerPrefs.GetInt("Score");

            }
        }

        private void LoadEntries()
        {
            // Q: How do I reference my own leaderboard?
            // A: Leaderboards.<NameOfTheLeaderboard>
        
            Leaderboards.SoloDevJa4.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";
                
                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    string tempText = entries[i].Rank.ToString();
                    string rankText = tempText.PadLeft(5 - tempText.Length);
                    _entryTextObjects[i].text = $"{rankText}.   {entries[i].Username}";
                    _entryTextObjectsScore[i].text = $"{entries[i].Score}";
                }
            });
        }
        
        public void UploadEntry()
        {
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
                }
                
            });
        }
    }
}