using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBook : MonoBehaviour
{
    HashTable _table;

    [SerializeField]
    string _letters;

    [SerializeField]
    string _wordsFilePath;

    void Start()
    {
        _table = new HashTable(50, _wordsFilePath);
    }

    public bool Contains (string word)
    {
        return _table.Check(word);
    }

    public char GetRandLetter()
    {
        int i = Random.Range(0, _letters.Length);
        return _letters[i];
    }

    public char[] GetLetters(int n)
    {
        char[] letters = new char[n];
        for (int i = 0; i < n; i++)
        {
            letters[i] = GetRandLetter();
        }

        return letters;
    }
}
