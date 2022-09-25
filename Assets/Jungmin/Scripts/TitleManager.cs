using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

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
            SceneManager.LoadScene("Stage_1");
        }
    }

    IEnumerator TextAlpha(int sign)
    {
        while (true)
        {
            Color color = pText.color;
            color.a = color.a + (sign * Time.deltaTime);
            pText.color = color;
            if(pText.color.a >= 255 || pText.color.a <= 0)
            {
                break;
            }
            yield return null;
        }
        StartCoroutine(TextAlpha(-sign));
    }
}
