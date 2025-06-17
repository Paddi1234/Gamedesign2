using UnityEngine;
using UnityEngine.UI;

public class CharacterFaceController : MonoBehaviour
{
    public Image characterImage;
    public Sprite erythNeutral;
    public Sprite erythHappy;
    public Sprite erythSad;

    private bool isSad = false;

    public void SetEmotion(bool isHit)
    {
        if (characterImage == null) return;

        if (isHit)
        {
            characterImage.sprite = erythHappy;
            isSad = false;
            CancelInvoke("BackToNeutral");
            Invoke("BackToNeutral", 1.2f); // happy nur kurz zeigen
        }
        else
        {
            characterImage.sprite = erythSad;
            isSad = true;
            // bleibt bis ein Hit kommt
        }
    }

    public void SetNeutralFace()
    {
        if (!isSad && characterImage != null)
        {
            characterImage.sprite = erythNeutral;
        }
    }

    private void BackToNeutral()
    {
        if (!isSad)
        {
            characterImage.sprite = erythNeutral;
        }
    }
}
