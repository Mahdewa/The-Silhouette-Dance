using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigator : MonoBehaviour
{
    public void PlayGame()
    {
        SceneLoader.sceneToLoad = "Prologue";
        SceneManager.LoadScene("LoadingScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenuWithLoading()
    {
        SceneLoader.sceneToLoad = "MainMenu";
        SceneManager.LoadScene("LoadingScene");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}