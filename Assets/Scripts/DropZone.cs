using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop (PointerEventData eventData)
    {
        AttachedDraggable c = eventData.pointerDrag.GetComponent<AttachedDraggable> ();
        if (c != null)
        {
            c.SetOwner(this.gameObject);
        }
        else Debug.LogError("No card");
    }

    public void OnPointerEnter (PointerEventData eventData)
    {

    }

    public void OnPointerExit (PointerEventData eventData)
    {

    }
}
