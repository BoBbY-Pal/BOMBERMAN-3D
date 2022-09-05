using System;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    public class GameManager : MonoGenericSingleton<GameManager>
    {
        private GameObject _controlsPanel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        private void PauseGame()
        {
            throw new NotImplementedException();
        }

        public void StartGame()
        {
            SceneManager.LoadScene((int) Scenes.GameScene);
        }

        public void ShowControls()
        {
            _controlsPanel.SetActive(true);
        }

        public void Back()
        {
            _controlsPanel.SetActive(false);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }

        public void GameOver()
        {
            GameLogManager.CustomLog("Game Over!");
        }
    }
}