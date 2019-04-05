using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : Pool<LetterBlock>
{
    override public void Store(LetterBlock item)
    {
        _pool.Add(item);
    }
}
