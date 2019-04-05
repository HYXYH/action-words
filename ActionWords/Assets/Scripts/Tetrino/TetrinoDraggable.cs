using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TetrinoDraggable : AttachedDraggable
{
    [SerializeField]
    private Pool<TetrinoDraggable> _pool;
    private bool _placeable;

    private Tetrino _tetrino;

    protected new void Awake()
    {
        _pool = FindObjectOfType<Pool<TetrinoDraggable>>();
        if (_pool == null)
        { Debug.LogError("Tetrino " + this.name + " started off not in a pool."); }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        _placeable = _tetrino.TakeAim();
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_placeable)
        {
            _tetrino.PlaceBlocks();

            base._ownerChanged = true;
            base.SetOwner(_pool.gameObject);

            _pool.Store(this);
        }

        base.OnEndDrag(eventData);
    }

    public void Construct(Tetrino.Type type, char[] letters, Thaum.Type[] thaums)
    {
        switch (type)
        {
            case Tetrino.Type.T1:
                _tetrino = new Tetrino1(letters, thaums, this.transform);
                break;
            case Tetrino.Type.T2:
                _tetrino = new Tetrino2(letters, thaums, this.transform);
                break;
            case Tetrino.Type.T3:
                _tetrino = new Tetrino3(letters, thaums, this.transform);
                break;
            case Tetrino.Type.T4:
                _tetrino = new Tetrino4(letters, thaums, this.transform);
                break;
            case Tetrino.Type.T5:
                _tetrino = new Tetrino5(letters, thaums, this.transform);
                break;

            default:
                Debug.LogError("Use tetrino types 1-5.");
                break;
        }

        /*
        switch (type)
        {
            case Tetrino.Type.T1:
                this.gameObject.AddComponent<Tetrino1>();
                break;
            case Tetrino.Type.T2:
                this.gameObject.AddComponent<Tetrino2>();
                break;
            case Tetrino.Type.T3:
                this.gameObject.AddComponent<Tetrino3>();
                break;
            case Tetrino.Type.T4:
                this.gameObject.AddComponent<Tetrino4>();
                break;
            case Tetrino.Type.T5:
                this.gameObject.AddComponent<Tetrino5>();
                break;

            default:
                Debug.LogError("Use tetrino types 1-5.");
                break;
        }
        */
    }
}
