using Jungmin;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerType
{
    Basic,
    Follow,
}

public class Player : Controller
{
    public System.Action<Transform> OtherPlayerFollowMe = null;
    public System.Func<int, bool> MovePlayerDecision = null;

    public PlayerType playerType;
    [SerializeField] private Player OtherPlayer = null;

    protected override void Awake()
    {
        base.Awake();
        CheckOtherPlayer();
    }

    protected override void Update()
    {
        base.Update();
    }
    private void CheckOtherPlayer()
    {
        if (OtherPlayer == null) return;

        if (playerType == PlayerType.Follow)
        {
            OtherPlayer.OtherPlayerFollowMe += this.FollowOther;
        }
        else if (playerType == PlayerType.Basic && OtherPlayer.playerType == PlayerType.Basic)
        {
            MovePlayerDecision += this.ShortPathThenOther;
        }
    }

    protected override void TouchScreen()
    {
        if (playerType == PlayerType.Follow) return;
        base.TouchScreen();
    }

    protected override void BuildPath(List<Transform> pathList)
    {
        base.BuildPath(pathList);
        OtherPlayerFollowMe?.Invoke(pathList[pathList.Count - 2]);
    }

    protected override void FollowPath()
    {
        if (MovePlayerDecision != null)
        {
            if (!MovePlayerDecision(OtherPlayer.walkPathQueue.Count))
            {
                StopWalking();
                return;
            }
        }
        base.FollowPath();
    }

    private bool ShortPathThenOther(int otherPathCount)
    {
        print(otherPathCount);
        if (otherPathCount > this.walkPathQueue.Count || otherPathCount == 0) return true;
        else return false;
    }
    private void FollowOther(Transform target)
    {
        StopWalking();

        this.targetNode = target;
        FindPathAndWalking();
    }

}
