using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;

public class Stage1 : StageManager
{
    protected override void ClearCheck()
    {
        if(player1.currentNode == portal)
        {
            StageClear();
            isClearStage = true;
        }
    }

    protected override void StageClear()
    {
        StartCoroutine(Stage1ClearEvent());
    }

    private IEnumerator Stage1ClearEvent()
    {
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));

        //æ¿ ¿Ãµø

        yield break;
    }
}
