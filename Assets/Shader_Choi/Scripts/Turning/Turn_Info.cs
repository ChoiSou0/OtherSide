using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turn_Info : MonoBehaviour
{
    [SerializeField] private int rotationdirection;

    public void Turn()
    {
        gameObject.transform.DORotate(new Vector3(gameObject.transform.rotation.x + rotationdirection * 90, gameObject.transform.rotation.y, gameObject.transform.rotation.z)
        , 2, RotateMode.WorldAxisAdd);
        

    }
}
