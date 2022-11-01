using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public partial class Controller : MonoBehaviour
{
    [SerializeField] private Transform currentNode;
    [SerializeField] private Transform targetNode;

    [SerializeField] private Queue<Walkable> walkPathQueue = new Queue<Walkable>();

    [SerializeField] private List<Transform> openList = new List<Transform>();
    [SerializeField] private List<Transform> closedList = new List<Transform>();

    private bool isWalking = false;

    void Update()
    {
        RayCheckToCurrentNode();
        TouchScreen();
        AnimationCheck();
    }

    private void TouchScreen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isWalking) StopWalking();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                targetNode = mouseHit.transform;
                FindPathAndWalking();
            }
        }
    }

    private void FindPathAndWalking()
    {
        List<Transform> pathList = new List<Transform>();
        int pathCount = 100;

        foreach (Node node in currentNode.GetComponent<Walkable>().neighborNode)
        {
            if (!node.isActive) continue;

            closedList.Add(currentNode.transform);
            openList.Add(node.nodePoint);
            ExplorePath(node);

            if (openList[openList.Count - 1] == targetNode && pathCount > openList.Count)
            {
                var tempList = openList.ToList();
                pathCount = tempList.Count;
                pathList = tempList;
            }
            ResetList();
        }

        if (pathList.Count != 0 && !isWalking)
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
        foreach (Transform path in pathList)
        {
            var walkable = path.GetComponent<Walkable>();
            walkPathQueue.Enqueue(walkable);
        }
    }

    private void FollowPath()
    {
        Sequence walk = DOTween.Sequence();
        isWalking = true;

        for (; walkPathQueue.Count > 0;)
        {
            var path = walkPathQueue.Dequeue();

            walk.Append(transform.DOMove(path.GetWalkPoint(), 0.25f).SetEase(Ease.Linear));

            if (!path.donRotate)
                walk.Join(transform.DOLookAt(path.transform.position, .1f, AxisConstraint.Y, Vector3.up));
        }
        walk.AppendCallback(() => isWalking = false);
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

    private void StopWalking()
    {
        DOTween.KillAll();
        walkPathQueue.Clear();
        isWalking = false;
    }
}
