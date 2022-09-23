using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] public GameObject player_Camera;

    public bool isActive;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (isActive)
        {
            rb.velocity = new Vector3(-Pos.x * Speed, rb.velocity.y, -Pos.z * Speed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var checkObj = other.GetComponent<Interaction>();
        if(checkObj != null)
        {
            checkObj.isActive = true;
        }

        var checkObj2 = other.GetComponent<Interact2>();
        if (checkObj2 != null)
        {
            checkObj2.isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var checkObj1 = other.GetComponent<Interaction>();
        if (checkObj1 != null)
        {
            checkObj1.isActive = false;
        }

        var checkObj2 = other.GetComponent<Interact2>();
        if (checkObj2 != null)
        {
            checkObj2.isActive = false;
        }
    }
}
