using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private Transform Hook;
    [SerializeField] private float grapplingDistance;
    [SerializeField] private float speed;

    private SpringJoint joint = null;
    private Vector3 ringPosition;
    private bool isGrappling = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && player.isActive)
        {
            StartHook();
        }  
        else if (Input.GetKeyUp(KeyCode.Z) && player.isActive)
        {
            StopHook();
        }

        if (isGrappling)
        {
            Grappling();
            DrawRope();
        }
    }

    private void Grappling()
    {
        Hook.position = Vector3.MoveTowards(Hook.position, ringPosition, speed * Time.deltaTime);

        if(Hook.position == ringPosition && joint == null)
        {
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = ringPosition;

            float distance = Vector3.Distance(player.gameObject.transform.position, ringPosition);

            joint.minDistance = distance * 0.25f;
            joint.maxDistance = distance * 0.8f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
        }
    }

    private void StartHook()
    {
        transform.position = player.transform.position;
        Hook.gameObject.SetActive(true);
        rope.gameObject.SetActive(true);
        isGrappling = true;

        Collider[] rings = 
            Physics.OverlapSphere(transform.position, grapplingDistance, LayerMask.GetMask("Ring"));

        if(rings != null)
        {
            GameObject ring = GetNearestRing(rings);
            ringPosition = ring.transform.GetChild(0).transform.position;
        }
    }

    private void StopHook()
    {
        isGrappling = false;
        Destroy(joint);
        Hook.gameObject.SetActive(false);
        rope.gameObject.SetActive(false);
        transform.position = player.transform.position;
    }

    GameObject GetNearestRing(Collider[] ring)
    {
        float minDistance = 0;
        GameObject nearest = null;

        foreach(Collider col in ring)
        {
            float dis = Vector3.Distance(transform.position, col.gameObject.transform.position);
            if (minDistance <= dis)
            {
                minDistance = dis;
                nearest = col.gameObject;
            }
        }
        return nearest;
    }

    private void DrawRope() 
    {
        rope.positionCount = 2;
        rope.SetPosition(0, player.gameObject.transform.position);
        rope.SetPosition(1, Hook.position);
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, grapplingDistance);
    }
}
