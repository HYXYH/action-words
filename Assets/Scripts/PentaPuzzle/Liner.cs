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
        Debug.Log("ADDING NODE");

        _nodesCount++;
        _lineRenderer.positionCount = _nodesCount;

        Vector3 letterPos = letter.transform.position;
        letterPos.z -= 1;

        _lineRenderer.SetPosition(_nodesCount-1, letterPos);
    }

    private void ClearNodes(PentaLetter lastLetter)
    {
        Debug.Log("CLEARED NODES");

        _nodesCount = 0;
        _lineRenderer.positionCount = 0;
    }
}
