using UnityEngine;
using TMPro;

public class RhythmFeedback : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public float displayTime = 0.5f;

    public void ShowFeedback(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        CancelInvoke();
        Invoke("ClearText", displayTime);
    }

    void ClearText()
    {
        feedbackText.text = "";
    }
}
