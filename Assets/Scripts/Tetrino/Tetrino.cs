using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrino : MonoBehaviour
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

    public const int __A__ = 2;
    [SerializeField]
    private BlockPool _blockPool;

    protected LetterBlock[] _blocks;
    private Transform _transform;
    private Table _table;

    private void Start()
    {
        _table = FindObjectOfType<Table>();
        _blockPool = FindObjectOfType<BlockPool>();
    }

    public void Construct(int nLetters, bool[][] blueprint, float size, char[] letters, Thaum.Type[] types)
    {
        int n = blueprint.Length;       float revN = 1 / (float)n;
        int m = blueprint[0].Length;    float revM = 1 / (float)m;

        RectTransform rect = GetComponent<RectTransform>();

        float cellSz = _table.GetCellSize();

        Debug.Log("Table cell size: " + cellSz + "x" + cellSz);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (m) * cellSz);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,   (n) * cellSz);

        Debug.Log("Tetrino created: table " + m + "x" + n + ", size: " + rect.rect.size.x + "x" + rect.rect.size.y);

        _blocks = new LetterBlock[nLetters];

        int k = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (blueprint[i][j]) {
                    _blocks[k] = _blockPool.Get();
                    _blocks[k].Construct(letters[k], types[k]);

                    _blocks[k].transform.SetParent(transform);
                    rect = _blocks[k].GetComponent<RectTransform>();
                    rect.anchorMin = new Vector2(revM *  j,      1 - revN * (i + 1));
                    rect.anchorMax = new Vector2(revM * (j + 1), 1 - revN *  i);

                    rect.offsetMax = Vector2.zero;
                    rect.offsetMin = Vector2.zero;
                    k++;
                }
            }
        }
    }

    
    
    public bool TakeAim()
    {
        bool placeable = true;

        foreach (LetterBlock block in _blocks)
        {
            block.FindAimedCell(_table);

            if (!block.CanBePlaced())
                placeable = false;
        }

        return placeable;
    }

    public delegate void Voidd();
    public static event Voidd TetrinoPlaced;

    public void PlaceBlocks()
    {
        foreach (LetterBlock block in _blocks)
        {
            block.StayAtCell();
        }

        TetrinoPlaced();
    }
    
    public void Rotate(float angle)
    {
        angle = Mathf.Deg2Rad * angle;

        int n = _blocks.Length;

        for (int i = 0; i < n; i++)
        {
            float x = _blocks[i].transform.localPosition.x;
            float y = _blocks[i].transform.localPosition.y;
            float z = _blocks[i].transform.localPosition.z;

            _blocks[i].transform.localPosition = new Vector3(Mathf.Cos(angle) * x - Mathf.Sin(angle) * y,
                                                             Mathf.Sin(angle) * x + Mathf.Cos(angle) * y,
                                                             z);
        }
    }

    public void SafeRotate90()
    {
        int n = _blocks.Length;
        for (int i = 0; i < n; i++)
        {
            _blocks[i].ResetLocalPosition();
            float x = _blocks[i].transform.localPosition.x;
            float y = _blocks[i].transform.localPosition.y;
            float z = _blocks[i].transform.localPosition.z;

            _blocks[i].transform.localPosition = new Vector3(-y, x, z);
        }
        RememberPosition();
    }

    public void RememberPosition()
    {
        int n = _blocks.Length;
        for (int i = 0; i < n; i++)
        {
            if (_blocks[i])
                _blocks[i].RememberLocalPosition();
        }
    }

    
}
