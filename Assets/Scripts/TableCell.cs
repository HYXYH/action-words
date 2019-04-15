using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TableCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,
                                        IPointerEnterHandler, IPointerDownHandler
{
    private bool _selected;

    private bool _hasBlock;
    public  bool  HasBlock() { return _hasBlock; }

    private LetterBlock _block;
    private Thaum.Type  _thaum;
    private char        _letter;

    public  char         Letter() { return _letter; }

    private WordInput _input;
    

    void Start ()
    {
        _input = FindObjectOfType<WordInput>();
        if (_input == null) { Debug.LogError("Couldn't find WordInput on scene."); }
    }

    public void Take (LetterBlock block)
    {
        _block  = block;
        _letter = block.Letter();
        _thaum  = block.Thaum();

        _block.transform.SetParent(this.transform);
        _hasBlock  = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _input.EnableInputMode();
        Select();
    }

    private void Select()
    {
        if (_hasBlock && !_selected && _input.InputModeEnabled())
        {
            _selected = true;
            Animator anim = _block.GetComponent<Animator>();
            anim.SetBool("Selected", true);

            _input.TakeLetter(this);
        }
    }

    public void Unselect ()
    {
        _selected = false;
        if (_block == null) Debug.LogError("NO BLOCK!");
        Animator anim = _block.GetComponent<Animator>();
        anim.SetBool("Selected", false);
    }

    public void Activate ()
    {
        _selected = false;

        _block.Activate();
        
        _hasBlock = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _input.EnableInputMode();
        Select();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _input.Activate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            Select();
        }
    }
}
