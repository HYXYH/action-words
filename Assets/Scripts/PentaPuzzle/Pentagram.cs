using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    private char[]      _letters;   public char[] Letters() { return _letters; }
    //private Dictionary<string, int> _words;
    private List<string> _words;    public List<string> GetSelectableWords() { return _words; }

    public Pentagram(char[] letters, string[] words)
    {
        //_words = new Dictionary<string, int>();
        _words = new List<string>();
        _letters = letters;
        foreach (string word in words)
        {
            //_words.Add(word.ToLower(), 0);
            _words.Add(word.ToLower());
        }
        _words.Sort();
    }

    public bool TryToUseWord(string word)
    {
        /*
        int timesWordIsUsed = 0;
        try
        {
            timesWordIsUsed = _words[wordToCheck]++;
        }
        catch(KeyNotFoundException)
        {
            timesWordIsUsed = -1;
        }
        return timesWordIsUsed;
        */
        bool wordCanBeUsed = _words.Remove(word);
        return wordCanBeUsed;
    }
}
