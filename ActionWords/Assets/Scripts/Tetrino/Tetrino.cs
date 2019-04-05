using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tetrino : MonoBehaviour
{
    public enum Type
    {
        T1,
        T2,
        T3,
        T4,
        T5
    }

    /*
    protected void Construct(int nLetters, char[] letters, Thaum.Type[] types, Transform transform)
    {
        _transform = transform;
        TakeBlocks(nLetters, letters, types);
    }
    */

    public Tetrino (int nLetters, char[] letters, Thaum.Type[] types, Transform transform)
    {
        _transform = transform;
        TakeBlocks(nLetters, letters, types);
    }

    [SerializeField]
    private BlockPool _blockPool;

    protected LetterBlock[] _blocks;
    private Transform _transform;

    public abstract void Construct();

    protected virtual void TakeBlocks (int nLetters, char[] letters, Thaum.Type[] types)
    {
        if (letters.Length < nLetters)
        { Debug.LogError("Not enough letters!"); }

        if (types.Length < nLetters)
        { Debug.LogError("Not enough thaums!!"); }

        _blockPool = FindObjectOfType<BlockPool>();
        if (_blockPool == null)
        { Debug.LogError("Scene didn't have a BlockPool object."); }
        
        _blocks = new LetterBlock[nLetters];
        
        for (int i = 0; i < nLetters; i++)
        {
            _blocks[i] = _blockPool.Get();
            _blocks[i].transform.SetParent(_transform);
            _blocks[i].Construct(letters[i], types[i]);
        }
    }

    
    
    public bool TakeAim()
    {
        bool placeable = true;

        foreach (LetterBlock block in _blocks)
        {
            block.FindAimedCell();

            if (!block.CanBePlaced())
                placeable = false;
        }

        return placeable;
    }

    public void PlaceBlocks()
    {
        foreach (LetterBlock block in _blocks)
        {
            block.StayAtCell();
        }
        FindObjectOfType<TetrinoPlayerSet>().GiveNewTetrino();
    }
}
