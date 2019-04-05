using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrino5 : Tetrino
{
    public Tetrino5(char[] letters, Thaum.Type[] types, Transform transform) : base(4, letters, types, transform)
    {
        Construct();
    }

    public override void Construct()
    {
        _blocks[0].transform.localPosition = new Vector3(-25, -25, 0);
        _blocks[1].transform.localPosition = new Vector3(-25, +25, 0);
        _blocks[2].transform.localPosition = new Vector3(+25, -25, 0);
        _blocks[3].transform.localPosition = new Vector3(+25, +25, 0);
    }
}
