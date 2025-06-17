using UnityEngine;
using TMPro;

public class FloatingFeedbackText : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public float floatSpeed = 40f;
    public float duration = 1.2f;
    private float timer;

    public void Setup(string message, Color color)
    {
        if (tmp == null) tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = message;
        tmp.color = color;
    }

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= duration) Destroy(gameObject);
    }
}
