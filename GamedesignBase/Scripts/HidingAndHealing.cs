using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HidingAndHealing : MonoBehaviour
{
    [Header("Healing Settings")]
    public float healDuration = 3f;
    public Slider healProgressBar;
    public Image healFillImage; // Fill Image des Sliders für Farbwechsel

    [Header("UI")]
    public GameObject healCompleteMessage; // UI Nachricht „Heilung abgeschlossen“
    public float messageDisplayTime = 2f;

    [Header("Portal")]
    public GameObject portal; // Portal GameObject, zunächst deaktiviert

    [Header("Hiding Settings")]
    public GameObject playerVisual;
    public LayerMask hiddenLayer;
    public LayerMask normalLayer;

    private bool isInHealZone = false;
    private bool isInHideZone = false;
    private float healProgress = 0f;
    private bool isHiding = false;

    // Maustasten: 0 = Links (heilen), 1 = Rechts (verstecken)
    private int healMouseButton = 0;
    private int hideMouseButton = 1;

    void Update()
    {
        HandleHealing();
        HandleHiding();
    }

    void HandleHealing()
    {

        if (isInHealZone && Input.GetMouseButton(healMouseButton))
        {
            // Fortschritt erhöhen beim Heilen
            healProgress += Time.deltaTime;
            healProgress = Mathf.Clamp(healProgress, 0f, healDuration);
        }
        else
        {
            // Fortschritt langsam verringern, wenn nicht geheilt wird
            healProgress -= Time.deltaTime * 0.5f; // Geschwindigkeit des Absinkens anpassen
            healProgress = Mathf.Clamp(healProgress, 0f, healDuration);
        }

        // UI aktualisieren
        healProgressBar.value = healProgress / healDuration;
        healFillImage.color = Color.Lerp(Color.red, Color.green, healProgress / healDuration);

        if (healProgress >= healDuration)
        {
            HealPlayer();
            healProgress = 0f;
        }
    }

    void HealPlayer()
    {
        Debug.Log("Spieler wurde geheilt.");

        if (healCompleteMessage != null)
        {
            StartCoroutine(ShowHealCompleteMessage());
        }

        if (portal != null)
        {
            portal.SetActive(true);
        }
    }

    IEnumerator ShowHealCompleteMessage()
    {
        healCompleteMessage.SetActive(true);
        yield return new WaitForSeconds(messageDisplayTime);
        healCompleteMessage.SetActive(false);
    }

    void HandleHiding()
    {
        if (isInHideZone && Input.GetMouseButtonDown(hideMouseButton))
        {
            isHiding = !isHiding;

            if (playerVisual != null)
            {
                playerVisual.SetActive(!isHiding);
            }

            gameObject.layer = isHiding ? LayerMask.NameToLayer("Hidden") : LayerMask.NameToLayer("Player");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealZone"))
        {
            isInHealZone = true;
        }
        else if (other.CompareTag("HideZone"))
        {
            isInHideZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HealZone"))
        {
            isInHealZone = false;
            // Kein Reset mehr hier!
        }
        else if (other.CompareTag("HideZone"))
        {
            isInHideZone = false;
            if (isHiding)
            {
                isHiding = false;
                if (playerVisual != null)
                    playerVisual.SetActive(true);
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }
}