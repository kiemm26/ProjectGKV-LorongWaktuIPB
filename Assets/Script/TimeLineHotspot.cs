using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeLineHotspot : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject contentSimple;
    public GameObject contentList;
    public GameObject timeLineEntryPrefab;
    public TimeLineData[] datas;
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

        foreach (var data in datas)
        {
            GameObject entry = Instantiate(timeLineEntryPrefab, contentList.transform);
            entry.transform.Find("Image").GetComponent<Image>().sprite = data.image;
            entry.transform.Find("Caption").GetComponent<TextMeshProUGUI>().text = data.caption;
        }

        InfoHotspot.IsPopupOpen = true;
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        InfoHotspot.IsPopupOpen = false;
    }
}