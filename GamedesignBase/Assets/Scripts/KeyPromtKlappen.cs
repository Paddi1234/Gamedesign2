using UnityEngine;
using TMPro;

public class KeyPromptKlappen : MonoBehaviour
{
    public TextMeshProUGUI keyText;                 // Zeigt den Buchstaben im UI an
    public GameObject feedbackPopupPrefab;          // Popup Prefab (z.B. "Treffer!")
    public Transform popupParent;                   // Elternobjekt für Popups (z. B. Canvas)
    public AudioClip hitSound;                      // Sound bei Treffer
    public AudioClip missSound;                     // Sound bei Verpassen

    private AudioSource audioSource;
    private float lifetime = 2f;
    private float timer;
    private char assignedKey;
    private GameManagerKlappen gameManager;
    private bool resolved = false;

    public void Init(char key, GameManagerKlappen gm)
    {
        assignedKey = key;
        keyText.text = key.ToString();
        timer = lifetime;
        gameManager = gm;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Awake()
    {
        // Fallback für Popup Parent (falls leer)
        if (popupParent == null)
        {
            Canvas c = Object.FindFirstObjectByType<Canvas>();
            if (c != null) popupParent = c.transform;
        }
    }

    void Update()
    {
        if (resolved) return;

        timer -= Time.deltaTime;

        if (Input.GetKeyDown(assignedKey.ToString().ToLower()))
        {
            resolved = true;
            gameManager.AddPoints(10);
            PlaySound(hitSound);
            ShowPopup("Hit!", Color.green);
            Destroy(gameObject);
        }

        if (timer <= 0f)
        {
            resolved = true;
            gameManager.AddPoints(-5);
            PlaySound(missSound);
            ShowPopup("Miss!", Color.red);
            Destroy(gameObject);
        }
    }

    void ShowPopup(string message, Color color)
    {
        GameObject popup = Instantiate(feedbackPopupPrefab, transform.position, Quaternion.identity, popupParent);

        var text = popup.GetComponent<TextMeshProUGUI>();
        text.text = message;
        text.color = color;

        StartCoroutine(PopupAnimation(popup.GetComponent<RectTransform>(), text));
    }


    System.Collections.IEnumerator PopupAnimation(RectTransform popup, TextMeshProUGUI text)
    {
        float duration = 0.5f;
        Vector2 start = popup.anchoredPosition;
        Vector2 end = start + Vector2.up * 30f;

        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            popup.anchoredPosition = Vector2.Lerp(start, end, t / duration);

            Color c = text.color;
            c.a = 1 - t / duration;
            text.color = c;

            yield return null;
        }

        Destroy(popup.gameObject);
    }


    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }
}




