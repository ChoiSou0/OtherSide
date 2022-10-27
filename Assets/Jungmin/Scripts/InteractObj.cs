using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObj : MonoBehaviour
{
    [SerializeField] protected GameObject interactObject;
    public bool isActive = false;
    protected Transform tr
    {
        get
        {
            return interactObject.GetComponent<Transform>();
        }
        set
        {
            tr = value;
        }
    }

    public abstract void Interaction();

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interaction();
            }
        }
    }
}
     