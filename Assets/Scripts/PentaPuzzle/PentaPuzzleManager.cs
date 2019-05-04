using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PentaPuzzleManager : MonoBehaviour, IBoardGame
{
    [CanBeNull] private Action<string> _wordActivationCallback;

    private PoolOfAll _pool;
    private ScrollManager _scrollManager;
    
    private string _currentWord;

    private void Awake()
    {
        _pool = GetComponentInChildren<PoolOfAll>();
        _scrollManager = GetComponentInChildren<ScrollManager>();

        if (!_pool || !_scrollManager) { Debug.LogError("PuzzleManager couldn't find something."); }

        Pentagram[] pents = FindObjectsOfType<Pentagram>();
        foreach (Pentagram pent in pents)
        { pent.AddSpellActivatedCallBack(OnWordActivation); }
    }


    public void StartBoardGame()
    {
        _scrollManager.PreparePentagram(ScrollManager.PentagramPosition.Front);
    }

    public void EndBoardGame()
    {
        this.gameObject.SetActive(false);
    }

    public void SetWordActivationCallback(Action<string> callback)
    { _wordActivationCallback += callback; }

    public void OnWordActivation(string word)
    { _wordActivationCallback(word); }
}
