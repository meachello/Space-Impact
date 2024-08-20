using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject LooseWindow;
    public GameObject WinWindow;
    public static GameManager instance;

    private void Start()
    {
        instance = this;
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ScoreManager.Score = 0;
        Hearts.HealthPoints = 3;
    }

    public void GameOver()
    {
        LooseWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinGame()
    {
        WinWindow.SetActive(true);
        Time.timeScale = 0;
    }
}
