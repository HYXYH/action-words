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

    private int             _nNeighbourLetters;
    private PentaLetter[]   _neighbourLetters;

    private Action< PentaLetter > _LetterSelected;
    private Action< PentaLetter > _DragEnded;

    void Awake()
    {
        _manager = FindObjectOfType<PentaPuzzleManager>();
        _liner = FindObjectOfType<Liner>();
        _text  = GetComponent<Text>();
    }


    public void Construct (char letter, int lettersInPentagram)
    {
        _letter = letter;
        _text.text = "" + letter;

        _neighbourLetters = new PentaLetter[lettersInPentagram - 1];
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
        TryToSelect(null);
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
        if (eventData.dragging && TryToSelect(eventData.pointerDrag.GetComponent<PentaLetter>()))
        {
            eventData.pointerDrag = this.gameObject;
        }
    }

    private bool TryToSelect(PentaLetter previousLetter)
    {
        if (previousLetter != null)
        {
            if (!previousLetter.AddToNeighbours(this))
            { return false; }
            AddToNeighbours(previousLetter);
        }
        OnLetterSelected();
        return true;
    }

    public void Unselect()
    {
        _nNeighbourLetters = 0;
    }

    public bool AddToNeighbours(PentaLetter letter)
    {
        for (int i = 0; i < _nNeighbourLetters; i++)
        {
            if (_neighbourLetters[i] == letter)
            {
                Debug.Log(this._letter + " had " + letter._letter + " as neighbour.");
                return false;
            }
        }

        Debug.Log(this._letter + " added " + letter._letter + " to neighbours.");
        _neighbourLetters[_nNeighbourLetters++] = letter;
        return true;
    }
}
