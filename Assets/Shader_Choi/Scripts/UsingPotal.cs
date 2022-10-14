using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingPotal : MonoBehaviour
{
    [SerializeField] private GameObject LinkPotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") &&
            gameObject.activeSelf && LinkPotal.activeSelf &&
            Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("telpo");
            other.transform.position = LinkPotal.transform.position;
        }
    }
}