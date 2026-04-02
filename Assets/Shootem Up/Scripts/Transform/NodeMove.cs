using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMove : MonoBehaviour
{
    public List<Vector3> nodes = new List<Vector3>();

    private const int CURVE_SEGMENT = 20;

    List<Vector3> GetCurveNodes()
    {
        List<Vector3> curvedNodes = new List<Vector3>();
        curvedNodes.Add(transform.position);

        for (int i = 0; i < nodes.Count - 3; i+= 3)
        {
            Vector3 p0 = nodes[i];
            Vector3 p1 = nodes[i + 1];
            Vector3 p2 = nodes[i + 2];
            Vector3 p3 = nodes[i + 3];

            if (i == 0)
            {
                curvedNodes.Add(CalculateBezierPath(p0, p1, p2, p3, 0f));
            }

            for (int j = 0; j < CURVE_SEGMENT; j++)
            {
                float t = j / (float)CURVE_SEGMENT;
                curvedNodes.Add(CalculateBezierPath(p0, p1, p2, p3, t));
            }
        }

        return curvedNodes;
    }

    Vector3 CalculateBezierPath(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float oneMinusT = 1f - t;

        Vector3 result = Mathf.Pow(oneMinusT, 3f) * p0 + 3f * Mathf.Pow(oneMinusT, 2f) * t * p1 
            + 3f * oneMinusT * (t * t) * p2 + Mathf.Pow(t, 3f) * p3;

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        List<Vector3> curvePositions = GetCurveNodes();

        for (int i = 1; i < curvePositions.Count; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(curvePositions[i - 1], curvePositions[i]);
        }
    }
}
