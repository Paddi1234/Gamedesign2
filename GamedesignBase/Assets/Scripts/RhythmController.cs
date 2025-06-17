using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhythmController : MonoBehaviour
{
    public GameObject rhythmTargetPrefab;
    public RectTransform spawnArea;
    public float beatInterval = 1.2f;

    public FeedbackSpawner feedbackSpawner;
    public CharacterFaceController characterFaceController;
    public AudioSource beatAudio;
    public TextMeshProUGUI scoreText;
    public GameResultUI gameResultUI;

    private float timer = 0f;
    private int score = 0;
    public int scoreToWin = 10;

    void Start()
    {
        characterFaceController?.SetNeutralFace();
        UpdateScoreDisplay();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= beatInterval)
        {
            timer -= beatInterval;
            SpawnTarget();
            if (beatAudio) beatAudio.Play();
        }
    }

    void SpawnTarget()
    {
        Vector2 randomPos = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        Vector2 anchoredPos = new Vector2(
            Mathf.Lerp(spawnArea.rect.xMin, spawnArea.rect.xMax, randomPos.x),
            Mathf.Lerp(spawnArea.rect.yMin, spawnArea.rect.yMax, randomPos.y)
        );

        GameObject go = Instantiate(rhythmTargetPrefab, spawnArea);
        go.GetComponent<RectTransform>().anchoredPosition = anchoredPos;

        RhythmTarget target = go.GetComponent<RhythmTarget>();
        target.Init(this);
    }

    public void OnTargetHit(Vector3 pos)
    {
        score++;
        characterFaceController?.SetEmotion(true);
        UpdateScoreDisplay();

        string[] texts = { "Perfekt!", "Nice!", "Sehr gut!" };
        feedbackSpawner.SpawnFeedback(texts[Random.Range(0, texts.Length)], Color.green, pos);

        if (score >= scoreToWin)
        {
            gameResultUI?.ShowWin();
        }
    }

    public void OnTargetMiss(Vector3 pos)
    {
        score -= 2;
        characterFaceController?.SetEmotion(false);
        UpdateScoreDisplay();

        string[] texts = { "Ups!", "Daneben!", "Mies!" };
        feedbackSpawner.SpawnFeedback(texts[Random.Range(0, texts.Length)], Color.red, pos);

        if (score < 0)
        {
            gameResultUI?.ShowLose();
        }
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = $"Punkte: {score}";
    }
}