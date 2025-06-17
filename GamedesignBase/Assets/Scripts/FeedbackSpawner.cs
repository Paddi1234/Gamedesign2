using UnityEngine;

public class FeedbackSpawner : MonoBehaviour
{
    public GameObject feedbackTextPrefab;

    public void SpawnFeedback(string message, Color color, Vector3 worldPos)
    {
        if (feedbackTextPrefab == null) return;

        GameObject go = Instantiate(feedbackTextPrefab, worldPos, Quaternion.identity, transform);
        FloatingFeedbackText text = go.GetComponent<FloatingFeedbackText>();
        if (text != null)
        {
            text.Setup(message, color);
        }
    }
}
