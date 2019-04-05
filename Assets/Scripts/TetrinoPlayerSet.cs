using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrinoPlayerSet : MonoBehaviour
{
    [SerializeField]
    Pool<TetrinoDraggable> _pool;

    WordBook _wb;

    void Start()
    {
        _pool = FindObjectOfType<Pool<TetrinoDraggable>>();
        _wb = FindObjectOfType<WordBook>();

        for (int i = 0; i <3; i++)
            GiveNewTetrino();
    }

    public void GiveNewTetrino()
    {
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
        
        tetrino.Construct(t, letters, thaums);

        tetrino.transform.SetParent(this.transform);
    }
}
