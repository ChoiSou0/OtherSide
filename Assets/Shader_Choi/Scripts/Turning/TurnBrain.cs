using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBrain : MonoBehaviour
{
    public List<Turn_Info> TurnObject;
    [SerializeField] private int isTurn_Cnt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Turning();
    }

    private void Turning()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < TurnObject.Count; i++)
            {
                if (!TurnObject[i].isTurn)
                {
                    TurnObject[i].Turn();
                }
                else
                {
                    break; 
                }
            }
        }
    }

}
