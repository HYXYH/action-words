using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    private char[]      _letters;   public char[] Letters() { return _letters; }
    private string[]    _words;

    public Pentagram(char[] letters, string[] words)
    {
        _letters = letters;
        _words = words;
    }

    public bool HasWord(string wordToCheck)
    {
        bool wordExists = false;
        wordToCheck = wordToCheck.ToLower();
        Debug.Log("Checking for word " + wordToCheck);
        foreach (string word in _words) {
            Debug.Log("Comparing with " + word);
            if (word.ToLower().Equals(wordToCheck)) {
                wordExists = true;
                break;
            }
        }
        return wordExists;
    }
}
