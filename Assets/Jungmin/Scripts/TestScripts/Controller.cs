using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform currentNode;
    [SerializeField] private Transform targetNode;

    [SerializeField] private Queue<Walkable> walkPathQueue = new Queue<Walkable>();

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
        List<Transform> pathList = new List<Transform>(100);
        foreach (Node node in currentNode.GetComponent<Walkable>().neighborNode)
        {
            if (!node.isActive) continue;

            closedList.Add(currentNode.transform);
            openList.Add(node.nodePoint);
            ExplorePath(node);

            if (openList[openList.Count - 1] == targetNode && pathList.Count > openList.Count) 
            {
                pathList = openList; 
            }
            ResetList();
        }

        if(pathList.Count != 100)
        {
            BuildPath(pathList);
            FollowPath();
        }
    }

    private void ExplorePath(Node startNode)
    {
        Walkable path = startNode.nodePoint.GetComponent<Walkable>();
        closedList.Add(startNode.nodePoint);

        foreach (Node node in path.neighborNode)
        {
            if (closedList.Contains(node.nodePoint) || !node.isActive)
            {
                continue;
            }
            
            if (targetNode != startNode.nodePoint)
            {
                openList.Add(node.nodePoint);
                ExplorePath(node);
            }
        }
    }

    private void BuildPath(List<Transform> pathList)
    {
        foreach(Transform path in pathList)
        {
            var walkable = path.GetComponent<Walkable>();
            walkPathQueue.Enqueue(walkable);
        }
        print(walkPathQueue.Dequeue());
    }

    private void FollowPath()
    {
        Sequence walk = DOTween.Sequence();

        for(; walkPathQueue.Count > 0;)
        {
            var path = walkPathQueue.Dequeue();
            walk.Append(transform.DOMove(path.GetWalkPoint(), 0.2f).SetEase(Ease.Linear));
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

    private void ResetList()
    {
        openList.Clear();
        closedList.Clear();
    }
}
