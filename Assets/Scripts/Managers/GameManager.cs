using System;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameManager : MonoGenericSingleton<GameManager>
    {
        private bool b_GamePaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (b_GamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        private void PauseGame()
        {
            b_GamePaused = true;
            Time.timeScale = 0;
            EventService.Instance.GamePaused?.Invoke();
        }
        private void ResumeGame()
        {
            b_GamePaused = false;
            Time.timeScale = 1;
            EventService.Instance.GameResumed?.Invoke();
        }
        public void GameOver()
        {
            GameLogManager.CustomLog("Game Over!");
        }
    }
}