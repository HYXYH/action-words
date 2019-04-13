using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrinoPlayerSet : MonoBehaviour
{
    [SerializeField]
    Pool<TetrinoDraggable> _pool;

    Table _table;

    WordBook _wb;

    private int _counter;


    void Awake()
    {
        _table = FindObjectOfType<Table>();
        _pool = FindObjectOfType<Pool<TetrinoDraggable>>();
        _wb = FindObjectOfType<WordBook>();

        Tetrino.TetrinoPlaced += DecreaseCounter;
    }

    private void FixedUpdate()
    {
        if (_counter < 3)
        {
            if (GiveNewTetrino())
            { _counter++; }
        }
    }

    public void DecreaseCounter()
    { _counter--; }

    public bool GiveNewTetrino()
    {
        if (!_table.CheckRTReady())
            return false;

        char[] letters;
        Thaum.Type[] thaums;
        Tetrino.Type t = (Tetrino.Type)(Random.Range((int)Tetrino.Type.T1, (int)Tetrino.Type.T5 + 1));

        if (t == Tetrino.Type.T5)
        {
            letters = _wb.GetLetters(4);
            thaums = new Thaum.Type[4];
        }
        else
        {
            letters = _wb.GetLetters(2);
            thaums = new Thaum.Type[2];
        }

        TetrinoDraggable tetrino = _pool.Get();
        tetrino.Construct(_table.GetCellSize(), t, letters, thaums);
        tetrino.transform.SetParent(this.transform);

        return true;
    }
}
