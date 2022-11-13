using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DesentObj : MonoBehaviour
{
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    [SerializeField] private Transform PlayNode;
    [SerializeField] private Walkable Node;
    [SerializeField] private float MinY;
    [SerializeField] private float MaxY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.currentNode == PlayNode || p2.currentNode == PlayNode)
        {
            StartCoroutine(Desent());
        }
        else
        {
            StartCoroutine(Increase());
        }
    }

    private IEnumerator Desent()
    {
        this.gameObject.transform.DOMoveY(MaxY, 3.5f);
        yield return new WaitForSecondsRealtime(3.5f);

        for (int i = 0; i < Node.neighborNode.Count; i++)
        {
            Node.neighborNode[i].isActive = true;
        }

        yield break;
    }

    private IEnumerator Increase()
    {
        for (int i = 0; i < Node.neighborNode.Count; i++)
        {
            Node.neighborNode[i].isActive = false;
        }

        this.gameObject.transform.DOMoveY(MinY , 3.5f);

        yield break;
    }
}
