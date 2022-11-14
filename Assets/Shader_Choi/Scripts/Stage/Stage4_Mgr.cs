using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cam_Control;
using Event = ProductionEvent.Event;
using GameManager = Jungmin.GameManager;


public class Stage4_Mgr : MonoBehaviour
{
    [SerializeField] private CameraShake shake;
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    [SerializeField] private Transform EndPoint1;
    [SerializeField] private Transform EndPoint2;
    private bool Ending;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Event.CameraMove(Camera.main, new(-15.82f, 18.29f, 16.96f), 150f));
    }

    // Update is called once per frame
    void Update()
    {
        if (((p1.currentNode == EndPoint1 && p2.currentNode == EndPoint2) || (p1.currentNode == EndPoint2 && p2.currentNode == EndPoint1)) && !Ending)
        {
            Ending = true;

            p1.gameObject.SetActive(Ending);
            p2.gameObject.SetActive(Ending);
            StartCoroutine(End());
        }
    }

    private IEnumerator End()
    {
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Last_Stage5");

        yield break;
    }
}