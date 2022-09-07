using TMPro;
using UnityEngine;
using Utilities;

public class UIManager : MonoGenericSingleton<UIManager>
{
    public GameObject panel;
    public TextMeshProUGUI title, score, highScore;
    public GameObject resumeBtn, playAgainBtn, restartBtn, menuBtn, nextLevelBtn;

    private void OnEnable()
    {
        EventService.Instance.GamePaused += PauseGame;
        EventService.Instance.GameResumed += ResumeGame;
    }

    private void PauseGame()
    {
        panel.SetActive(true);
        title.text = "GAME PAUSED";
        restartBtn.SetActive(true);
        resumeBtn.SetActive(true);
    }

    private void ResumeGame()
    {
        restartBtn.SetActive(false);
        resumeBtn.SetActive(false);
        panel.SetActive(false);
    }
    
    private void GameOver()
    {
        panel.SetActive(true);
        title.text = "GAME OVER";
        playAgainBtn.SetActive(true);
    }
}
