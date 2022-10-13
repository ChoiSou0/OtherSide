using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float groundCheckDis;

    [SerializeField] public GameObject player_Camera;
    [SerializeField] private GameObject model;

    [HideInInspector] public bool isDie;
    [SerializeField] private bool isLocalMove;
    public bool isActive;
    private bool isGrounded;

    private float gravityVelocity;
    private Rigidbody rb;
    private SpringJoint joint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();
    }

    private void Move(Vector3 Pos)
    {
        Vector3 movePos;
        if (isLocalMove)
        {
            var v1 = ((transform.forward * Pos.x) * Speed) + (-(transform.right * Pos.z) * Speed);
            movePos = new Vector3(v1.x, rb.velocity.y, v1.z);
        }
        else
        {
            movePos = new Vector3(-Pos.x * Speed, rb.velocity.y, -Pos.z * Speed);
        }

        //중력 가속도
        gravityVelocity = Time.deltaTime * Physics.gravity.y;
        if (isGrounded) gravityVelocity = 0f;

        rb.velocity = movePos + Vector3.up * gravityVelocity;
        
        //캐릭터 방향
        if (Pos == Vector3.zero) return;
        var dir = (isLocalMove) ? -(transform.forward * Pos.x) + ((transform.right *Pos.z)) : Pos;
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
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void Update()
    {
        GroundCheck();
        if (isDie) Die();

        if (Input.GetKeyDown(KeyCode.C) && isActive && isGrounded)
        {
            Jump();
        }
    }
   
    private void GroundCheck()
    {
        Vector3 checkPos = transform.position + Vector3.up * -1;
        Collider[] obj = Physics.OverlapSphere(checkPos, groundCheckDis, ~LayerMask.GetMask("Player"));

        if (obj.Length != 0) isGrounded = true;
        else isGrounded = false;
    }

    private void Die()
    {
        GameManager.instance.ReStart();
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0, JumpForce, 0));
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
