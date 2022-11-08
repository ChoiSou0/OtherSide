using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;

public class Stage3 : StageManager
{
    [SerializeField] InteractBrain interactBrain;
    private bool isPortal = false;

    private void Start()
    {
        StartCoroutine(Event.CameraMove(Camera.main, new Vector3(19f, 18.6f, 18.59f), 180f));    
    }

    protected override void Update()
    {
        base.Update();
        if (!isPortal && player1.currentNode != null) PortalCondition();

        if (player2.playerType == PlayerType.Follow && player1.currentNode == portal)
            player1.OtherPlayerFollowMe(player1.currentNode);
    }

    protected override void StageClear()
    {
        StartCoroutine(Stage3ClearEvent());
    }

    protected override void ClearCheck()
    {
        if(player1.currentNode == portal && player2.currentNode == portal)
        {
            StageClear();
            isClearStage = true;
        }
    }

    private void PortalCondition()
    {
        var neighborNode = player1.currentNode.GetComponent<Walkable>().neighborNode;
        foreach (var node in neighborNode)
        {
            if (player2.currentNode == node.nodePoint && node.isActive)
            {
                StartCoroutine(PortalApeear());
                isPortal = true;
            }
        }      
    }

    private IEnumerator PortalApeear()
    {
        StartCoroutine(Event.CameraShake(Camera.main, 0.5f, 3));
        StartCoroutine(Event.ObjectAppearance(portal.gameObject, portal.transform.position + Vector3.up * 10.5f, 3f));
        yield return new WaitForSeconds(3);

        LayerChange(portal, 10);
        yield break;
    }

    private IEnumerator Stage3ClearEvent()
    {
        yield return new WaitForSeconds(0.08f); 
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(1);

        //æ¿ ¿Ãµø

        yield break;
    }
}

