using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pentagram : MonoBehaviour
{



    private PoolOfAll _pool;

    public Action<string> _SpellActivated;


    private char[] _letters;
    private string[] _intendedWords;
    private PentaLetter[] _pentaLetters;

    private List<PentaLetter> _selectedLetters;
    private string _currentWord;


    
    public void AddSpellActivatedCallBack(Action<string> callback)
    { _SpellActivated += callback; }

    public void OnSpellActivated()
    { _SpellActivated(_currentWord); }


    public void Awake()
    {
        _selectedLetters = new List<PentaLetter>();


        _pool = FindObjectOfType<PoolOfAll>();
    }
    
    public void Create(char[] letters, string[] words, float radius)
    {
        _letters = (char[])letters.Clone();
        _intendedWords = (string[])words.Clone();

        Debug.LogWarning(name + "Word1: " + _intendedWords[0] + "Word2: " + _intendedWords[1]);

        int nLetters = letters.Length;
        float turningAngle = 2 * Mathf.PI / nLetters;
        float stepVectorLength = 2 * radius * Mathf.Sin(turningAngle / 2);

        Debug.Log("Radius: " + radius + "\nStep: " + stepVectorLength);

        Vector2 letterPosition = new Vector2(0, radius);
        Vector2 stepVector = new Vector2(0, -stepVectorLength);
        RotateVector(ref stepVector, (turningAngle - Mathf.PI) / 2);

        _pentaLetters = new PentaLetter[nLetters];
        for (int i = 0; i < nLetters; i++)
        {
            _pentaLetters[i] = _pool.GetLetter();
            _pentaLetters[i].Construct(letters[i]);


            RectTransform rt = _pentaLetters[i].GetComponent<RectTransform>();
            rt.SetParent(this.transform);
            rt.anchoredPosition3D = new Vector3(letterPosition.x, letterPosition.y, 0);
            letterPosition = letterPosition + stepVector;


            _pentaLetters[i].AddDragEndedCallback(TryActivate);
            _pentaLetters[i].AddLetterSelectedCallback(SelectLetter);

            RotateVector(ref stepVector, turningAngle);
            Debug.Log("StepVector: " + stepVector);
        }
    }

    private void SelectLetter(PentaLetter letter)
    {
        _selectedLetters.Add(letter);
        _currentWord += letter.GetLetter();
    }

    private void UnselectLetters()
    {
        foreach (PentaLetter letter in _selectedLetters)
        { letter.Unselect(); }
        _selectedLetters.Clear();

        _currentWord = "";
    }

    public void TryActivate(PentaLetter lastSelectedLetter)
    {
        Debug.Log("Word is " + _currentWord);
        bool wordExists = false;
        foreach (string word in _intendedWords)
        {
            Debug.LogWarning("Checking for word " + word);
            if (word.ToLower().Equals(_currentWord.ToLower()))
            { wordExists = true; }
        }

        if (wordExists) {
            Debug.Log("There is word " + _currentWord);
            OnSpellActivated();
        }
        UnselectLetters();
    }



    private void RotateVector(ref Vector2 vector, float angle)
    {
        Debug.Log("Angle in degs: " + angle * Mathf.Rad2Deg);
        float newX = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float newY = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);

        vector.x = newX;
        vector.y = newY;
    }
}
