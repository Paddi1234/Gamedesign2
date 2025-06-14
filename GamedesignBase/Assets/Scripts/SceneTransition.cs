using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeFromBlack()
    {
        float time = 0f;
        Color color = fadeImage.color;
        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            fadeImage.color = new Color(color.r, color.g, color.b, 1f - t);
            time += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(color.r, color.g, color.b, 0f);
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            fadeImage.color = new Color(color.r, color.g, color.b, t);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 1f);
        SceneManager.LoadScene(sceneName);
    }
}