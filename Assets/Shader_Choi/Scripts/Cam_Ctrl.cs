using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cam_Control
{
    public class Cam_Ctrl : MonoBehaviour
    {
    static Transform NowPos;

        public static IEnumerator Enlargement(GameObject Cam, float Distance, float Time)
        {
            NowPos = Cam.transform;
            
            Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z + Distance),
                Time).SetEase(Ease.OutQuad); 
        
            yield break;
        }

        public static IEnumerator Reduction(GameObject Cam, float Distance, float Time)
        {
            NowPos = Cam.transform;

            Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z - Distance),
                Time).SetEase(Ease.OutQuad);

            yield break;    
        }
    }
}