using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPortal : Walkable
{
    public GameObject interactPlayer;

    public Tween GetWalkPoint(Transform tr, bool check)
    {
        if (check) return tr.DOMove(GetWalkPoint(), 0.25f);

        return null;
    }
}
