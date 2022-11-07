using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turn_Info : MonoBehaviour
{
    [SerializeField] private int rotationdirection;

    public void Turn()
    {
        Debug.Log("돌아간다");
        gameObject.transform.DOLocalRotate(new Vector3(gameObject.transform.rotation.x + rotationdirection * 90, gameObject.transform.rotation.y, gameObject.transform.rotation.z)
        , 2, RotateMode.LocalAxisAdd).SetEase(Ease.InQuad);
        //gameObject.transform.DOLocalRotateQuaternion(Quaternion.EulerAngles
        //    (gameObject.transform.rotation.x + rotationdirection * 90, gameObject.transform.rotation.y, gameObject.transform.rotation.z), 2).SetEase(Ease.InQuad);


    }
}
