using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CircleAtherien : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    public Image countdownRing;
    public float timeToReact = 3f;

    private string requiredKey;
    private GameManagerAtherien gameManager;
    private bool isActive = true;
    private float timer = 0f;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManagerAtherien>();

        string[] keys = { "A", "S", "D", "F" };
        requiredKey = keys[Random.Range(0, keys.Length)];
        keyText.text = requiredKey;
        countdownRing.fillAmount = 1f;
    }

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        countdownRing.fillAmount = 1 - (timer / timeToReact);

        if (Input.GetKeyDown(requiredKey.ToLower()))
        {
            isActive = false;
            gameManager.UpdateScore(1);
            Destroy(gameObject);
        }

        if (timer >= timeToReact)
        {
            isActive = false;
            gameManager.UpdateScore(-1);
            Destroy(gameObject);
        }
    }
}

