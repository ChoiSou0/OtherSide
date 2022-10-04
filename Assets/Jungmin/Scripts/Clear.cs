using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<PlayerController>();
        if(obj != null)
        {
            Invoke("NextStage", 2f);
        }
    }

    void NextStage()
    {
        if(SceneManager.GetActiveScene().name == "Stage_2")
        {
            SceneManager.LoadScene("Title");
            return;
        }
        SceneManager.LoadScene("Stage_2");
    }
}
