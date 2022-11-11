using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cam_Control;

public class Stage2_Mgr : MonoBehaviour
{
    [SerializeField] private Controller p1;
    [SerializeField] private Transform EndPoint1;
    private bool Ending = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cam_Ctrl.FadeIn(GameObject.Find("FadeInOut").GetComponent<Image>(), 2));
        StartCoroutine(Cam_Ctrl.Move(Camera.main.gameObject, 
            new Vector3(13, 21.73f, 13), 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.currentNode == EndPoint1 && !Ending)
        {
            Ending = true;
            StartCoroutine(End());
        }
    }

    private IEnumerator End()
    {
        StartCoroutine(Cam_Ctrl.FadeOut(GameObject.Find("FadeInOut").GetComponent<Image>(), 2));
        yield return new WaitForSecondsRealtime(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("RE_Stage3");

        yield break;
    }
}
