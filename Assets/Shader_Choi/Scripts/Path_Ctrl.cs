using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Ctrl : MonoBehaviour
{
    public List<Transform> NearPath = new List<Transform>();

    public bool isStair;

    private float WalkOffset = 0.4f;
    private float StairOffset = 0f;
    public float stair;

    public Vector3 GetWalkPoint()
    {
        stair = isStair ? StairOffset : WalkOffset;
        return transform.position + transform.forward * stair;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(GetWalkPoint(), 0.1f);
    }
}
