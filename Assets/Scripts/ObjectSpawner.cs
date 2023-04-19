using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectSpawner : MonoBehaviour
{
    public Transform __bombForPlayer;

    public Transform __bombForEnemy;

    public Transform __collectable;

    [SerializeField] private Transform __startPointForPlayerBombs;
    [SerializeField] private Transform __startPointForEnemyBombs;

    public static ObjectSpawner instance;

    private void Start()
    {
        StartCoroutine(ProduceBombs());
        StartCoroutine(ProduceCollectable());

        instance = this;
    }

    private void Update()
    {
        __bombForPlayer.position = __startPointForPlayerBombs.position;
        __bombForEnemy.position = __startPointForEnemyBombs.position;

        if (transform.childCount >= 6)
        {
            __startPointForEnemyBombs.DOMoveX(6.78f, 0.1f);
            __startPointForPlayerBombs.DOMoveX(6.78f, 0.1f);
        }
    }

    IEnumerator ProduceBombs()
    {
        while (true)
        {
            if (transform.childCount < 100)
            {
                Instantiate(__bombForPlayer);
                Instantiate(__bombForEnemy);
            }

            yield return new WaitForSecondsRealtime(4);
        }
    }

    IEnumerator ProduceCollectable()
    {
        while (true)
        {
            if (transform.childCount < 100)
            {
                Instantiate(__collectable);

                float randomXAxis = Random.Range(2.45f, -2.872f);
                float yPos = Mathf.Clamp(transform.position.y, 1.14f, 1.14f);
                float randomZAxis = Random.Range(-4.065f, -9.424f);

                __collectable.transform.position = new Vector3(randomXAxis, yPos, randomZAxis);
            }

            yield return new WaitForSecondsRealtime(4);
        }
    }
}
