using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] public GameObject player_Camera;
    [SerializeField] private GameObject model;

    [HideInInspector] public bool isDie;
    [SerializeField] private bool isLocalMove;
    public bool isActive;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Move(Vector3 Pos)
    {
        Vector3 movePos;
        if (isLocalMove)
        {
            movePos = ((transform.forward * Pos.x) * Speed) + (-(transform.right * Pos.z) * Speed);
            rb.velocity = new Vector3(movePos.x, rb.velocity.y, movePos.z);
        }
        else
        {
            movePos = new Vector3(-Pos.x * Speed, rb.velocity.y, -Pos.z * Speed);
            rb.velocity = movePos;
        }

        if (Pos == Vector3.zero) return;
        var dir = (isLocalMove) ? -movePos : Pos;
        model.transform.forward = dir;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 Pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (isActive)
        {
            Move(Pos);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isActive)
        {
            rb.AddForce(new Vector3(0, JumpForce, 0));
        }

        if (isDie) Die();
    }

    private void Die()
    {
        GameManager.instance.ReStart();
    }

    private void OnTriggerEnter(Collider other)
    {
        var checkObj = other.GetComponent<InteractObj>();
        if (checkObj != null)
        {
            checkObj.isActive = true;
        }

        if (other.CompareTag("DeathZone"))
        {
            isDie = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var checkObj1 = other.GetComponent<InteractObj>();
        if (checkObj1 != null)
        {
            checkObj1.isActive = false;
        }
    }
}
