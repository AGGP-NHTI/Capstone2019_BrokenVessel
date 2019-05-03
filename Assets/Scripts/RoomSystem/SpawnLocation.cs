using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [SerializeField] GameObject Player;
    void Awake()
    {
        GameObject P = GameObject.Find("Player");
        if (P) { P.transform.position = transform.position; }
        else { Instantiate(Player, transform.position, transform.rotation); }
    }
}
