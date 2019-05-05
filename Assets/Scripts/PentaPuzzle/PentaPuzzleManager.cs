using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PentaPuzzleManager : MonoBehaviour, IBoardGame
{
    [CanBeNull] private Action<string> _wordActivationCallback;

    [SerializeField] private GameObject _changeScrollButton;

    private PentaLoader _pentaLoader;
    private PoolOfAll _pool;
    private ScrollManager _scrollManager;

    //private int _pentagramCounter;
    //private Pentagram[] _pentagrams;

    private void Awake()
    {
        //_pentagramCounter = 0;

        _pentaLoader = GetComponent<PentaLoader>();
        _pool = GetComponentInChildren<PoolOfAll>();
        _scrollManager = GetComponentInChildren<ScrollManager>();

        if (!_pool || !_scrollManager) { Debug.LogError("PuzzleManager couldn't find something."); }

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
        NextPentagram();
    }

    public void EndBoardGame()
    {
        this.gameObject.SetActive(false);
    }

    public void SetWordActivationCallback(Action<string> callback)
    { _wordActivationCallback += callback; }

    public void OnWordActivation(string word)
    { _wordActivationCallback(word); }


    public void NextPentagram()
    {
        /*
        _scrollManager.ChangeScroll(_pentagrams[_pentagramCounter]);
        _pentagramCounter++;

        if (_pentagramCounter == _pentagrams.Length)    { _changeScrollButton.SetActive(false); }
        */

        _scrollManager.ChangeScroll(_pentaLoader.GetNextPentagram());
        if (_pentaLoader.OutOfPentagrams()) { _changeScrollButton.SetActive(false); }
    }
}
