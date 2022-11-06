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
    private bool isIce;
    private bool isMove;
    private bool isJump;

    private float gravityVelocity;
    private Rigidbody rb;
    private SpringJoint joint;
    private Animator animator;

    private Vector3 movePos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();
        animator = model.GetComponent<Animator>();
    }

    private void Move(Vector3 Pos)
    {
        Vector3 movePos;
        if (isLocalMove)
        {
            var moveDir = ((transform.forward * Pos.x) * Speed) + (-(transform.right * Pos.z) * Speed);
            movePos = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
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
        Vector3 dir;
        if (isLocalMove) 
        {
            dir = (transform.forward * Pos.x) + (-(transform.right * Pos.z));
        }
        else
        {
            dir = new Vector3(movePos.x, 0, movePos.z);
        }
        model.transform.forward = dir;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isIce)
        {
            movePos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        else
        {
            movePos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        if (isActive)
        {
            Move(movePos);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void Update()
    {
        if (isDie) Die();
        GroundCheck();
        AnimationChange();

        if (Input.GetKeyDown(KeyCode.Space) && isActive && isGrounded)
        {
            Jump();
        }
    }


    private void AnimationChange()
    {
        isMove = (movePos == Vector3.zero) ? false : true;
        animator.SetBool("isMove", isMove);
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
 
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        rb.AddForce(new Vector3(0, JumpForce, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        var checkObj = other.GetComponent<InteractObj>();
        if (checkObj != null)
        {
            checkObj.isActive = true;
        }

        if (other.CompareTag("IceGround"))
        {
            isIce = true;
        }
        else
        {
            isIce = false;
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
