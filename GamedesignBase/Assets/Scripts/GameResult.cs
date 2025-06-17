using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemType; // "O2", "CO2", "Bakterie"

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = transform.position;
    }

    void Start()
    {
        InvokeRepeating("MoveRandomly", 1f, 1.5f);
    }

    void MoveRandomly()
    {
        float x = Random.Range(-400f, 400f);
        float y = Random.Range(-200f, 200f);
        rectTransform.anchoredPosition = new Vector2(x, y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CancelInvoke("MoveRandomly");
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
        transform.position = originalPosition;
        InvokeRepeating("MoveRandomly", 1f, 1.5f);
    }

    public void MarkAsCollected()
    {
        gameObject.SetActive(false);
        CancelInvoke("MoveRandomly");
    }
}
