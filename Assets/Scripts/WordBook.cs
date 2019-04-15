using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class WordBook : MonoBehaviour
{
    HashTable _table;

    // [SerializeField]
    //private List<KeyValuePair<char, double>> _letters;
    private IOrderedEnumerable<KeyValuePair<char, double>> _letters;

    // file extension is not needed
    [SerializeField] private string _wordsFilePath = "russian_nouns";

    void Awake()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(_wordsFilePath);
        _table = new HashTable(50, textAsset);
        
        string text = textAsset.text;
        text = Regex.Replace(text, @"\t|\n|\r|-", "");
        _letters = text.Where(c => Char.IsLetter(c))
                       .GroupBy(c => c)
                       .ToDictionary(g => g.Key, g => (double)g.Count() / text.Length)
                       .OrderBy(key => key.Value);

        /*
        Debug.Log("Dictionary count: " + _letters.Count());
        
        foreach (KeyValuePair<char, double> kvp in _letters)
        {
            Debug.Log("Key = " + kvp.Key + ", Value = "  + kvp.Value);
        } 
        */
    }

    public bool Contains (string word)
    {
        return _table.Check(word);
    }

    public char GetRandLetter(double diceRoll)
    {
        char selectedElement = 'о';

        double cumulative = 0.0;
        foreach (KeyValuePair<char, double> kvp in _letters)
        {
            cumulative += kvp.Value;
            if (diceRoll < cumulative)
            {
                // Debug.Log(cumulative);
                selectedElement = kvp.Key;
                // Debug.Log(selectedElement);
                break;
            }
        }
        return selectedElement;
    }

    public char[] GetLetters(int n)
    {
        char[] letters = new char[n];

        System.Random r = new System.Random();
        double diceRoll;

        for (int i = 0; i < n; i++)
        {
            diceRoll = (double)r.Next(0, 100)/100;
            // Debug.Log(diceRoll);
            letters[i] = GetRandLetter(diceRoll);
        }

        return letters;


    }
}
