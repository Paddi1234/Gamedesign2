using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public DragAndDropManager manager;

    public void OnDrop(PointerEventData eventData)
    {
        DragItemMover item = eventData.pointerDrag.GetComponent<DragItemMover>();
        if (item == null) return;

        if (item.itemType == "O2")
        {
            manager.AddScore(1);
        }
        else
        {
            manager.AddScore(-2);
        }

        item.MarkAsCollected();
    }
}

