using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            EventService.GamePaused?.Invoke();
        }
        public void ResumeGame()
        {
            b_GamePaused = false;
            Time.timeScale = 1;
            EventService.GameResumed?.Invoke();
        }

        public void Restart()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonClick);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToMenu()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonClick);
            Time.timeScale = 1;
            SceneManager.LoadScene((int)Scenes.MainMenu);
        }
    }
}