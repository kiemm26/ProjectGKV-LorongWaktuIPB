using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoHotspot : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject contentSimple;
    public GameObject contentList;
    public TextMeshProUGUI contentText;
    public Image[] contentImages;

    [TextArea(5, 15)]
    public string infoText;
    public Sprite[] infoImages;

    public static bool IsPopupOpen = false;

    public ScrollRect scrollRect;

    public void ShowInfo()
    {
        if (IsPopupOpen) return;

        infoPanel.SetActive(true);
        contentSimple.SetActive(true);
        contentList.SetActive(false);

        scrollRect.content = contentSimple.GetComponent<RectTransform>();
        scrollRect.verticalNormalizedPosition = 1f; // reset scroll ke atas

        contentText.text = infoText;

        for (int i = 0; i < contentImages.Length; i++)
        {
            if (i < infoImages.Length && infoImages[i] != null)
            {
                contentImages[i].sprite = infoImages[i];
                contentImages[i].gameObject.SetActive(true);
            }
            else
            {
                contentImages[i].gameObject.SetActive(false);
            }
        }

        IsPopupOpen = true;
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        IsPopupOpen = false;
    }
}