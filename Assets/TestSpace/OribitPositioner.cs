using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OribitPositioner
{
    private OrbitPositionerSettings _settings;

    public OribitPositioner(OrbitPositionerSettings settings)
    {
        _settings = settings;
    }

    private int GetPieCount(int pointsCount) //як позбутися магії чисел?
    {
        int result; 

        if (_settings._plasementAngle == 360)
        {
            result = pointsCount;
        }
        else
        {
            if (pointsCount <= 1)
            {
                result = 1;
            }
            else
            {
                result = pointsCount - 1;
            }
        }

        return result;
    }

    private Quaternion CalculateRotation(float angle)
    {
        Vector3 temp = _settings.GetRotatableAxis();
        temp *= angle;

        return Quaternion.Euler(temp);
    }

    public List<Vector3> GetOrbitPosition(Vector3 center, int pointsCount)
    {
        List<Vector3> result = new List<Vector3>();

        int pieCount = GetPieCount(pointsCount);
        float pieAngle =  _settings._plasementAngle / pieCount;

        for (int i = 0; i < pointsCount; i++)
        {
            float currentAngle = pieAngle * i;
            currentAngle += _settings._offsetAngle;

            Quaternion rotation = CalculateRotation(currentAngle);

            Vector3 radius = _settings.GetStartSide();
            radius *= _settings._offsetRadius;

            Vector3 temp = center + (rotation * radius);

            result.Add(temp);
        }

        return result;
    }
}
