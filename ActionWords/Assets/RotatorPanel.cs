using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatorPanel : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            TetrinoDraggable tetr = eventData.pointerDrag
                                    .GetComponent<TetrinoDraggable>();
            if (tetr != null) { tetr.Rotate(); }
        }
    }
}
