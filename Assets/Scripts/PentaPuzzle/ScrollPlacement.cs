using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPlacement : MonoBehaviour
{
    private Pentagram _currentPentagram;

    private void Awake()
    {
        _currentPentagram = GetComponentInChildren<Pentagram>();
        if (!_currentPentagram) { Debug.LogError("ScrollPlacement didn't have an initial pentagram."); }
    }

    public Pentagram GetPentagram()
    { return _currentPentagram; }

    public void SetPentagram(Pentagram newPentagram)
    {
        _currentPentagram = newPentagram;
        newPentagram.transform.SetParent(transform);
    }
}
