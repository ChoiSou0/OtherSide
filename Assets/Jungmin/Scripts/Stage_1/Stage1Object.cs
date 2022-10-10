using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Object : InteractObj
{
    public override void Interaction()
    {
        StartCoroutine(Rotation(0.37f));
    }

    IEnumerator Rotation(float time)
    {
        Vector3 movePos = new Vector3(tr.position.x, 0.004f, tr.position.z);
        while (tr.position != movePos)
        {
            tr.position = Vector3.MoveTowards(tr.position, movePos, time);
            yield return new WaitForFixedUpdate();
        }
    }

}
