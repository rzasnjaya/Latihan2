using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMove : MonoBehaviour
{
    public float speed = 2f, rotateSpeed = 2f, bankingValue = 5f;
    public bool rotateObject, loopMove;
    public int loopToNode;
    public List<Vector3> nodes = new List<Vector3>();

    private const int CURVE_SEGMENT = 20;
    private int realLoopNode;

    private void OnEnable()
    {
        StartCoroutine(StartMove());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator StartMove()
    {
        int posID = 0;
        List<Vector3> path = GetCurveNodes();

        while (loopMove || posID < path.Count - 1)
        {
            if ((path[posID] - transform.position).sqrMagnitude < 0.01f)
            {
                if (loopMove)
                {
                    if (posID < path.Count - 1)
                    {
                        posID += 1;
                    }
                    else
                    {
                        posID = realLoopNode;
                    }
                }
                else
                {
                    if (posID < path.Count - 1)
                    {
                        posID += 1;
                    }
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, path[posID], speed * Time.deltaTime);

            yield return null;
        }
    }

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
                p0 = transform.position;
                curvedNodes.Add(CalculateBezierPath(p0, p1, p2, p3, 0f));
            }

            for (int j = 0; j < CURVE_SEGMENT; j++)
            {
                float t = j / (float)CURVE_SEGMENT;
                curvedNodes.Add(CalculateBezierPath(p0, p1, p2, p3, t));
            }
        }

        realLoopNode = (int)(curvedNodes.Count * (loopToNode / (float)nodes.Count));

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

        for (int i = 1; i < nodes.Count; i++)
        {
            Color gizmoColor = Color.yellow;
            gizmoColor.a = 0.5f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(nodes[i - 1], nodes[i]);
        }
    }
}
