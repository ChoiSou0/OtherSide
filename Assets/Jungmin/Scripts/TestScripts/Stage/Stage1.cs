using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;

public class Stage1 : StageManager
{
    protected override void ClearCheck()
    {
        
    }

    protected override void StageClear()
    {
        StartCoroutine(Stage1ClearEvent());
    }

    private IEnumerator Stage1ClearEvent()
    {
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        //nextSceneName = "RE_Stage3";
        //GameManager.Instance.LoadStage(nextSceneName);

        yield break;
    }
}
