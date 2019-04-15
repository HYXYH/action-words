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
    public Tetrino CurrentTetrino() { return _tetrino; }


//  ~~~~~~~~~~~~~~~~~~~~~~~~ All the rotation ~~~~~~~~~~~~~~~~~~~~~~~~
    public void Rotate() { GetComponent<Animator>().SetTrigger("StartRotating"); }
    public void Rotate(float angle) { _tetrino.Rotate(angle); }

    [SerializeField]
    private float AnglePerSecond;
    private bool _rotating = false;

    void FixedUpdate()
    {
        if (_rotating)
        {
            Rotate(AnglePerSecond * Time.deltaTime);
        }
    }

    public void StartRotation()
    { _rotating = true; }

    public void StopRotation()
    {
        _rotating = false;
        _tetrino.SafeRotate90();
    }
    //  ~~~~~~~~~~~~~~~~~~~~~~~~ All the rotation ~~~~~~~~~~~~~~~~~~~~~~~~


    protected void Awake()
    {
        _tetrino = GetComponent<Tetrino>();
        _pool = FindObjectOfType<Pool<TetrinoDraggable>>();
        if (_pool == null)
        { Debug.LogError("Tetrino " + this.name + " started off not in a pool."); }
    }


    public override void OnBeginDrag(PointerEventData eventData)
    {
        _tetrino.RememberPosition();
        base.OnBeginDrag(eventData);
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

    public void Construct(float size, Tetrino.Type type, char[] letters, Thaum.Type[] thaums)
    {
        bool[][] blueprint = new bool[2][];
        blueprint[0] = new bool[2];
        blueprint[1] = new bool[2];

        int nBlocks = 2;
        switch (type)
        {
            case Tetrino.Type.T5:
                blueprint[0][0] = true; blueprint[0][1] = true;
                blueprint[1][0] = true; blueprint[1][1] = true;
                nBlocks = 4;
                break;
            case Tetrino.Type.T1:
                blueprint[0][0] = true;  blueprint[0][1] = true;
                blueprint[1][0] = false; blueprint[1][1] = false;
                break;
            case Tetrino.Type.T2:
                blueprint[0][0] = true;  blueprint[0][1] = false;
                blueprint[1][0] = true;  blueprint[1][1] = false;
                break;
            case Tetrino.Type.T3:
                blueprint[0][0] = true;  blueprint[0][1] = false;
                blueprint[1][0] = false; blueprint[1][1] = true;
                break;
            case Tetrino.Type.T4:
                blueprint[0][0] = false; blueprint[0][1] = true;
                blueprint[1][0] = true;  blueprint[1][1] = false;
                break;

            default:
                Debug.LogError("Use tetrino types 1-5.");
                break;
        }
        _tetrino.Construct(nBlocks, blueprint, size, letters, thaums);
    }
}
