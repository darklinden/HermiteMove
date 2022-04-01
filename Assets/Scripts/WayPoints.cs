using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public List<Transform> _points;

    private void RefreshChild()
    {
        _points = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _points.Add(transform.GetChild(i));
        }
    }

    private void OnValidate()
    {
        RefreshChild();
    }

    private void OnEnable()
    {
        RefreshChild();
    }

    public List<Vector3> Points
    {
        get
        {
            var points = new List<Vector3>();

            for (int i = 0; i < _points.Count; i++)
            {
                points.Add(_points[i].position);
            }

            return points;
        }
    }


#if UNITY_EDITOR

    public Vector3 PointOnHermite(IList<Vector3> pointList, float progress)
    {
        if (pointList.Count == 2)
        {
            return pointList[0] + (progress * (pointList[1] - pointList[0]));
        }

        int idx = 0;
        float segmentCount = pointList.Count - 1;
        for (var i = 0; i < segmentCount; i++)
        {
            if (
                progress >= i / segmentCount &&
                progress < (i + 1) / segmentCount
            )
            {
                idx = i;
                break;
            }
        }

        var t = (progress - (idx / segmentCount)) * segmentCount;
        if (t < 0) t = 0;
        if (t > 1) t = 1;

        // determine control points of segment
        var p0 = pointList[idx];
        var p1 = pointList[idx + 1];

        Vector3 m0;
        if (idx > 0)
        {
            m0 = (pointList[idx + 1] - pointList[idx - 1]) * 0.5f;
        }
        else
        {
            m0 = pointList[idx + 1] - pointList[idx];
        }

        Vector3 m1;
        if (idx < pointList.Count - 2)
        {
            m1 = (pointList[idx + 2] - pointList[idx]) * 0.5f;
        }
        else
        {
            m1 = pointList[idx + 1] - pointList[idx];
        }

        float t2 = t * t;
        float t3 = t2 * t;
        var position =
            (2.0f * t3 - 3.0f * t2 + 1.0f) * p0 +
            (t3 - 2.0f * t2 + t) * m0 +
            (-2.0f * t3 + 3.0f * t2) * p1 +
            (t3 - t2) * m1;

        return position;
    }

    private Color? _lineColor;

    private void OnDrawGizmos()
    {
        DrawGizmos(false);
    }

    private void OnDrawGizmosSelected()
    {
        DrawGizmos(true);
    }

    private void DrawGizmos(bool selected)
    {
        if (_points == null) return;
        if (_points.Count <= 1) return;

        var points = Points;

        if (_lineColor == null)
        {
            _lineColor = UnityEngine.Random.ColorHSV();
        }

        Gizmos.color = (Color) _lineColor;
        var prev = PointOnHermite(points, 0);
        UnityEditor.Handles.Label(prev, "WayPoints", GUIStyle.none);
        for (float i = 0; i <= 1.0f; i += 0.01f)
        {
            var next = PointOnHermite(points, i);
            Gizmos.DrawLine (prev, next);
            prev = next;
        }
    }
#endif
}
