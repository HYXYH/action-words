using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableChecker : MonoBehaviour
{
    HashTable _table;
    
    void Start()
    {
        _table = new HashTable(50, @"C:\Users\BlindEyed\word_release.txt");

        Debug.Log ("абажур: " + _table.Check("абажур"));
        Debug.Log ("папа: " + _table.Check("папаха"));
        Debug.Log("стол: " + _table.Check("столец"));
        Debug.Log("артем: " + _table.Check("артем"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
