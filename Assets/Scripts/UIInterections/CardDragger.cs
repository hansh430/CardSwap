using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    private bool isPicked = false;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Card card;
    private Vector2 lastPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        card = GetComponent<Card>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        if (isPicked)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
        lastPos = rectTransform.anchoredPosition;
        isPicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
        isPicked = false;
        rectTransform.anchoredPosition = lastPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        IDropable idrop;
        foreach (var result in results)
        {
            idrop = result.gameObject.GetComponent<IDropable>();
            if (idrop != null)
            {
                Debug.Log("sas");
                card.GetGroup().RemoveCard(card);
                idrop.OnDrop(card);
            }
        }
    }
}
