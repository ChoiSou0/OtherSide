using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Object : InteractObj
{
    private Vector3 smoothrotReference;
    private bool isComplete = true;

    public override void Interaction()
    {
        if (!isComplete) return;
        isComplete = false;
        StartCoroutine(Rotation(tr.localEulerAngles.y + 90));
    }

    IEnumerator Rotation(float value)
    {
        print(value);
        while (value != Mathf.RoundToInt(tr.localEulerAngles.y))
        {
            tr.localEulerAngles = Vector3.SmoothDamp(tr.localEulerAngles,
                new Vector3(tr.localEulerAngles.x, value, tr.localEulerAngles.z), ref smoothrotReference, 0.9f);
            yield return new WaitForFixedUpdate();
        }
        tr.localEulerAngles = new Vector3(tr.localEulerAngles.x, Mathf.RoundToInt(tr.localEulerAngles.y), tr.localEulerAngles.z);
        isComplete = true;
    }
}
