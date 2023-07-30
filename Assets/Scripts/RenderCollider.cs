using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RenderCollider
{
    public static void renderCircle(LineRenderer line, Vector2 center, float radius)
    {
        var points = new Vector3[line.positionCount];
        float angleStep = 360 / line.positionCount;
        Vector2 currentRotation = Vector2.right;
        for (int i = 0; i < line.positionCount; i++)
        {
            currentRotation = RotateVector(currentRotation, angleStep * i);
            points[i] = center + currentRotation * radius;
        }
        line.useWorldSpace = true;
        line.SetPositions(points);
    }
    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return vector.x * new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) + vector.y * new Vector2(-Mathf.Sin(radian), Mathf.Cos(radian));
    }
}
