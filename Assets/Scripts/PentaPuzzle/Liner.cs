using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Liner : MonoBehaviour
{
    
    private LineRenderer _lineRenderer;

    private int _nodesCount;

    private void Awake()
    { _lineRenderer = GetComponent<LineRenderer>(); }

    void Start()
    {
        PentaLetter[] letters = FindObjectsOfType<PentaLetter>();
        foreach (PentaLetter letter in letters)
        {
            letter.AddDragEndedCallback(ClearNodes);
            letter.AddLetterSelectedCallback(AddNode);
        }
    }



    
    private void AddNode(PentaLetter letter)
    {
        _nodesCount++;
        _lineRenderer.positionCount = _nodesCount;

        Vector3 letterPos = letter.transform.position;
        letterPos.z -= 1;

        _lineRenderer.SetPosition(_nodesCount-1, letterPos);
    }

    public void ClearNodes(PentaLetter lastLetter)
    {
        _nodesCount = 0;
        _lineRenderer.positionCount = 0;
    }
}
