using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeRange;
    private Vector3 cameraPos;

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

    private void StopShake()
    {
        CancelInvoke("StartShake");
        Camera.main.transform.position = cameraPos;
    }
}
