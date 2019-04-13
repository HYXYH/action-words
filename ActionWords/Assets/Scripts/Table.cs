using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{

    [SerializeField] private TableCell[] _cells;
    private char[][] _table;

    public float GetCellSize ()
    {
        return _cells[0].GetComponent<RectTransform>().rect.size.y;
    }

    private void Awake()
    {
        _cells = GetComponentsInChildren<TableCell>();
        Debug.LogError("ON AWAKE: Cell size: "+ GetCellSize());
    }

    public TableCell GetAimedCell (Vector3 blockPosition)
    {
        RectTransform rt = _cells[0].GetComponent<RectTransform>();

        Debug.Log("Cell 0.\nSize: " + rt.rect.width + "x" + rt.rect.height + "\nOffsets: " + rt.rect.left + ", " + rt.rect.bottom + ", " + rt.rect.right + ", " + rt.rect.top +
                  "\nDeltaSize: " + rt.sizeDelta.x + "x" + rt.sizeDelta.y + "\nSizeVector2: " + rt.rect.size.x + "x" + rt.rect.size.y);
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
