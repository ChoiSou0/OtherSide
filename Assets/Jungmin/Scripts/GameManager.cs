using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    public System.Action GameOver;

    [SerializeField] private PlayerController player_1;
    [SerializeField] private PlayerController player_2;

    private void Awake()
    {
        instance = this;
        GameOver += ReStart;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapPlayer();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Invoke("ExitStage", 0.5f);
        }
    }

    private void ExitStage()
    {
        SceneManager.LoadScene("SelectStage");
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwapPlayer()
    {
        var swap = (player_1.isActive) ? false : true;

        player_1.isActive = swap;
        player_1.player_Camera.SetActive(swap);

        player_2.isActive = !swap;
        player_2.player_Camera.SetActive(!swap);
    }
}
