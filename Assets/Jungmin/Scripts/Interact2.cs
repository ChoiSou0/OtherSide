using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact2 : MonoBehaviour, IRotatable
{
    [SerializeField] private GameObject interactionObject;
    [HideInInspector] public bool isActive = false;
    Vector3 smoothrotReference;

    Vector3 rot
    {
        get
        {
            return interactionObject.GetComponent<Transform>().position;
        }
        set
        {
            interactionObject.GetComponent<Transform>().position = value;
        }
    }

    public void RotateObject()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(Rotation(0.37f));
            }
        }
    }

    IEnumerator Rotation(float time)
    {
        Vector3 movePos = new Vector3(rot.x, 0.004f, rot.z);
        while (rot != movePos)
        {
            rot = Vector3.MoveTowards(rot, movePos, time);
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateObject();
    }
}
