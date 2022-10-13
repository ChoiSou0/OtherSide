using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private readonly int WaitTime = 2;
    private readonly int FireTime = 5;
    private readonly int FireDelayTime = 1;

    [SerializeField] private GameObject flame;
    private BoxCollider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();

        StartCoroutine(FlameRadiation());
    }

    IEnumerator FlameRadiation()
    {
        while (true)
        {
            flame.SetActive(!flame.activeSelf);

            if (!coll.enabled)
            {
                yield return new WaitForSeconds(FireDelayTime);
            }
            coll.enabled = !coll.enabled;

            if (flame.activeSelf) yield return new WaitForSeconds(FireTime);
            else yield return new WaitForSeconds(WaitTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            player.isDie = true;
        }
    }
}
