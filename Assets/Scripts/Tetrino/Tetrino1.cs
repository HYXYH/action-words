using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrino1 : Tetrino
{
    public Tetrino1(char[] letters, Thaum.Type[] types, Transform transform) : base(2, letters, types, transform)
    {
        Construct();
    }

    public override void Construct()
    {
        _blocks[0].transform.localPosition = new Vector3(-25, 0, 0);
        _blocks[1].transform.localPosition = new Vector3(+25, 0, 0);
    }

    /*
    public override void Construct(char[] letters, Thaum.Type[] types, Transform transform)
    {
        base.Construct(2, letters, types, transform);
        _blocks[0].transform.localPosition = new Vector3(-25, 0, 0);
        _blocks[1].transform.localPosition = new Vector3(+25, 0, 0);
    }
    */
}
