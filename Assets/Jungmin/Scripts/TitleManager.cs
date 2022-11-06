using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Event = ProductionEvent.Event;
using Jungmin;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Text pText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextAlpha(-1));
    }

    // Update is called once per frame
    void Update()
    {
        PressKey();
    }

    void PressKey()
    {
        if (Input.anyKey)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("RE_Stage1");
    }

    IEnumerator TextAlpha(int sign)
    {
        while (true)
        {
            Color color = pText.color;
            color.a = color.a + (sign * Time.deltaTime);
            pText.color = color;

            if(pText.color.a >= 1 || pText.color.a <= 0)
            {
                color.a = (pText.color.a >= 1) ? 1 : 0;
                pText.color = color;
                break;
            }
            yield return null;
        }
        StartCoroutine(TextAlpha(-sign));
    }
}
