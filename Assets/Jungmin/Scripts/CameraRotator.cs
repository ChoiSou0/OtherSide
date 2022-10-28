using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X");
            transform.Rotate(0, x * Speed, 0);
        }
    }
}
