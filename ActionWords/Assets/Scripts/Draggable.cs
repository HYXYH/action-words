using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
     
    private Vector3 delta_;

    private CanvasGroup canvasGroup_;
    
    protected void Start ()
    {
        canvasGroup_ = GetComponent<CanvasGroup>();
    }

    virtual public void OnDrag (PointerEventData eventData)
    {
        this.transform.position = (Vector3)eventData.position + delta_;
    }

    virtual public void OnBeginDrag (PointerEventData eventData)
    {
        delta_ = this.transform.position - (Vector3)eventData.pressPosition;
        canvasGroup_.blocksRaycasts = false;
    }

    virtual public void OnEndDrag (PointerEventData eventData)
    {
        canvasGroup_.blocksRaycasts = true;
    }
}
