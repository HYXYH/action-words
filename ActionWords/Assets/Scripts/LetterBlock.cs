using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBlock : MonoBehaviour
{
    [SerializeField]    private Thaum.Type  _thaum;
    [SerializeField]    private char        _letter;

    public Thaum.Type Thaum()  { return _thaum; }
    public char       Letter() { return _letter; }

    private Text _text;

    [SerializeField] private Table _table;
    private TableCell _aimedCell;
    private Animator _anim;

    void Awake()
    {
        _table = FindObjectOfType<Table>();
        _text = GetComponentInChildren<Text>();

        _letter = _text.text[0];

        _anim = GetComponent<Animator>();
    }

    public void Construct (char letter, Thaum.Type thaum)
    {
        _letter = letter;
        _thaum  = thaum;

        _text.text = "" + letter;
    }


    public TableCell FindAimedCell ()
    {
        _aimedCell = _table.GetAimedCell(this.gameObject);
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
