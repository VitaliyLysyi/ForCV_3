using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour, ICharacterInteractable
{
    [SerializeField] private Node _startNode;
    [SerializeField] private Node _targetNode;
    [SerializeField] private LineRenderer _lineRenderer;

    private Node _previousStartNode;

    private void OnValidate()
    { 
        RefreshStartNodePathList();
    }

    private void OnDrawGizmos()
    {
        if (_startNode == null || _targetNode == null)
        {
            Gizmos.color = Color.red;

            for (int pointIndex = 0; pointIndex < _lineRenderer.positionCount; pointIndex++)
            {
                Gizmos.DrawSphere(_lineRenderer.GetPosition(pointIndex), 0.1f);
            }
        }

        ConnectSidePointsWithNodes();
    }

    public List<Vector3> GetPathPoints()
    {
        List<Vector3> points = new List<Vector3>();

        for (int position = 0; position < _lineRenderer.positionCount; position++)
        {
            points.Add(_lineRenderer.GetPosition(position));
        }

        return points;
    }

    private void ConnectSidePointsWithNodes()
    {
        if (_startNode != null)
        {
            _lineRenderer.SetPosition(0, _startNode.transform.position);
        }

        if (_targetNode != null)
        {
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _targetNode.transform.position);
        }
    }

    private void RefreshStartNodePathList()
    {
        if (_startNode != null)
        {
            if (_startNode != _previousStartNode)
            {
                _startNode.SetUpPath(gameObject.GetComponentInChildren<Path>());
            }

            _previousStartNode = _startNode;
        }
        else
        {
            if (_previousStartNode != null)
            {
                _previousStartNode.RemovePath(gameObject.GetComponentInChildren<Path>());
            }

            _previousStartNode = null;
        }
    }

    public Node GetTargetNode() { return _targetNode; }
}
