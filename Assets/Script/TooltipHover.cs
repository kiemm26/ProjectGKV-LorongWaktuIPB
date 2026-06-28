using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipObject != null)
            tooltipObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipObject != null)
            tooltipObject.SetActive(false);
    }
}