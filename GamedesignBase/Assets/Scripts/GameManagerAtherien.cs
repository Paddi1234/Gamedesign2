using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManagerAtherien : MonoBehaviour
{
    public GameObject circlePrefab;
    public RectTransform canvasTransform;
    public RectTransform spawnPoint; // Hier ziehst du das Platzhalter-Objekt rein
    public TextMeshProUGUI scoreText;

    public int score = 0;
    public int winScore = 10;
    public float spawnInterval = 2f;

    void Start()
    {
        UpdateScore(0);
        StartCoroutine(SpawnCircles());
    }

    IEnumerator SpawnCircles()
    {
        while (true)
        {
            SpawnCircleAtFixedPosition();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCircleAtFixedPosition()
    {
        GameObject circle = Instantiate(circlePrefab, canvasTransform);
        circle.GetComponent<RectTransform>().anchoredPosition = spawnPoint.anchoredPosition;
    }

    public void UpdateScore(int change)
    {
        score += change;
        scoreText.text = "Score: " + score;

        if (score >= winScore)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
