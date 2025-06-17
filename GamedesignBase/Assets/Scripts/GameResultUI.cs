using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultUI : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    public void ShowWin()
    {
        if (winScreen != null)
            winScreen.SetActive(true);
    }

    public void ShowLose()
    {
        if (loseScreen != null)
            loseScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        Debug.Log("Nächstes Minigame würde hier starten...");
    }
}

