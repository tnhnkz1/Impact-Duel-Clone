using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombForEnemyMovementController : MonoBehaviour
{
    private void Start()
    {
        float randomXAxis = Random.Range(2.45f, -2.872f);
        float yPos = Mathf.Clamp(transform.position.y, 1.055f, 1.055f);
        float randomZAxis = Random.Range(-3.415f, 2.183f);

        transform.DOMove(new Vector3(randomXAxis, yPos, randomZAxis), 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.GetComponent<Rigidbody>().AddForce(0, 200, -200);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            transform.GetComponent<Rigidbody>().AddForce(0, 200, 200);
        }
    }
}
