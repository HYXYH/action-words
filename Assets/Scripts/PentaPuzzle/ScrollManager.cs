﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public enum PentagramPosition
    {
        Front,
        Back
    }

    [SerializeField] private ScrollPlacement[] _scrollPlacements;


    private float _pentagramRelativeRadius = 0.333f;
    private float _letterRelativeSize = 0.200f;

    void Awake()
    {
        foreach (ScrollPlacement sp in _scrollPlacements)
        {
            sp.GetScroll().AddThrownAwayCallback(Swap);
        }
    }

    public void ChangeScroll(Pentagram pentagram)
    {
        PrepareScroll(PentagramPosition.Back, pentagram);
        _scrollPlacements[(int)PentagramPosition.Front]
            .GetScroll()
            .GetComponent<Animator>()
            .SetTrigger("ThrownAway");
    }

    private void Swap(int thisIsNotUsed)
    {
        Scroll p0 = _scrollPlacements[0].GetScroll();
        Scroll p1 = _scrollPlacements[1].GetScroll();

        _scrollPlacements[0].SetPentagram(p1);
        _scrollPlacements[1].SetPentagram(p0);
    }

    private void PrepareScroll(PentagramPosition pentagramPosition, Pentagram pentagram)
    {
        Scroll scroll = _scrollPlacements[(int)pentagramPosition].GetScroll();
        scroll.ReturnLettersToPool();


        float screenWidth = GetComponent<RectTransform>().rect.width;
        //float screenWidth = Screen.width; Debug.Log("ScreenWidth " + screenWidth);
        float radius = screenWidth * _pentagramRelativeRadius;
        float letterSize = screenWidth * _letterRelativeSize; Debug.Log("letterSize " + letterSize);

        scroll.Load(pentagram, radius, letterSize);
    }


    public Scroll GetActivePentagram()
    {
        return _scrollPlacements[0].GetScroll();
    }
}