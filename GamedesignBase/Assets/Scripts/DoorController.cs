using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        Debug.Log("Tür öffnet sich!");
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false); // Optional: Objekt komplett deaktivieren
    }
}