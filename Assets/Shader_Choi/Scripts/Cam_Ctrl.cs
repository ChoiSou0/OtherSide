using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Cam_Control
{
    public class Cam_Ctrl : MonoBehaviour
    {
        private Vector3 cameraPos;
        private static float shakeRange;
        private static bool Cancel;

        public static IEnumerator Move(GameObject Cam ,Vector3 Pos, float Time)
        {
            Cam.transform.DOMove(Pos, Time).SetEase(Ease.OutQuad);

            yield break;
        }

        public static IEnumerator BackMove(GameObject Cam, Vector3 Pos, float Time)
        {
            Cam.transform.DOMove(Pos, Time).SetEase(Ease.OutQuad);

            yield break;
        }

        public static IEnumerator EnlargementAndReduction(Camera Cam, float Size, float Time)
        {
            Cam.DOOrthoSize(Size, Time).SetEase(Ease.OutQuad);

            foreach (Transform ChildCam in Cam.transform)
            {
                Camera camera = ChildCam.gameObject.GetComponent<Camera>();
                camera.DOOrthoSize(Size, Time).SetEase(Ease.OutQuad);
            }

            yield break;
        }
        // 일단 놔두는 코드
        #region
        // 카메라 확대 (카메라, 거리, 시간)
        //public static IEnumerator Enlargement(GameObject Cam, float Distance, float Time)
        //{
        //    NowPos = Cam.transform;

        //    Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z + Distance),
        //        Time).SetEase(Ease.OutQuad); 

        //    yield break;
        //}

        // 카메라 축소 (카메라, 거리, 시간)
        //public static IEnumerator Reduction(GameObject Cam, float Distance, float Time)
        //{
        //    NowPos = Cam.transform;

        //    Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z - Distance),
        //        Time).SetEase(Ease.OutQuad);

        //    yield break;    
        //}
        #endregion

        // 카메라 흔들기 (현재 카메라 위치, 진행시간, 흔드는 범위(0.01하면 평균))
        #region
        public void Shake(Camera Cam, float Duration, float ShakeRange)
        {
            shakeRange = ShakeRange;
            cameraPos = Cam.transform.position;
            InvokeRepeating("StartShake", 0f, 0.005f);
            Invoke("StopShake", Duration);
        }

        private void StartShake()
        {
            float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
            float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
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

        #endregion

        public static IEnumerator FadeIn(Image Fade, float Time)
        {
            Fade.DOColor(Color.black, 0);
            Fade.DOColor(Color.white, Time);

            yield break;
        }

        public static IEnumerator FadeOut(Image Fade, float Time)
        {
            Fade.DOColor(Color.black, Time);

            yield break;
        }
    }
}