using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RectorListHotspot : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject contentSimple;
    public GameObject contentList;
    public GameObject rectorEntryPrefab;
    public RectorData[] rectors;
    public ScrollRect scrollRect;

    public void ShowInfo()
    {
        if (InfoHotspot.IsPopupOpen) return;

        infoPanel.SetActive(true);
        contentSimple.SetActive(false);
        contentList.SetActive(true);

        scrollRect.content = contentList.GetComponent<RectTransform>();
        scrollRect.verticalNormalizedPosition = 1f; // reset scroll ke atas

        foreach (Transform child in contentList.transform)
            Destroy(child.gameObject);

        foreach (var rector in rectors)
        {
            GameObject entry = Instantiate(rectorEntryPrefab, contentList.transform);
            entry.transform.Find("RectorPhoto").GetComponent<Image>().sprite = rector.photo;
            entry.transform.Find("TextGroup/RectorName").GetComponent<TextMeshProUGUI>().text = rector.name;
            entry.transform.Find("TextGroup/RectorBio").GetComponent<TextMeshProUGUI>().text = rector.bio;
        }

        InfoHotspot.IsPopupOpen = true;
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        InfoHotspot.IsPopupOpen = false;
    }
}