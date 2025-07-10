using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerDaD : MonoBehaviour
{
    public static GameManagerDaD Instance;

    public TMP_Text scoreText;         // TextMeshPro statt UI.Text
    private int score = 0;

    public AudioClip successClip;
    private AudioSource audioSource;

    public int scoreToWin = 5; // Beispiel: Nach 5 Punkten wechselt die Szene
    public string nextSceneName;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
        PlaySuccessSound();

        if (score >= scoreToWin)
        {
            LoadNextScene();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Punkte: " + score;
    }

    private void PlaySuccessSound()
    {
        if (successClip != null && audioSource != null)
            audioSource.PlayOneShot(successClip);
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set! Scene not loaded.");
        }
    }

}

