using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolOfAll : MonoBehaviour
{
    [SerializeField] private List<PentaLetter>  _letters;
    [SerializeField] private Transform         _lettersHolder;

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
    {
        _letters.Add(letter);
        letter.transform.SetParent(_lettersHolder);
        letter.transform.position = Vector2.zero;
        //letter.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        //letter.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    public void Store (PentaLetter[] letters)
    {
        int n = letters.Length;
        for (int i = 0; i < n; i++) Store(letters[i]);
    }
}
