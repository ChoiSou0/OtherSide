using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turn_Info : MonoBehaviour
{
    [SerializeField] private float Rotation = -90;
    [SerializeField] private float RotationPower = 90;
    [SerializeField] private int rotationdirection;
    public bool isTurn;
    public void Turn()
    {
        isTurn = true;

        gameObject.transform.DORotate(new Vector3(rotationdirection * Rotation, 0, 0)
        , 1, RotateMode.Fast);

        Rotation -= RotationPower;

        isTurn = false;
    }
}
