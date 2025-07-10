using UnityEngine;
using UnityEngine.UI;

public class BeatVisual : MonoBehaviour
{
    public float scaleUp = 1.3f;
    public float scaleSpeed = 5f;

    private Vector3 originalScale;
    private bool pulse = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (pulse)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * scaleUp, Time.deltaTime * scaleSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * scaleSpeed);
        }
    }

    public void PulseOnce()
    {
        pulse = true;
        Invoke("ResetPulse", 0.1f);
    }

    void ResetPulse()
    {
        pulse = false;
    }
}
