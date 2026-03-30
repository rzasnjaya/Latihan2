using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private SplineContainer path;
    [SerializeField] private PathLoopOption endOfPath;
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool isMoving = true;
    [SerializeField] private ShootOutEntry[] shootOutEntries;

    [Header("Debug Options")]
    [SerializeField] private float previewDistance;
    [SerializeField] private bool enableDebug;

    private float _distanceTravelled;
    private float _splineLength;

    void Start()
    {
        _splineLength = path.Spline.GetLength();

        foreach (var entry in shootOutEntries)
        {
            entry.shootOutPoint.Initialize(this);
        }
    }

    void Update()
    {
        if (path != null && isMoving)
        {
            _distanceTravelled += speed * Time.deltaTime;
            var normalizedDistance = CalculateNormalizeDistance(_distanceTravelled, _splineLength);
            RepositionCamera(normalizedDistance);

            for (int i = 0; i < shootOutEntries.Length; i++)
            {
                var entry = shootOutEntries[i];

                if (((Vector3)path.EvaluatePosition(entry.distance / _splineLength) - transform.position).sqrMagnitude < 0.01f)
                {
                    if (entry.shootOutPoint.AreaCleared) continue;

                    if (isMoving)
                        entry.shootOutPoint.StartShootOut(shootOutEntries[i].areaTimer);
                }
            }
        }
    }

    private void OnValidate()
    {
        if (enableDebug && path != null)
        {
            var normalizedDistance = CalculateNormalizeDistance(previewDistance, path.Spline.GetLength());
            RepositionCamera(normalizedDistance);
        }
    }

    private void RepositionCamera(float normalizedDistance)
    {
        path.Evaluate(normalizedDistance, out var position, out var forward, out var up);
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(forward, up);
    }

    private float CalculateNormalizeDistance(float distanceTravelled, float splineLength)
    {
        var normalized = distanceTravelled / splineLength;

        if (endOfPath == PathLoopOption.Stop)
        {
            normalized = Mathf.Clamp01(normalized);
        }
        else if (endOfPath == PathLoopOption.Loop)
        {
            normalized %= 1f;
            if (normalized < 0f)
                normalized = 1 + normalized;
        }

        return normalized;
    }

    public void SetPlayerMovement(bool enable)
    {
        isMoving = enable;
    }
}

[Serializable]
public class ShootOutEntry
{
    public ShootOutPoint shootOutPoint;
    public float distance;
    public float areaTimer = 15f;
}

public enum PathLoopOption
{
    Loop,
    Stop
}