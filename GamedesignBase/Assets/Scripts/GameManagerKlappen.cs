using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManagerKlappen : MonoBehaviour
{
    public GameObject keyPromptPrefab;
    public Transform spawnArea; // z.B. Canvas-Transform, in dem gespawnt wird
    public RectTransform canvasRectTransform; // Canvas RectTransform, im Inspector zuweisen
    public TextMeshProUGUI scoreText;
    public int winScore = 50;

    [Header("Szenenwechsel")]
    public string nextSceneName; // Zielszene im Inspector eintragen

    private int score = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnKeyPrompt();
            yield return new WaitForSeconds(1.5f);
        }
    }

    void SpawnKeyPrompt()
    {
        char randomKey = (char)Random.Range(65, 91); // A-Z
        // Instanziere KeyPrompt als Kind von spawnArea (Canvas-Transform)
        GameObject kp = Instantiate(keyPromptPrefab, spawnArea);

        // Setze die Position innerhalb des Canvas-Rects zufällig
        RectTransform rt = kp.GetComponent<RectTransform>();
        rt.anchoredPosition = GetRandomSpawnPosition();

        // Initialisiere den KeyPrompt mit Taste und Referenz auf diesen GameManager
        kp.GetComponent<KeyPromptKlappen>().Init(randomKey, this);
    }

    Vector2 GetRandomSpawnPosition()
    {
        float padding = 100f;
        float width = canvasRectTransform.rect.width;
        float height = canvasRectTransform.rect.height;

        // Mittiger Nullpunkt → -halbe Breite bis +halbe Breite
        float x = Random.Range(-width / 2 + padding, width / 2 - padding);
        float y = Random.Range(-height / 2 + padding, height / 2 - padding);

        return new Vector2(x, y);
    }

    public void AddPoints(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;

        if (score >= winScore)
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("Keine Zielszene angegeben!");
            }
        }
    }
}



