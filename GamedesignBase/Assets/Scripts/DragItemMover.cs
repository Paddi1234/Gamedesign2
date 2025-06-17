using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemType; // "O2", "CO2", "Bakterie"

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Vector2 movementDirection;
    public float speed = 50f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = transform.position;
    }

    void Start()
    {
        movementDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        if (canvasGroup.blocksRaycasts)
        {
            rectTransform.anchoredPosition += movementDirection * speed * Time.deltaTime;

            // Begrenzung innerhalb der Canvas
            RectTransform canvasRect = canvas.transform as RectTransform;
            Vector2 canvasSize = canvasRect.sizeDelta * 0.5f;
            Vector2 pos = rectTransform.anchoredPosition;

            if (Mathf.Abs(pos.x) > canvasSize.x)
            {
                movementDirection.x *= -1;
                pos.x = Mathf.Sign(pos.x) * canvasSize.x;
            }
            if (Mathf.Abs(pos.y) > canvasSize.y)
            {
                movementDirection.y *= -1;
                pos.y = Mathf.Sign(pos.y) * canvasSize.y;
            }

            rectTransform.anchoredPosition = pos;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void MarkAsCollected()
    {
        gameObject.SetActive(false);
    }
}
