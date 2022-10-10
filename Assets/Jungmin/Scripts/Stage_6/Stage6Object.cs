using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Object : InteractObj
{
    [SerializeField] private Vector3[] movePoint;
    [SerializeField] private float moveSpeed;
    private bool isComplete = true;
    private void Awake()
    {
        movePoint[0] = tr.localPosition;
    }

    public override void Interaction()
    {
        if (!isComplete) return;

        var checkPos = (tr.localPosition == movePoint[0]) ? movePoint[1] : movePoint[0];
        isComplete = false;
        StartCoroutine(MoveToWall(checkPos));
    }

    IEnumerator MoveToWall(Vector3 targetPos)
    {   
        while (tr.localPosition != targetPos)
        {
            tr.localPosition = Vector3.MoveTowards(tr.localPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        isComplete = true;
    }
}
