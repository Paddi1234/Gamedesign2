using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObjectDaD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    public string correctDropTag;

    void Start()
    {
        startPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag(correctDropTag))
            {
                GameManagerDaD.Instance.IncreaseScore();
                Destroy(gameObject);
                return;
            }
        }

        transform.position = startPosition;
    }
}
