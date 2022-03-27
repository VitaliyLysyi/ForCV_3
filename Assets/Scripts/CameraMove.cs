using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _horizontalLimit;
    [SerializeField] private Transform _verticalLimit;
    [SerializeField] private float _moveSpeed = 10f;

    private List<Vector3> _rectangleVertices;

    private void OnDrawGizmos()
    {
        RefreshBounds();

        Gizmos.color = Color.red;
        if (_rectangleVertices.Count > 4)
        {
            for (int index = 0; index < _rectangleVertices.Count - 1; index++)
            {
                Gizmos.DrawLine(_rectangleVertices[index], _rectangleVertices[index + 1]);
            }
        }
    }

    private void Update()
    {
        Vector3 horizontal = Vector3.right * Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        Vector3 vertical = Vector3.up * Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + horizontal + vertical;

        if (_horizontalLimit != null && _verticalLimit != null)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, _horizontalLimit.position.x, _horizontalLimit.position.x * -1);
            newPosition.y = Mathf.Clamp(newPosition.y, _verticalLimit.position.y * -1, _verticalLimit.position.y);
        }

        transform.position = newPosition;
    }

    private void RefreshBounds()
    {
        if (_horizontalLimit != null && _verticalLimit != null)
        {
            _rectangleVertices = new List<Vector3>();
            _rectangleVertices.Add((Vector3.right * _horizontalLimit.position.x) + (Vector3.up * _verticalLimit.position.y));
            _rectangleVertices.Add((Vector3.left * _horizontalLimit.position.x) + (Vector3.up * _verticalLimit.position.y));
            _rectangleVertices.Add((Vector3.left * _horizontalLimit.position.x) + (Vector3.down * _verticalLimit.position.y));
            _rectangleVertices.Add((Vector3.right * _horizontalLimit.position.x) + (Vector3.down * _verticalLimit.position.y));
            _rectangleVertices.Add(_rectangleVertices[0]);
        }
    }
}
