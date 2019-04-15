using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSettings : MonoBehaviour
{

    private TableCell[] _cells;

    // Start is called before the first frame update
    void Start()
    {

    }


    private void FindChildrenCells()
    {
        _cells = GetComponentsInChildren<TableCell>();
    }

    private void SetupSquareTable()
    {
        int n = _cells.Length;
        if (!Mathf.IsPowerOfTwo(n))
        {
            Debug.LogError("Can't make a square table if there is not squared ammount of cells.");
            return;
        }

        n = (int)Mathf.Sqrt ((float)n);



        for (int i = 0; i < n; i++)
        {

            for (int j = 0; j < n; j++)
            {

            }
        }
    }
}
