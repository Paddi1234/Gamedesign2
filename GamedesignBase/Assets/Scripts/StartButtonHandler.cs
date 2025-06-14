using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    public Button startButton;
    public Image buttonImage;
    public Sprite normalSprite;
    public Sprite clickedSprite;
    public string sceneToLoad = "MainScene";
    public float fadeDuration = 1f;
    public Image fadeOverlay; // Das schwarze Overlay-Image

    private void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);

        if (buttonImage != null && normalSprite != null)
            buttonImage.sprite = normalSprite;

        if (fadeOverlay != null)
        {
            // Sicherstellen, dass FadeOverlay zu Beginn transparent ist
            Color c = fadeOverlay.color;
            c.a = 0f;
            fadeOverlay.color = c;
        }
    }

    void OnStartClicked()
    {
        Debug.Log("Start Button clicked!");
        startButton.interactable = false;

        if (buttonImage != null && clickedSprite != null)
            buttonImage.sprite = clickedSprite;

        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        // Fade-Out einleiten
        if (fadeOverlay != null)
        {
            float t = 0f;
            Color c = fadeOverlay.color;

            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                c.a = Mathf.Clamp01(t / fadeDuration);
                fadeOverlay.color = c;
                yield return null;
            }
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
