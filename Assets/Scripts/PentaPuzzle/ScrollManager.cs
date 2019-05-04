using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public enum PentagramPosition
    {
        Front,
        Back
    }

    [SerializeField] private ScrollPlacement[] _scrolls;
    
    void Start()
    {
        
    }

    public void Swap()
    {
        Pentagram p0 = _scrolls[0].GetPentagram();
        Pentagram p1 = _scrolls[1].GetPentagram();

        _scrolls[0].SetPentagram(p1);
        _scrolls[1].SetPentagram(p0);
    }

    public void PreparePentagram(PentagramPosition pentagramPosition)
    {
        Pentagram pentagram = _scrolls[(int)pentagramPosition].GetPentagram();

        char[] letters = { 'к', 'о', 'т', 'л', 'о', 'л' };
        string[] words = { "кот", "кто", "ток", "ок", "отк" };
        pentagram.Create(letters, words, 400f);
    }

    public Pentagram GetActivePentagram()
    {
        return _scrolls[0].GetPentagram();
    }
}
