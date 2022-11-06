using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Player1 : Controller1
{
    public bool isActive;
    void Start()
    {
        
    }

    protected override void Update()
    {
        if (isActive)
            base.Update();
    }
}
