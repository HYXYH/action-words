using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pool<T> : MonoBehaviour
{
    [SerializeField]
    protected List<T> _pool;

    protected void Start()
    {
        if (_pool.Capacity == 0)
        {
            Debug.LogError("Capacity of pool \"" + this.name + "\" is 0.");
        }
    }

    virtual public T Get()
    {
        T item = _pool[0];
        _pool.RemoveAt(0);

        return item;
    }

    virtual public void Store(T item)
    {
        _pool.Add(item);
    }
}
