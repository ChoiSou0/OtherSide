using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cam_Control;

public class Stage2 : MonoBehaviour
{
    [SerializeField] private Player1 player1;
    [SerializeField] private Player1 player2;

    [SerializeField] Transform End1;
    [SerializeField] Transform End2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cam_Ctrl.FadeIn(GameObject.Find("FadeInOut").GetComponent<Image>(), 2));
        StartCoroutine(Cam_Ctrl.Move(GameObject.Find("Cam1") ,new Vector3(-21.87f, 24.18f, 20.38f), 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.transform.position == End1.position)
        {
            if (player2.transform.position == End2.position)
            {
                StartCoroutine(Ending());
            }
        }
    }

    private IEnumerator Ending()
    {
        StartCoroutine(Cam_Ctrl.FadeOut(GameObject.Find("FadeInOut").GetComponent<Image>(), 2));
        yield return new WaitForSecondsRealtime(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Re_Stage3");

        yield break;
    }
}
