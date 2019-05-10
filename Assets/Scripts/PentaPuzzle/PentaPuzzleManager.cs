﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PentaPuzzleManager : MonoBehaviour, IBoardGame
{
    

    private Action<string, SpellEffect> _wordActivationCallback;

    [SerializeField] private Animator _rewardAnimator;
    [SerializeField] private GameObject _changeScrollButton;

    private PentaLoader _pentaLoader;
    private PoolOfAll _pool;
    private ScrollManager _scrollManager;
    private Liner _liner;

    //private int _pentagramCounter;
    //private Pentagram[] _pentagrams;

    private void Awake()
    {
        //_pentagramCounter = 0;
        _liner = FindObjectOfType<Liner>();
        _pentaLoader = GetComponent<PentaLoader>();
        _pool = GetComponentInChildren<PoolOfAll>();
        _scrollManager = GetComponentInChildren<ScrollManager>();

        if (!_liner || !_pentaLoader || !_pool || !_scrollManager)
        { Debug.LogError("PuzzleManager couldn't find something."); }

        Scroll[] pents = FindObjectsOfType<Scroll>();
        foreach (Scroll pent in pents)
        { pent.AddSpellActivatedCallBack(OnWordActivation); }
    }


    public void StartBoardGame()
    {
        /*
        _pentagrams = new Pentagram[2];

        char[] letters = { 'к', 'о', 'т'};
        string[] words = { "кот", "кто", "ток", "ок", "отк" };
        _pentagrams[0] = new Pentagram(letters, words);

        char[] letters2 = { 'о', 'з', 'а', 'н'};
        string[] words2 = { "зона", "за", "он", "назо", "оз" };
        _pentagrams[1] = new Pentagram(letters2, words2);
        */
        NextScroll();
    }

    public void EndBoardGame()
    {
        this.gameObject.SetActive(false);
    }

    public void SetWordActivationCallback(Action<string, SpellEffect> callback)
    { _wordActivationCallback += callback; }

    public void OnWordActivation(string word, int[] letterNumbersSequence)
    {
        string[] s = { "Flame1", "Explosion" };
        if (word.Length >= 5)
        {
            int i = UnityEngine.Random.Range((int)0, 2);
            _rewardAnimator.SetTrigger(s[i]);
        }
        

        _wordActivationCallback(word, EffectOfSequence(word.Length, letterNumbersSequence));
    }


    private SpellEffect EffectOfSequence(int wordLength, int[] sequence)
    {
        // Less than 3 letters -> Stun
        if (wordLength <= 2) return SpellEffect.Stun;


        //Cycle -> Shield
        for (int i = 0; i < wordLength; i++)
        {
            for (int j = i+1; j < wordLength; j++)
            {
                if (sequence[i] == sequence[j])
                {
                    Debug.Log("SHIELD!");
                    return SpellEffect.Shield;
                }
            }
        }

        //None -> None
        return SpellEffect.None;
    }

    public void NextScroll()
    {
        _scrollManager.ChangeScroll(_pentaLoader.GetNextPentagram());
        if (_pentaLoader.OutOfPentagrams()) { _changeScrollButton.SetActive(false); }
    }


    public List<string> GetSelectableWords()
    {
        return _scrollManager
                .GetActiveScroll()
                .GetPentagram()
                .GetSelectableWords();
    }

    public IEnumerator EmulateWordActivation(string word)
    {
        Scroll activeScroll = _scrollManager.GetActiveScroll();
        yield return StartCoroutine(activeScroll.SelectWord(word));

        if (word != activeScroll.GetSelectedWord())
        {
            Debug.LogError("Somehow word \"" + word + " couldn't have been selected in a pentagram.");
            yield break;
        }

        activeScroll.GetPentagram().TryToUseWord(word);
        activeScroll.UnselectLetters();
        _liner.ClearNodes(null);
    }

    public void ZARUBA()
    {
        List<string> words = GetSelectableWords();
        if (words.Count > 0)
            StartCoroutine(EmulateWordActivation(words[0]));
        else
            NextScroll();
    }
}
