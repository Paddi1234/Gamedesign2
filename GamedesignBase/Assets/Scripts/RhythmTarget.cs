using UnityEngine;

public class RhythmTarget : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.Space;
    public float expandSpeed = 300f;
    public float maxRadius = 140f;
    public float destroyRadius = 160f;

    private RectTransform rect;
    private RhythmController controller;
    private bool hasBeenHit = false;

    public void Init(RhythmController ctrl)
    {
        controller = ctrl;
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = Vector2.zero;
    }

    void Update()
    {
        if (hasBeenHit) return;

        rect.sizeDelta += Vector2.one * expandSpeed * Time.deltaTime;

        if (Input.GetKeyDown(inputKey))
        {
            float radius = rect.sizeDelta.x * 0.5f;
            if (radius >= maxRadius - 20f && radius <= maxRadius + 20f)
            {
                hasBeenHit = true;
                controller.OnTargetHit(transform.position);
                Destroy(gameObject);
            }
        }

        if (rect.sizeDelta.x >= destroyRadius)
        {
            if (!hasBeenHit)
            {
                controller.OnTargetMiss(transform.position);
            }
            Destroy(gameObject);
        }
    }
}
