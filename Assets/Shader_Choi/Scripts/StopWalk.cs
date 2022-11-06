using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWalk : Controller1
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (base.isWalking)
        {
            base.StopWalking();
            if (gameObject.transform == base.targetNode)
            {
                
            }
        }
    }
}
