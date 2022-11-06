using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cam_Control;

public class Player_Ctrl : MonoBehaviour
{
    public Transform NowNode;
    public Transform TargetNode;

    private void Start()
    {
        //Cam_Ctrl.Shake(Camera.main, 0.1f, 0.01f);
    }

    private void Update()
    {
        CheckNowNode();
        ChooseNode();
    }

    private void ChooseNode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                TargetNode = mouseHit.transform;
                //FindPathAndWalking();
            }
        }
    }

    private void CheckNowNode()
    {
        Ray ray = new Ray(transform.GetChild(0).transform.position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(ray, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                NowNode = playerHit.transform;
            }
        }
    }

    private void Click()
    {

    }
}
