using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoutThunder : MonoBehaviour
{
    [SerializeField] private GameObject Thunder;
    [SerializeField] private GameObject Mark;
    [SerializeField] private Vector3 SpawnVec;
    private bool Check;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shout();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Check)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Shout()
    {
        Mark.SetActive(true);
        Invoke("Spawn", 2f);
        Invoke("Effect", 2.1f);
    }

    private void Spawn()
    {
        Instantiate(Thunder, SpawnVec, Quaternion.identity);
    }

    private void Effect()
    {
        Check = true;
    }

}
