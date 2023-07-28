using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrackPath : MonoBehaviour
{

    public PolygonCollider2D colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponent<PolygonCollider2D>();
        DrawTrack();
    }

    private void DrawTrack()
    {
        var points = colliders.points;
        print(points);

        int count = 0;
        int maxCount = colliders.GetTotalPointCount();

        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = maxCount;
        line.widthMultiplier = 0.1f;
        line.useWorldSpace = false;

        while (count <= maxCount)
        {
            line.SetPosition(count, points[count]);


            count++;
        }
    }
}
