using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AppearObj : MonoBehaviour
{
    [SerializeField] private List<Appear_Info> appear_Info;
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    private bool once;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((p1.currentNode == this.gameObject.transform || p2.currentNode == this.gameObject.transform)
            && !once)
        {
            once = true;

            Appear();
        }
    }

    private IEnumerator Appear()
    {
        for (int i = 0; i < appear_Info.Count; i++)
        {
            if (appear_Info[i].isShake)
            {

            }
            else
            {

            }

            switch (appear_Info[i].appearVec)
            {
                case AppearVec.X:
                    appear_Info[i].gameObject.transform.DOMoveX(appear_Info[i].Distance, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;

                case AppearVec.Y:
                    appear_Info[i].gameObject.transform.DOMoveY(appear_Info[i].Distance, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;

                case AppearVec.Z:
                    appear_Info[i].gameObject.transform.DOMoveZ(appear_Info[i].Distance, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;
            }

            yield return new WaitForSecondsRealtime(0.3f);
        }

        yield break;
    }
    
}
