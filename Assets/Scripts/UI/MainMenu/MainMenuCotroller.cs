using UnityEngine;
using UnityEngine.SceneManagement; // Задължително, за да можем да зареждаме други сцени!

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "GameDemo";

    public void PlayGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenOptions()
    {
        Debug.Log("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}