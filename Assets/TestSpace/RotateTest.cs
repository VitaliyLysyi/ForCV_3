using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    [SerializeField] private int count;

    [SerializeField] private OrbitPositionerSettings _settings;

    private OribitPositioner _oribitPositioner;
    private List<Vector3> _points;

    private void OnDrawGizmos()
    {
        if (_points == null)
        {
            _points = new List<Vector3>();
        }

        if (_oribitPositioner == null)
        {
            _oribitPositioner = new OribitPositioner(_settings);
        }

        _points = _oribitPositioner.GetOrbitPosition(transform.position, count);

        if (_points.Count > 0)
        {
            Gizmos.color = Color.yellow;

            foreach (var point in _points)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }
    }
}
