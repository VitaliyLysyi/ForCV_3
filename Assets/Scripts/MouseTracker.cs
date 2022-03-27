using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProjectA.Game
{
    public class MouseTracker
    {
        private Camera _camera;
        private Vector3 _cameraDirection;

        public MouseTracker()
        {
            _camera = Camera.main;
            _cameraDirection = _camera.transform.forward;
        }

        public Vector3 MouseWorldPosition(Vector3 mousePosition, float offsetDistance)
        {
            Vector3 mousePositionOffset = _cameraDirection * offsetDistance;

            mousePosition += mousePositionOffset;
            Vector3 result = _camera.ScreenToWorldPoint(mousePosition);

            return result;
        }
    }
}