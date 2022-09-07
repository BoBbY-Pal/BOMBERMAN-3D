using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject controlsPanel;
    

    public void StartGame()
    {
        SceneManager.LoadScene((int) Scenes.GameScene);
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
    }

    public void Back()
    {
        controlsPanel.SetActive(false);
    }
        
    public void QuitGame()
    {
        Application.Quit();
    }
}
