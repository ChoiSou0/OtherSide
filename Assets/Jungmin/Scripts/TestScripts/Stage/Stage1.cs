using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;
using DG.Tweening;

public class Stage1 : StageManager
{
    [SerializeField] GameObject clouds;

    private void Start()
    {
        StartCoroutine(CloudMove(1));
    }

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

    private IEnumerator CloudMove(int sign)
    {
        clouds.transform.DOLocalMove(new Vector3(0.5f, 0, sign * 0.5f), 5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(5.2f);

        StartCoroutine(CloudMove(-sign));
        yield break;
    }
}
