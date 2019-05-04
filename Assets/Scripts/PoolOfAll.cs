using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolOfAll : MonoBehaviour
{
    [SerializeField] private List<PentaLetter> _letters;

    protected void Start()
    {
        if (_letters.Capacity == 0)
        { Debug.LogError("Capacity of Letters pool is 0."); }
    }

    public PentaLetter GetLetter()
    {
        PentaLetter item = _letters[0];
        _letters.RemoveAt(0);
        return item;
    }

    public void Store (PentaLetter letter)
    { _letters.Add(letter); }
}
