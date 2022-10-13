using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    // Start is called before the first frame update
    private void Awake()
    {
        ButtonSetting();
    }

    public void LoadStage(int stageNum)
    {
        SceneManager.LoadScene($"Stage{stageNum}");
    }

    private void ButtonSetting()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int temp = i;
            buttons[temp].onClick.AddListener(() => LoadStage(temp + 1));
        }
    }
}
