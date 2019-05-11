using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PentaLoader : MonoBehaviour
{
    private string _pentagramsFilePath = "levels";

    private int _pentaCounter;
    private Pentagram[] _pentagrams;

    private void Awake()
    {
        _pentaCounter = 0;
        LoadPentagrams();
    }

    private void LoadPentagrams()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(_pentagramsFilePath);
        StringReader sr = new StringReader(textAsset.text);

        string[] lineSeparators = { "\n\r", "\n" }; // UNNECESSARY READING OF THE WHOLE FILE HERE.
        string[] wordSeparators = { ", " };         // THINK OF ANOTHER SOLUTION!
        string[] lines = sr.ReadToEnd().Split(lineSeparators, System.StringSplitOptions.RemoveEmptyEntries);

        int n = lines.Length;
        _pentagrams = new Pentagram[n];

        string[] row;
        int      nLetters;
        char[]   letters;
        string[] words;
        for (int i = 0; i < n; i++)
        {
            // LINE IS SPLIT IN TOO MANY STRINGS. SOLUTION CAN BE WAY MORE ELEGANT!
            row = lines[i].Split(wordSeparators, System.StringSplitOptions.RemoveEmptyEntries);

            nLetters = int.Parse(row[0]);
            letters = new char[nLetters];
            for (int j = 0; j < nLetters; j++)  { letters[j] = row[j + 1][0]; }

            int nWords = row.Length - nLetters - 1;
            words = new string[nWords];
            for (int j = 0; j < nWords; j++)    { words[j] = row[j + nLetters + 1]; }

            _pentagrams[i] = new Pentagram(letters, words);
        }
    }

    public Pentagram GetNextPentagram()
    {
        return _pentagrams[_pentaCounter++];
    }

    public bool OutOfPentagrams()
    {
        return (_pentaCounter >= _pentagrams.Length);
    }
}
