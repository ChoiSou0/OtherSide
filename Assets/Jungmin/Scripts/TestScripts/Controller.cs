using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform currentNode;
    [SerializeField] private Transform targetNode;

    [SerializeField] private Queue<Walkable> walkPath = new Queue<Walkable>();

    private List<Transform> openList = new List<Transform>();
    private List<Transform> closedList = new List<Transform>();


    void Update()
    {
        RayCheckToCurrentNode();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                targetNode = mouseHit.transform;
                FindPath();
            }
        }
    }

    private void FindPath()
    {
        List<Walkable> finalList = new List<Walkable>();
        foreach (Node node in currentNode.GetComponent<Walkable>().neighborNode)
        {
            if (!node.isActive) continue;

            closedList.Add(currentNode.transform);
            openList.Add(node.nodePoint);
            ExplorePath(node);

        }
    }

    private void ExplorePath(Node startNode)
    {
        Walkable path = startNode.nodePoint.GetComponent<Walkable>();
        closedList.Add(startNode.nodePoint);

        foreach (Node node in path.neighborNode)
        {
            if (closedList.Contains(node.nodePoint))
            {
                continue;
            }
            openList.Add(node.nodePoint);

            if (targetNode != startNode.nodePoint)
            {
                ExplorePath(node);

            }
        }
    }

    private void RayCheckToCurrentNode()
    {
        Ray ray = new Ray(transform.GetChild(0).transform.position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(ray, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentNode = playerHit.transform;
            }
        }
    }
}
