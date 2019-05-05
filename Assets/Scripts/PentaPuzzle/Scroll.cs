﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public Action<string>   _SpellActivated;
    public Action<int>      _ThrownAway;
    
    private PoolOfAll _pool;
    
    private Pentagram _pentagram;
    private PentaLetter[] _pentaLetters;

    private Animator _anim;

    private List<PentaLetter> _selectedLetters;
    private string _currentWord;


    
    public void AddSpellActivatedCallBack(Action<string> callback)
    { _SpellActivated += callback; }

    public void OnSpellActivated()
    { _SpellActivated(_currentWord); }


    public void AddThrownAwayCallback(Action<int> callback)
    { _ThrownAway += callback; }

    public void OnThrownAway()
    { _ThrownAway(0); }


    public void Awake()
    {
        _anim = GetComponent<Animator>();
        _selectedLetters = new List<PentaLetter>();
        _pool = FindObjectOfType<PoolOfAll>();
    }


    public void Load(Pentagram pentagram, float radius)
    {
        _pentagram = pentagram;

        int nLetters = pentagram.Letters().Length;
        float turningAngle = 2 * Mathf.PI / nLetters;
        float stepVectorLength = 2 * radius * Mathf.Sin(turningAngle / 2);

        Debug.Log("Radius: " + radius + "\nStep: " + stepVectorLength);

        Vector2 letterPosition = new Vector2(0, radius);
        Vector2 stepVector = new Vector2(0, -stepVectorLength);
        RotateVector(ref stepVector, (turningAngle - Mathf.PI) / 2);

        Debug.Log("Going to place " + nLetters + "letters.");
        _pentaLetters = new PentaLetter[nLetters];
        for (int i = 0; i < nLetters; i++)
        {
            Debug.Log("Placing letter №" + i + ": " + pentagram.Letters()[i]);
            _pentaLetters[i] = _pool.GetLetter();
            _pentaLetters[i].Construct(pentagram.Letters()[i]);


            RectTransform rt = _pentaLetters[i].GetComponent<RectTransform>();
            rt.SetParent(this.transform);
            rt.anchoredPosition3D = new Vector3(letterPosition.x, letterPosition.y, 0);
            letterPosition = letterPosition + stepVector;


            _pentaLetters[i].AddDragEndedCallback(TryActivate);
            _pentaLetters[i].AddLetterSelectedCallback(SelectLetter);

            RotateVector(ref stepVector, turningAngle);
        }
    }

    public void ReturnLettersToPool()
    {
        if (_pentaLetters == null || _pentaLetters.Length == 0) return;

        foreach (PentaLetter letter in _pentaLetters)
        {
            letter.RemoveLetterSelectedCallback(SelectLetter);
            letter.RemoveDragEndedCallback(TryActivate);
        }
        _pool.Store(_pentaLetters);
    }
    
    private void SelectLetter(PentaLetter letter)
    {
        _selectedLetters.Add(letter);
        //if (_selectedLetters.Count >= 3) _selectedLetters.
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
        if (_pentagram.HasWord(_currentWord)) {
            Debug.Log("There is word " + _currentWord);
            OnSpellActivated();
        }
        UnselectLetters();
    }

    
    private void RotateVector(ref Vector2 vector, float angle)
    {
        float newX = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float newY = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);

        vector.x = newX;
        vector.y = newY;
    }
}
