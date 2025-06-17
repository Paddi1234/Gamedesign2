using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DragAndDropManager : MonoBehaviour
{
    public int score = 0;
    public int targetScore = 5;

    public Text scoreText;
    public GameObject winScreen;
    public GameObject loseScreen;

    public string returnToScene = "MainLevel";

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        score = Mathf.Max(0, score); // Kein negativer Score
        UpdateScoreUI();

        if (score >= targetScore)
        {
            ShowWin();
        }
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Punkte: " + score.ToString();
        }
    }

    public void ShowWin()
    {
        if (winScreen != null) winScreen.SetActive(true);
    }

    public void ShowLose()
    {
        if (loseScreen != null) loseScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(returnToScene);
    }
}
