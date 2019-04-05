﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrino2 : Tetrino
{
    public Tetrino2(char[] letters, Thaum.Type[] types, Transform transform) : base(2, letters, types, transform)
    {
        Construct();
    }

    public override void Construct()
    {
        _blocks[0].transform.localPosition = new Vector3(0, -25, 0);
        _blocks[1].transform.localPosition = new Vector3(0, +25, 0);
    }
}
