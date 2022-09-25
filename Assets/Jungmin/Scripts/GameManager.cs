using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player_1;
    [SerializeField] private PlayerController player_2;
    [SerializeField] private
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwapPlayer();
        }
    }

    void SwapPlayer()
    {
        var swap = (player_1.isActive) ? false : true;

        player_1.isActive = swap;
        player_1.player_Camera.SetActive(swap);

        player_2.isActive = !swap;
        player_2.player_Camera.SetActive(!swap);
    }
}
