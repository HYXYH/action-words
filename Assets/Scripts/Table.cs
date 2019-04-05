using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{

    [SerializeField] private TableCell[] _cells;
    private char[][] _table;
    
    public TableCell GetAimedCell (GameObject block)
    {
        Vector3 blockPosition = block.transform.position;
        
        float cellWidth  = 24.9f;      // To change
        float cellHeight = 24.9f;      // To change


        foreach (TableCell cell in _cells)
        {
            Vector3 delta = cell.transform.position - blockPosition;
            if (Mathf.Abs(delta.x) < cellWidth && Mathf.Abs(delta.y) < cellHeight)
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
