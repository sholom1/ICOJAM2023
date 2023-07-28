using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrackPath : MonoBehaviour
{

    public PolygonCollider2D colliders;
    public EdgeCollider2D edge;

    public float line_width;
    // Start is called before the first frame update
    void Start()
    {
        DrawTrack();
    }
    private void OnDrawGizmos()
    {
        DrawTrack();
    }
    private void DrawTrack()
    {
        var points = colliders.points;
        print(points);

        int count = 0;
        int maxCount = colliders.GetTotalPointCount();

        LineRenderer line = colliders.gameObject.GetComponent<LineRenderer>();
        line.positionCount = maxCount + 1;
        line.widthMultiplier = line_width;
        line.useWorldSpace = false;

        while (count <= maxCount-1)
        {
            line.SetPosition(count, points[count]);
            count++;
        }

        line.SetPosition(maxCount, points[0]);

        //outer track
        LineRenderer edgeLine = edge.gameObject.GetComponent<LineRenderer>();
        count = 0;
        maxCount = edge.pointCount;

        List<Vector2> outerPoints = new List<Vector2>();
        outerPoints.Capacity = maxCount;
        edge.GetPoints(outerPoints);

        edgeLine.positionCount = maxCount;
        edgeLine.useWorldSpace = false;
        edgeLine.widthMultiplier = line_width;

        while (count <= maxCount-1)
        {
            edgeLine.SetPosition(count, outerPoints[count]);
            count++;
        }

    }
}
