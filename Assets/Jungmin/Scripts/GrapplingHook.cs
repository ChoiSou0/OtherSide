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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && player.isActive)
        {
            StartHook();
        }  
        else if (Input.GetMouseButtonUp(1) && player.isActive)
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

            joint.spring = 15f;
            joint.damper = 15f;
            joint.massScale = 4.5f;
        }
    }

    private void StartHook()
    {  
        transform.position = player.transform.position; 
        Collider[] rings = 
            Physics.OverlapSphere(transform.position, grapplingDistance, LayerMask.GetMask("Ring"));

        if(rings.Length != 0)
        {
            GameObject ring = GetNearestRing(rings);
            ringPosition = ring.transform.GetChild(0).transform.position;

            isGrappling = true;
            Hook.gameObject.SetActive(true);
            rope.gameObject.SetActive(true);
            Hook.position = player.transform.position;
        }
    }

    private void StopHook()
    {
        isGrappling = false;
        Destroy(joint);
        Hook.gameObject.SetActive(false);
        rope.gameObject.SetActive(false);
    }

    GameObject GetNearestRing(Collider[] ring)
    {
        float minDistance = 0;
        GameObject nearest = null;

        foreach(Collider col in ring)
        {
            float dis = Vector3.Distance(player.transform.position, col.gameObject.transform.position);
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
        rope.SetPosition(0, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
        rope.SetPosition(1, Hook.position);
    }

    private void OnDrawGizmos()
    {
        Color color = Color.white;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, grapplingDistance);
    }
}
