using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{
    private int _rank = 5;
    private float _cellSize;
    private float _cellOffset;

    [SerializeField] private TableCell[] _cells;
    private char[][] _table;

    public bool CheckRTReady()
    {
        float rtWidth = GetComponent<RectTransform>().rect.width;
        _cellOffset = rtWidth / (_rank * 10 + 1);
        _cellSize = _cellOffset * 9;

        return (Mathf.Abs(rtWidth) > 0.001);
    }

    public float GetCellSize ()
    {
        return _cellSize;
    }

    private void Awake()
    {
        _cells = GetComponentsInChildren<TableCell>();
    }

    public TableCell GetAimedCell (Vector3 blockPosition)
    {
        RectTransform rt = _cells[0].GetComponent<RectTransform>();
        float cellSz = GetCellSize();

        foreach (TableCell cell in _cells)
        {
            Vector3 delta = cell.transform.position - blockPosition;
            if (Mathf.Abs(delta.x) < cellSz/2 && Mathf.Abs(delta.y) < cellSz/2)
            {
                return cell;
            }
        }

        return null;
    }

    public void FindAllWords ()
    {

    }
}
