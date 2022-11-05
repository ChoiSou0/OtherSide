using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cam_Control
{
    public class Cam_Ctrl : MonoBehaviour
    {
        static Transform NowPos;

        // 카메라 확대
        public static IEnumerator Enlargement(GameObject Cam, float Distance, float Time)
        {
            NowPos = Cam.transform;
            
            Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z + Distance),
                Time).SetEase(Ease.OutQuad); 
        
            yield break;
        }

        // 카메라 축소
        public static IEnumerator Reduction(GameObject Cam, float Distance, float Time)
        {
            NowPos = Cam.transform;

            Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z - Distance),
                Time).SetEase(Ease.OutQuad);

            yield break;    
        }

        public void Shake(Vector3 cameraPos, float Duration, float ShakeRange)
        {
            cameraPos = Camera.main.transform.position;
            InvokeRepeating(StartShake(ShakeRange), 0f, 0.005f);
            Invoke("StopShake", ShakeRange);
        }

        private void StartShake(float ShakeRange)
        {
            float cameraPosX = Random.value * ShakeRange * 2 - ShakeRange;
            float cameraPosY = Random.value * ShakeRange * 2 - ShakeRange;
            Vector3 cameraPos = Camera.main.transform.position;
            cameraPos.x += cameraPosX;
            cameraPos.y += cameraPosY;
            Camera.main.transform.position = cameraPos;
        }

        private void StopShake(Vector3 cameraPos)
        {
            CancelInvoke("StartShake");
            Camera.main.transform.position = cameraPos;
        }
    }
}