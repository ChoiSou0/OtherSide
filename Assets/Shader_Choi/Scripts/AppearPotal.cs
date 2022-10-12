using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPotal : MonoBehaviour
{
    [SerializeField] private GameObject Potal;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && 
            Input.GetKeyDown(KeyCode.F))
        {
            Potal.SetActive(true);
        }
    }
}
