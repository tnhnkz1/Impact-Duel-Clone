using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class EnemyAIBombDetection: MonoBehaviour
{
    public bool hasEntered;

    public static EnemyAIBombDetection instance;

    public GameObject newBomb;

    public List<GameObject> bombs = new List<GameObject>();

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            hasEntered = true;

            newBomb = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            bombs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            hasEntered = false;

            bombs.Remove(other.gameObject);
        }
    }
}
