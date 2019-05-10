using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using JetBrains.Annotations;
using UnityEngine;

public class BoardGame : MonoBehaviour, IBoardGame
{
    [CanBeNull] private Action<string, SpellEffect> _wordActivationCallback;
    [CanBeNull] private Action _tetrinoUsedCallback;
    [CanBeNull] private Action _noMovesCallback;

    public void StartBoardGame()
    {
        gameObject.SetActive(true);
    }

    public void EndBoardGame()
    {
        gameObject.SetActive(false);
        
        // todo: clear board
    }


    public void SetWordActivationCallback(Action<string, SpellEffect> callback)
    {
        _wordActivationCallback += callback;
    }

    // todo: call this method in WordInput.Activate
    public void OnWordActivation() { }
    public void CallWordActivationCallback(string word, List<Thaum> thaums)
    {
        if (_wordActivationCallback != null)
        {
            _wordActivationCallback(word, SpellEffect.None);
        }
        else
        {
            Debug.Log("_wordActivationCallback is null!");
        }
    }

}
