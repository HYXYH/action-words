using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBlock : MonoBehaviour
{
    [SerializeField] private Thaum.Type  _thaum;
    [SerializeField] private char        _letter;
    public Thaum.Type Thaum()  { return _thaum; }
    public char       Letter() { return _letter; }

    private Text _text;
    
    private TableCell _aimedCell;
    private Animator _anim;

//  ~~~~~~~~~~~~~~~~~~~~~~~~ All about rotation ~~~~~~~~~~~~~~~~~~~~~~~
    private Vector3 _stableLocalPosition;
    
    public void RememberLocalPosition()
    { _stableLocalPosition = transform.localPosition; }

    public void ResetLocalPosition()
    { transform.localPosition = _stableLocalPosition; }
//  ~~~~~~~~~~~~~~~~~~~~~~~~ Not about rotation ~~~~~~~~~~~~~~~~~~~~~~~


    void Awake()
    {
        _text = GetComponentInChildren<Text>();

        _anim = GetComponent<Animator>();
    }

    public void Construct (char letter, Thaum.Type thaum)
    {
        //_anim.SetTrigger(letter.ToString());
        _letter = letter;
        _thaum  = thaum;

        _anim.SetTrigger("NoLetter"); _text.text = "" + letter;
    }


    public TableCell FindAimedCell (Table table)
    {
        _aimedCell = table.GetAimedCell(this.transform.position);
        return _aimedCell;
    }

    public void StayAtCell ()
    {
        this.transform.position = _aimedCell.transform.position;
        _aimedCell.Take(this);
    }

    public bool CanBePlaced ()
    {
        return (_aimedCell == null) ? false : !_aimedCell.HasBlock();
    }

    
    public void Activate()
    {
        _anim.SetTrigger("Activated");
        _anim.SetBool("Selected", false);
    }

    // Called by Animator!
    public void StoreToPool()
    {
        BlockPool pool = FindObjectOfType<BlockPool>();
        pool.Store(this);

        this.transform.SetParent(pool.transform);
        this.transform.localPosition = Vector3.zero;
    }
}
