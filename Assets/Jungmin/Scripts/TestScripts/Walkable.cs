using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Transform nodePoint;
    public bool isActive = true;
}

public class Walkable : MonoBehaviour
{
    public List<Node> neighborNode = new List<Node>();
    private float walkPointOffset = 0.5f;

    public Vector3 GetWalkPoint()
    {
        return transform.position + transform.up * walkPointOffset; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(GetWalkPoint(), 0.1f);
    }
}
