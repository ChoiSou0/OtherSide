using Jungmin;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Player : Controller
{
    public System.Action<Transform> OtherPlayerFollowMe;

    [SerializeField] private Player OtherPlayer = null;
    public bool isFollow;
    public bool isActive;

    protected override void Awake()
    {
        base.Awake();
        if (OtherPlayer != null && isFollow)
        {
            OtherPlayer.OtherPlayerFollowMe += FollowOther;
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void TouchScreen()
    {
        if (!isActive) return;
        base.TouchScreen();
    }

    protected override void BuildPath(List<Transform> pathList)
    {
        base.BuildPath(pathList);
        OtherPlayerFollowMe?.Invoke(pathList[pathList.Count - 2]);
    }

    //다른 플레이어 따라가기
    private void FollowOther(Transform target)
    {
        StopWalking();

        this.targetNode = target;
        FindPathAndWalking();
    }

}
