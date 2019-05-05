using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PentaLetter : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler
{

    private PentaPuzzleManager _manager;
    private Liner _liner;
    private Text  _text;

    private char _letter;   public char GetLetter() { return _letter; }

    private bool _selectable;

    private Action< PentaLetter > _LetterSelected;
    private Action< PentaLetter > _DragEnded;

    void Awake()
    {
        _selectable = true;
        _manager = FindObjectOfType<PentaPuzzleManager>();
        _liner = FindObjectOfType<Liner>();
        _text  = GetComponent<Text>();
    }


    public void Construct (char letter)
    {
        _letter = letter;
        _text.text = "" + letter;
    }


    public void AddLetterSelectedCallback(Action<PentaLetter> action)
    { _LetterSelected += action; }

    public void RemoveLetterSelectedCallback(Action<PentaLetter> action)
    { _LetterSelected -= action; }

    public void OnLetterSelected()
    { _LetterSelected(this); }


    public void AddDragEndedCallback(Action<PentaLetter> action)
    { _DragEnded += action; }

    public void RemoveDragEndedCallback(Action<PentaLetter> action)
    { _DragEnded -= action; }

    public void OnDragEnded()
    { _DragEnded(this); }


    private void Update()
    {
        RectTransform rt = GetComponent<RectTransform>();
        //rt.anchoredPosition.Set(0, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        TryToSelect();
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _DragEnded(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            eventData.pointerDrag = this.gameObject;
            TryToSelect();
        }
    }

    private void TryToSelect()
    {
        Debug.Log(this.name + " position: " + this.transform.position);
        if (_selectable)
        {
            _selectable = false;
            OnLetterSelected();
        }
    }

    public void Unselect()
    {
        _selectable = true;
    }

    public void SetSelectable (bool selectable)
    {
        _selectable = selectable;
    }
}
