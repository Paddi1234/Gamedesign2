using UnityEngine;

public class SceneTriggerWithFade : MonoBehaviour
{
    public string sceneToLoad;
    public AudioClip enterSound;
    private SceneTransition sceneTransition;
    private AudioSource audioSource;
    private bool hasTriggered = false;

    private void Start()
    {
        sceneTransition = Object.FindFirstObjectByType<SceneTransition>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player") && sceneTransition != null)
        {
            hasTriggered = true;

            if (enterSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(enterSound);
            }

            StartCoroutine(DelayedSceneLoad());
        }
    }

    private System.Collections.IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(0.3f);
        sceneTransition.FadeToScene(sceneToLoad);
    }
}