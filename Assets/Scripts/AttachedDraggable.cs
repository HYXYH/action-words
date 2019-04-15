using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttachedDraggable : Draggable
{
    protected bool _ownerChanged;
    private GameObject _owner;

    protected new void Start()
    {
        base.Start();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _ownerChanged = false;
        _owner = this.transform.parent.gameObject;
        this.transform.SetParent(this.transform.parent.parent.parent);
        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!_ownerChanged)
        {
            this.transform.SetParent(_owner.transform);
        }
        else
            _ownerChanged = false;
        base.OnEndDrag(eventData);
    }

    public void SetOwner (GameObject owner)
    {
        _owner = owner;
        this.transform.SetParent(owner.transform);
        this.transform.localPosition = Vector3.zero;
    }
}
