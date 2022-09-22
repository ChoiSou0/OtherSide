using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float Speed;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        rb.velocity = new Vector3(-Pos.x * Speed, rb.velocity.y, -Pos.z * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        var checkObj = other.GetComponent<Interaction>();
        if(checkObj != null)
        {
            checkObj.isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var checkObj = other.GetComponent<Interaction>();
        if (checkObj != null)
        {
            checkObj.isActive = false;
        }
    }
}
