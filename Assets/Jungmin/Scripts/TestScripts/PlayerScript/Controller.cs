using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Controller : MonoBehaviour
{
    public Transform currentNode;
    public Transform targetNode;

    [SerializeField] protected Queue<Walkable> walkPathQueue = new Queue<Walkable>();

    protected List<Transform> openList = new List<Transform>();
    protected List<Transform> closedList = new List<Transform>();
    protected int nodeCount = 0;
    protected bool isEndBuild = false;

    private bool isWalking = false;
    private Sequence walk;


    protected virtual void Update()
    {
        RayCheckToCurrentNode();
        AnimationCheck();
        TouchScreen();
    }

    protected virtual void TouchScreen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                targetNode = mouseHit.transform;
                StopWalking();
                FindPathAndWalking();
            }
        }
    }

    protected void FindPathAndWalking()
    {
        List<Transform> pathList = new List<Transform>();
        int pathCount = 100;

        foreach (Node node in currentNode.GetComponent<Walkable>().neighborNode)
        {
            if (!node.isActive) continue;

            openList.Add(currentNode);
            closedList.Add(currentNode);

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

        if (pathList.Count != 0) BuildPath(pathList);
        else isEndBuild = true;
    }

    protected void ExplorePath(Node startNode)
    {
        Walkable path = startNode.nodePoint.GetComponent<Walkable>();
        closedList.Add(startNode.nodePoint);

        var temp = openList.ToList();
        for (int i = 0; i < path.neighborNode.Count; i++)
        {
            if (openList[openList.Count - 1] != targetNode && path.neighborNode.Count >= 3) openList = temp.ToList();

            if (closedList.Contains(path.neighborNode[i].nodePoint) || !path.neighborNode[i].isActive)
            {
                continue;
            }

            if (targetNode != startNode.nodePoint)
            {
                openList.Add(path.neighborNode[i].nodePoint);
                ExplorePath(path.neighborNode[i]);
            }
        }
    }

    protected virtual void BuildPath(List<Transform> pathList)
    {
        foreach (Transform path in pathList)
        {
            var walkable = path.GetComponent<Walkable>();
            nodeCount++;
            walkPathQueue.Enqueue(walkable);
        }
        isEndBuild = true;
        StartCoroutine(FollowPath());
    }

    protected virtual IEnumerator FollowPath()
    {
        walk = DOTween.Sequence();
        isWalking = true;

        for (; walkPathQueue.Count > 0;)
        {
            var path = walkPathQueue.Dequeue();

            if (path.transform == currentNode) continue;

            if (path.tag == "Teleport")
            {
                walk.Append(transform.DOMove(path.GetWalkPoint(), 0));
            }

            else
                walk.Append(transform.DOMove(path.GetWalkPoint(), 0.25f).SetEase(Ease.Linear));

            if (!path.donRotate)
                walk.Join(transform.DOLookAt(path.transform.position, .1f, AxisConstraint.Y, Vector3.up));

            transform.SetParent(path.transform);
        }
        walk.AppendCallback(() => StopWalking());

        yield break;
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

    protected void StopWalking()
    {
        isWalking = false;
        isEndBuild = false;

        walk.Kill();
        walkPathQueue.Clear();
        nodeCount = 0;

        transform.parent = currentNode.transform;
    }
}
