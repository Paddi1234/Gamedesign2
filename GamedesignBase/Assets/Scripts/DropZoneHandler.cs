using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneHandler : MonoBehaviour, IDropHandler
{
    public DragAndDropManager manager;

    public void OnDrop(PointerEventData eventData)
    {
        DragItemMover item = eventData.pointerDrag.GetComponent<DragItemMover>();
        if (item == null || manager == null) return;

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
