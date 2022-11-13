using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePort : Walkable
{
    public GameObject interactPlayer = null;
    public PortalType portalType;
    public System.Func<Transform, Tween> GetTelePortAction;

    [SerializeField] Walkable linkedNode;
    [SerializeField] Walkable arrivalNode;

    private void Awake()
    {
        if (portalType == PortalType.InteractPlayer) GetTelePortAction = GetTelePortInteractPlayer;
        else GetTelePortAction = GetTelePortBasic;
    }

    public Tween GetTelePortInteractPlayer(Transform tr)
    {
        if (tr.gameObject == interactPlayer)
        {
            return tr.DOMove(GetWalkPoint(), 0.25f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    tr.transform.position = linkedNode.GetWalkPoint();
                    tr.DOMove(arrivalNode.GetWalkPoint(), 0.4f).SetEase(Ease.Linear);
                    tr.DOLookAt(arrivalNode.transform.position, .1f, AxisConstraint.Y, Vector3.up);
                });
        }
        return null;
    }

    public Tween GetTelePortBasic(Transform tr)
    {
        return tr.DOMove(GetWalkPoint(), 0.25f).SetEase(Ease.Linear)
            .OnComplete(() => tr.DOMove(linkedNode.GetWalkPoint(), 0f))
            .OnComplete(() => tr.DOMove(arrivalNode.GetWalkPoint(), 0.25f).SetEase(Ease.Linear))
            .OnPlay(() => transform.DOLookAt(tr.position, .1f, AxisConstraint.Y, Vector3.up));
    }
}
