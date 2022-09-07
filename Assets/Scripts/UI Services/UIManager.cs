using Enums;
using Managers;
using TMPro;
using UnityEngine;
using Utilities;

namespace UI_Services
{
    public class UIManager : MonoGenericSingleton<UIManager>
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI title, scoreTxt;
        [SerializeField] private GameObject resumeBtn, playAgainBtn, restartBtn, menuBtn, nextLevelBtn;

        private int _score;
        private void OnEnable()
        {
            EventService.GamePaused += PauseGame;
            EventService.GameResumed += ResumeGame;
            EventService.GameOver += GameOver;
            EventService.GameWon += GameWon;
            EventService.UpdateScore += UpdateScore;
        }

        private void PauseGame()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonClick);
            panel.SetActive(true);
            title.text = "GAME PAUSED";
            restartBtn.SetActive(true);
            resumeBtn.SetActive(true);
        }

        private void ResumeGame()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonClick);
            restartBtn.SetActive(false);
            resumeBtn.SetActive(false);
            panel.SetActive(false);
        }
    
        private void GameOver()
        {
            SoundManager.Instance.Play(SoundTypes.GameLose);
            GameLogManager.CustomLog("Game Over!");
            panel.SetActive(true);
            title.text = "GAME OVER";
            playAgainBtn.SetActive(true);
        }
    
        private void GameWon()
        {
            SoundManager.Instance.Play(SoundTypes.GameWon);
            panel.SetActive(true);
            title.text = "YOU WON";
            nextLevelBtn.SetActive(true);
        }

        private void UpdateScore(int val)
        {
            _score += val;
            scoreTxt.text = "SCORE: " + _score;
        }
        private void OnDisable()
        {
            EventService.GamePaused -= PauseGame;
            EventService.GameResumed -= ResumeGame;
            EventService.GameOver -= GameOver;
            EventService.GameWon -= GameWon;
            EventService.UpdateScore -= UpdateScore;
        }
    }
}
