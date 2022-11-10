using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AppearVec
{
    X, Y, Z
}

public class Appear_Info
{
    public AppearVec appearVec;
    public float Distance;
    public float Time;
}

public class AppearObj : MonoBehaviour
{
    [SerializeField] private List<Appear_Info> appear_info;
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    private bool once;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((p1.currentNode == this.gameObject.transform || p2.currentNode == this.gameObject.transform)
            && !once)
        {
            once = true;


        }
    }

    
}
