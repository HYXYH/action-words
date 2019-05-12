using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public Action<string, int[]>   _SpellActivated;
    public Action<int>      _ThrownAway;
    
    private PoolOfAll _pool;
    private Animator _anim;

    [SerializeField]
    private Text _wordsSelectedText;            public Text GetWordsSelectedCounter() { return _wordsSelectedText; }

    private Pentagram _pentagram;               public Pentagram GetPentagram() { return _pentagram; }
    private PentaLetter[] _pentaLetters;


    private List<PentaLetter> _selectedLetters;
    private string _currentWord;                    public string GetSelectedWord() { return _currentWord; }
    private int[] _letterNumbersSequence;

    
    public void AddSpellActivatedCallBack(Action<string, int[]> callback)
    { _SpellActivated += callback; }

    public void OnSpellActivated()
    {
        GetLetterNumbersSequence();
        _SpellActivated(_currentWord, _letterNumbersSequence);
    }


    public void AddThrownAwayCallback(Action<int> callback)
    { _ThrownAway += callback; }

    public void OnThrownAway()
    { _ThrownAway(0); }


    public void Awake()
    {
        _letterNumbersSequence = new int[16];
        _anim = GetComponent<Animator>();
        _selectedLetters = new List<PentaLetter>();
        _pool = FindObjectOfType<PoolOfAll>();
    }


    public void Load(Pentagram pentagram, float radius, float letterSize)
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
            _pentaLetters[i].Construct(pentagram.Letters()[i], nLetters);


            RectTransform rt = _pentaLetters[i].GetComponent<RectTransform>();
            rt.SetParent(this.transform);


            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, letterSize);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, letterSize);

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
        _currentWord += letter.GetLetter();
    }

    public void UnselectLetters()
    {
        foreach (PentaLetter letter in _selectedLetters)
        { letter.Unselect(); }
        _selectedLetters.Clear();

        _currentWord = "";
    }

    public void TryActivate(PentaLetter lastSelectedLetter)
    {
        bool wordIsSelectable = _pentagram.TryToUseWord(_currentWord);
        if (wordIsSelectable) {
            OnSpellActivated();
        }
        UnselectLetters();
    }

    
    private int[] GetLetterNumbersSequence()
    {
        _letterNumbersSequence = new int[_currentWord.Length];

        
        for (int i = 0; i < _letterNumbersSequence.Length; i++)
        {
            for (int j = 0; j < _pentaLetters.Length; j++)
            {
                if (_selectedLetters[i] == _pentaLetters[j])
                {
                    _letterNumbersSequence[i] = j;
                    break;
                }
            }
        }

        return _letterNumbersSequence;
    }


    private void RotateVector(ref Vector2 vector, float angle)
    {
        float newX = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float newY = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);

        vector.x = newX;
        vector.y = newY;
    }



    public IEnumerator SelectWord(string word)
    {
        bool letterFound = false;
        PentaLetter previousLetter = null;
        word = word.ToLower();

        foreach (char letter in word)
        {
            letterFound = false;
            foreach (PentaLetter pentaLetter in _pentaLetters)
            {
                if (pentaLetter.GetLetter() == letter)
                {
                    letterFound = true;
                    bool nextLetterIsSelectable = pentaLetter.TryToSelect(previousLetter);
                    yield return new WaitForSecondsRealtime(0.3f);
                    if (!nextLetterIsSelectable) { yield break; }

                    previousLetter = pentaLetter;
                    break;
                }
            }
            if (!letterFound) yield break;
        }
    }
}
