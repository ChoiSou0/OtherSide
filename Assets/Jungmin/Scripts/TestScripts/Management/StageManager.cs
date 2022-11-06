using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageManager : MonoBehaviour
{
    [HideInInspector] public bool isClearStage = false;

    [SerializeField] private List<PathCondition> pathConditions;
    [SerializeField] protected Transform portal;
    [SerializeField] LayerMask portalLayerMask;
    protected bool isClearOneTime = false;
    protected bool isPortal= false;

    public void ConnectPathOfStage()
    {
        foreach(PathCondition condition in pathConditions)
        {
            if (!condition.interactionObject.isInteract) continue;

            var interact = condition.interactionObject;
            Vector3 checkValue = 
                (interact.interactType == InteractType.Rotate) ? interact.interactionAngle : interact.interactionPosition;

            if(condition.activeValue == checkValue)
            {
                for (int i = 0; i < condition.nodes.Count; i++)
                {
                    var node = condition.nodes[i];
                    if (node.isConnectNode)
                    {
                        node.walkable.neighborNode[node.index].isActive = true;
                    }
                    else
                    {
                        node.walkable.neighborNode[node.index].isActive = false;
                    }
                }
            }
        }
    }

    protected virtual void Update()
    {
        if(!isClearStage) ConnectPathOfStage();
        if(!isClearOneTime) ClearCheck();
        if (!isPortal) isPortalCondition();
    }

    public abstract void StageClear();
    protected abstract void isPortalCondition();
    private void ClearCheck()
    {
        if (isClearStage)
        {
            StageClear();
            isClearOneTime = true;
        }
    }
}

[System.Serializable]
public class PathCondition
{
    public Interaction interactionObject;
    public Vector3 activeValue;
    public List<NodeData> nodes;
}

[System.Serializable]
public class NodeData
{
    public Walkable walkable;
    public int index;
    public bool isConnectNode;
}
