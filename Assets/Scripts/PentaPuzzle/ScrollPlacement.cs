using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPlacement : MonoBehaviour
{
    [SerializeField]
    private Scroll _currentScroll;

    private void Awake()
    {
    }

    public Scroll GetScroll()
    { return _currentScroll; }

    public void SetPentagram(Scroll newPentagram)
    {
        _currentScroll = newPentagram;
        newPentagram.transform.SetParent(transform);
    }
}
