using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProductionEvent;
using Event = ProductionEvent.Event;

public class Stage3 : StageManager
{
    [SerializeField] InteractBrain interactBrain;
    private void Start()
    {
        StartCoroutine(Event.CameraShake(Camera.main, 0.8f, 3));    
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StageClear()
    {
        
    }

    protected override void isPortalCondition()
    {
        var conditionObj = interactBrain.interactions[1];
        if (conditionObj.interactionAngle == conditionObj.activeValues[1])
        {
            StartCoroutine(PortalApeear());
            isPortal = true;
        }
    }

    private IEnumerator PortalApeear()
    {
        StartCoroutine(Event.CameraShake(Camera.main, 0.8f, 3));
        StartCoroutine(Event.ObjectAppearance(portal.gameObject, portal.transform.position + Vector3.up * 10.5f, 3f));
        yield break;
    }

    private IEnumerator Stage3ClearEvent()
    {
        yield break;
    }
}

