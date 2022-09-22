using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour, IRotatable
{
    [HideInInspector] public bool isActive = false;
    Vector3 smoothrotReference;

    Vector3 rot
    {
        get
        { 
            return GetComponent<Transform>().eulerAngles;
        }

        set
        {
            GetComponent<Transform>().eulerAngles = value;
        }
    }
    
    public void RotateObject()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(Rotation(90));
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(Rotation(-90));
            }
        }
    }

    IEnumerator Rotation(float value)
    {
        var defaultValue = rot.y;
        while (defaultValue + value != Mathf.RoundToInt(rot.y))
        {
            rot = Vector3.SmoothDamp(rot, new Vector3(rot.x, defaultValue + value, rot.z), ref smoothrotReference, 0.9f);
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateObject();
    }
}
