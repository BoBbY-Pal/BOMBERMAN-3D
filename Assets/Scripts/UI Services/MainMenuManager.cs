using Enums;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject controlsPanel;
    

    public void StartGame()
    {
        SoundManager.Instance.Play(SoundTypes.ButtonClick);
        SceneManager.LoadScene((int) Scenes.GameScene);
    }

    public void ShowControls()
    {
        SoundManager.Instance.Play(SoundTypes.ButtonClick);
        controlsPanel.SetActive(true);
    }

    public void Back()
    {
        SoundManager.Instance.Play(SoundTypes.ButtonClick);
        controlsPanel.SetActive(false);
    }
        
    public void QuitGame()
    {
        SoundManager.Instance.Play(SoundTypes.ButtonClick);
        Application.Quit();
    }
}
